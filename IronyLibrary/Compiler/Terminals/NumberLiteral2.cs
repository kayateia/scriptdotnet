//Revised version by Kabelsalat, posted to Irony site on codePlex 

#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for Irony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Irony.Compiler {
  using BigInteger = Microsoft.Scripting.Math.BigInteger;


  //TODO: For VB, we may need to add a flag to automatically use long instead of int (default) when number is too large
  public class NumberLiteral : CompoundTerminalBase {

    #region constructors
    public NumberLiteral(string name, TermOptions options)  : this(name) {
      SetOption(options);
    }
    public NumberLiteral(string name) : base(name) {
      base.MatchMode = TokenMatchMode.ByType;
    }
    public NumberLiteral(string name, string alias) : this(name) {
      base.Alias = alias;
    }
    #endregion 

    #region Public fields/properties: ExponentSymbols, Suffixes
    public string ExponentSymbols = "eE"; //most of the time; in some languages (Scheme) we have more

		public char DecimalSeparator {
			get	{
				return numberFormat.NumberDecimalSeparator[0];
			}	set {
				numberFormat.NumberDecimalSeparator = value.ToString();
			}
		}

    //Default types are assigned to literals without suffixes
    public TypeCode DefaultIntType = TypeCode.Int32;  
    public TypeCode DefaultFloatType = TypeCode.Double;

    #endregion

    #region events: ConvertingValue
    public event EventHandler<ScannerConvertingValueEventArgs> ConvertingValue; 
    #endregion

    #region Private fields
    private string _quickParseTerminators;
		private NumberFormatInfo numberFormat = (NumberFormatInfo)NumberFormatInfo.InvariantInfo.Clone();
    #endregion

    #region overrides
    public override void Init(Grammar grammar) {
      _quickParseTerminators = grammar.WhitespaceChars + grammar.Delimiters;
      base.Init(grammar);
    }

    //Most numbers in source programs are just one-digit instances of 0, 1, 2, and maybe others until 9
    // so we try to do a quick parse for these, without starting the whole general process
    protected override Token QuickParse(ISourceStream source) {
      char current = source.CurrentChar;
      if (char.IsDigit(current) && _quickParseTerminators.IndexOf(source.NextChar) >= 0) {
        int value = (int)(current - '0');
        Token token = new Token(this, source.TokenStart, current.ToString(), value);
        source.Position++;
        return token;
      } else 
        return null;
    }

    protected override void ReadPrefix(ISourceStream source, ScanDetails details) {
      //check that is not a  0 followed by dot; 
      //this may happen in Python for number "0.123" - we can mistakenly take "0" as octal prefix
      if (source.CurrentChar == '0' && source.NextChar == '.') return;
      base.ReadPrefix(source, details);
    }//method

    protected override void ReadSuffix(ISourceStream source, ScanDetails details) {
      base.ReadSuffix(source, details);
      if (string.IsNullOrEmpty(details.Suffix))
        details.TypeCode = details.IsSet(ScanFlags.HasDotOrExp) ? DefaultFloatType : DefaultIntType;
    }

    protected override bool ReadBody(ISourceStream source, ScanDetails details) {
      //remember start - it may be different from source.TokenStart, we may have skipped 
      int start = source.Position;  
      //Figure out digits set
      string digits = GetDigits(details);
      bool isDecimal = (digits == TextUtils.DecimalDigits);
      
      while (!source.EOF()) {
        char current = source.CurrentChar;
        //1. If it is a digit, just continue going
        if (digits.IndexOf(current) >= 0) {
          source.Position++;
          continue;
        }
        //2. Check if it is a dot
        if (current == DecimalSeparator) {
          //If we had seen already a dot or exponent, don't accept this one; also, we accept dot only if it is followed by a digit
          if (details.IsSet(ScanFlags.HasDotOrExp)  || (digits.IndexOf(source.NextChar) < 0))
            break; //from while loop
          details.Flags |= ScanFlags.HasDot;
          source.Position++;
          continue;
        }
        //3. Only for decimals - check if it is (the first) exponent symbol
        if ((isDecimal) && (details.ControlSymbol == null) && (ExponentSymbols.IndexOf(current) >= 0)) {
          char next = source.NextChar;
          bool nextIsSign = next == '+' || next == '-';
          bool nextIsDigit = digits.IndexOf(next) >= 0;
          if (!nextIsSign && !nextIsDigit)
            break;  //Exponent should be followed by either sign or digit
          //ok, we've got real exponent
          details.ControlSymbol = current.ToString(); //remember the exp char
          details.Flags |= ScanFlags.HasExp;
          source.Position++;
          if (nextIsSign)
            source.Position++; //skip +/- explicitly so we don't have to deal with them on the next iteration
          continue; 
        }
        //4. It is something else (not digit, not dot or exponent) - we're done
        break; //from while loop
      }//while
      int end = source.Position;
      details.Body = source.Text.Substring(start, end - start);
      return true;
    }

    protected override object ConvertValue(ScanDetails details) {
      if (ConvertingValue != null) {
				object value = null;
        value = OnConvertingValue(details);
        if (value != null || details.Error != null) return value; 
      }

			if (String.IsNullOrEmpty(details.Body))
				return null;

			//fraction (double, decimal...) expected
			if (details.IsSet(ScanFlags.HasDotOrExp))	{
				return ParseFraction(details);
			}	else {
				return ParseInteger(details);
			}
    }//method

    protected object OnConvertingValue(ScanDetails details) {
      if (ConvertingValue == null) return null; 
      ScannerConvertingValueEventArgs args = new ScannerConvertingValueEventArgs(details);
      ConvertingValue(this, args);
      return args.Value;
    }

    public override IList<string> GetFirsts() {
      StringList result = new StringList();
      result.AddRange(base.Prefixes);
      //we assume that prefix is always optional, so number can always start with plain digit
      result.AddRange(new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
      return result; 
    }
    #endregion
    
    #region private utilities
		private object ParseFraction(ScanDetails details) {
			//only decimal numbers can be fractions
			if (details.IsSet(ScanFlags.Octal) || details.IsSet(ScanFlags.Hex)) {
				details.Error = "Invalid number.";
				return null;
			}

			//Some languages allow exp symbols other than E. Check if it is the case, and change it to E
			// - otherwise .NET conversion methods may fail
			if (details.IsSet(ScanFlags.HasExp) && details.ControlSymbol.ToUpper() != "E")
				details.Body = details.Body.Replace(details.ControlSymbol, "E");

			try	{
				return Convert.ChangeType(details.Body, details.TypeCode, numberFormat);
			}	catch	{
				details.Error = "Invalid number.";
			}

			return null;
		}
		private object ParseInteger(ScanDetails details)
		{
			BigInteger integerValue = ParseBigInteger(details.Body, GetNumBase(details));

			try	{
				return Convert.ChangeType(integerValue, details.TypeCode);
			}	catch (OverflowException) {
				if (IsSet(TermOptions.NumberAllowBigInts))
					return integerValue;

				if (IsSet(TermOptions.NumberUseFloatOnIntOverflow))	{
					double floatValue;
					if (integerValue.TryToFloat64(out floatValue))
						return floatValue;
				}

				details.Error = "Invalid number - overflow.";
			}	catch	{
				details.Error = "Invalid number.";
			}

			return null;
		}
		private static BigInteger ParseBigInteger(string number, int radix){
			BigInteger value = BigInteger.Create(0);

			int numberLength = number.Length;
			for (int i = 0; i < numberLength; i++) {
				//number string verified by ReadBody(ISourceStream, ScanDetails)
				//NumberStyles.HexNumber can also be used for parsing decimal digits
				byte digitValue = Byte.Parse(number[i].ToString(), NumberStyles.HexNumber);

				value = value * radix + digitValue;
			}

			return value;
		}
    private int GetNumBase(ScanDetails details) {
      if (details.IsSet(ScanFlags.Hex))
        return 16; 
      if (details.IsSet(ScanFlags.Octal))
        return 8;
      return 10;
    }
    private string GetDigits(ScanDetails details) {
      if (details.IsSet(ScanFlags.Hex))
        return TextUtils.HexDigits;
      if (details.IsSet(ScanFlags.Octal))
        return TextUtils.OctalDigits;
      return TextUtils.DecimalDigits;
    }
    #endregion


  }//class


}
