# ScriptDotNet distribution for CliMOO

This repo represents a forked copy of an old (2010) version of the Script.NET/S# scripting library from [Petro Protsyk](http://www.protsyk.com/scriptdotnet). This version, and its embedded IronyLibrary, were (and are) licensed under the MIT license.

#### Some changes of note since the fork:
* Various features for reusing scripts in very lightweight containers
* Loosening of language strictness (e.g. Javascript-style if(foo) type coercion)
* Allow identifiers to start with $ and #, for MOO objects
* Some extra support for whitelisting available types
* Mono fixes
* Removal of BigInteger and Complex support so that the MSPL does not apply.

Please check the Git history for full details.

Note that because the version I forked was not on GitHub (or at least I didn't realize it, if it was), this is not a proper Git fork.
