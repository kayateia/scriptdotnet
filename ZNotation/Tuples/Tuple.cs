using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptNET.ZNotation
{
    public class Tuple
    {
        #region Fields
        int cardinality;
        List<object> items;
        #endregion

        #region constructors
        public Tuple():this(2)
        {
        }

        public Tuple(int cardinality)
        {
            Initialize(cardinality); 
        }

        private void Initialize(int cardinality)
        {
            this.cardinality = cardinality;
            items = new List<object>(cardinality);
            for (int i = 0; i < cardinality; i++)
                items.Add(Constants.Undefined);
        }
        #endregion

        #region Properties
        public object this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }

        public int Cardinality
        {
            get
            {
                return cardinality;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return "(" + items.Aggregate((a, b) => a.ToString() + "," + b.ToString()) + ")";
        }

        public override bool Equals(object obj)
        {
            Tuple tuple = obj as Tuple;
            if (tuple == null) return false;
            if (tuple.Cardinality != Cardinality) return false;

            for (int i = 0; i < Cardinality; i++)
                if ((tuple[i] == null && this[i] != null) ||
                    (tuple[i] != null && !tuple[i].Equals(this[i]))) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < Cardinality; i++)
                if (this[i] != null)
                    result ^= this[i].GetHashCode();

            return result;
        }

        public IEnumerator GetEnumerator()
        {
          return (IEnumerator)items.GetEnumerator();
        }
        #endregion
    }

}
