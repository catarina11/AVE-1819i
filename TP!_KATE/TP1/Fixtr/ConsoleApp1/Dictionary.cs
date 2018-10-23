using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Dictionary
    {
        private static Dictionary<Type, IFixture> CacheMapp = new Dictionary<Type, IFixture>();

        public static IFixture GetFixture(Type t)
        {
            IFixture fix;
            if (CacheMapp.TryGetValue(t, out fix))
                return fix;
            else
            {
                fix = new FixtureReflect(t);
                if (CacheMapp.TryGetValue(t, out fix))
                    return fix;
                
                CacheMapp.Add(t, fix);
                return fix;
            }
            
        }

        public static void Add(Type t, IFixture fix)
        {
            if(!CacheMapp.ContainsKey(t))
                CacheMapp.Add(t, fix);
        }

        private static Dictionary<Type, PrimitiveFixture> cachePrimitive = new Dictionary<Type, PrimitiveFixture>();
        private static Dictionary<Type, StringFixture> cacheString = new Dictionary<Type, StringFixture>();
        private static Dictionary<Type, ArrayFixture> cacheArrays = new Dictionary<Type, ArrayFixture>();
        private static Dictionary<Type, ComplexFixture> cacheComplex = new Dictionary<Type, ComplexFixture>();

        public static PrimitiveFixture GetPrimitiveGenerator(Type t)
        {
            PrimitiveFixture primitive;
            if (cachePrimitive.TryGetValue(t, out primitive))
                return primitive;

            primitive = new PrimitiveFixture(t);
            cachePrimitive.Add(t, primitive);

            return primitive;
        }

        public static StringFixture GetStringGenerator(Type t)
        {
            StringFixture strings;
            if (cacheString.TryGetValue(t, out strings))
                return strings;

            strings = new StringFixture(t);
            cacheString.Add(t, strings);

            return strings;
        }

        public static ComplexFixture GetComplexGenerator(Type t)
        {
            ComplexFixture complex;
            if (cacheComplex.TryGetValue(t, out complex))
                return complex;

            complex = new ComplexFixture(t);
            cacheComplex.Add(t, complex);

            return complex;
        }

        public static ArrayFixture GetArrayGenerator(Type t)
        {
            ArrayFixture arrays;
            if (cacheArrays.TryGetValue(t, out arrays))
                return arrays;

            arrays = new ArrayFixture(t);
            cacheArrays.Add(t, arrays);

            return arrays;
        }
    }
}
