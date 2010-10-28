using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptNET.ZNotation
{
  /// <summary>
  /// Base finite set
  /// </summary>
    public class FiniteSet : ModifiableSet, IEnumerable
    {
        #region Fields
        HashSet<object> items = new HashSet<object>();
        #endregion

        #region Constructor
        public FiniteSet()
        {

        }

        public FiniteSet(IEnumerable items)
        {
            this.items.UnionWith(items.OfType<object>());
        }
        #endregion

        #region ModifiableSet
        public override bool Contains(object item)
        {
            return items.Contains(item);
        }

        public override void Add(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            items.Add(item);
        }

        public override void Remove(object item)
        {
            items.Remove(item);
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (obj.Equals(StandardSets.Empty) && items.Count == 0) return true;
            FiniteSet fs = obj as FiniteSet;
            if (fs != null)
                return fs.items.SetEquals(items);

            return false;
        }

        public override string ToString()
        {
            if (items.Count == 0) return "{}";

            return "{" + items.Aggregate<object, string>("", (s, o) => s + " " + o.ToString()) + " }";
        }

        public Set Intersect(FiniteSet a)
        {
            return new FiniteSet(this.items.Intersect(a.items));
        }
        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            foreach (object item in items)
                yield return item;
        }

        #endregion
    }
}
