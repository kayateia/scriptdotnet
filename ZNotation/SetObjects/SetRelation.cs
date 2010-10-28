using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    /// <summary>
    /// Relation
    /// </summary>
    public class SetRelation : Relation
    {
        #region Fields
        protected HashSet<Tuple> items;
        #endregion

        #region Constructors
        public SetRelation()
            : this(2)
        {

        }

        public SetRelation(int cardinality)
            : base(cardinality)
        {
            Initialize();
        }

        private void Initialize()
        {
            items = new HashSet<Tuple>();
        }
        #endregion

        #region Methods
        public void Add(Tuple tuple)
        {
            if (tuple.Cardinality != Cardinality) 
                throw new WrongCardinalityException();
            items.Add(tuple);
        }

        public override bool Exists(Tuple tuple)
        {
            return items.Contains(tuple);
        }
        #endregion
    }
}
