using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    public static class Sets
    {
        public static Set Union(Set a, Set b)
        {
            return new CompositeSet(a, b, (s1, s2, item) => s1.Contains(item) || s2.Contains(item));
        }

        public static Set Intersection(Set a, Set b)
        {
            return new CompositeSet(a, b, (s1, s2, item) => s1.Contains(item) && s2.Contains(item));
        }

        public static Set Intersection(FiniteSet a, FiniteSet b)
        {
            return a.Intersect(b);
        }

        public static Set Subtraction(Set a, Set b)
        {
            return new CompositeSet(a, b, (s1, s2, item) => s1.Contains(item) && !s2.Contains(item));
        }
    }
}
