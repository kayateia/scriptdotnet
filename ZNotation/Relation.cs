using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    /// <summary>
    /// Relation
    /// </summary>
    public abstract class Relation
    {
        #region Fields
        int cardinality;
        #endregion

        #region Properties
        public int Cardinality
        {
            get
            {
                return cardinality;
            }
        }
        #endregion

        #region Constructors
        public Relation()
            : this(2)
        {

        }

        public Relation(int cardinality)
        {
            this.cardinality = cardinality;
        }
        #endregion

        #region Methods
        public abstract bool Exists(Tuple tuple);
        #endregion
    }
   
}
