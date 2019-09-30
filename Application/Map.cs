using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class Map
    {
        public static void AtoB<T, TU>(this T A, TU B)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { 
                        if(p.PropertyType.ToString().Contains("List"))
                        {
                            //foreach(var sourceItem in (IList<dynamic>)sourceProp.GetValue(A))
                            //{
                            //    Map.AtoB(sourceItem, p);
                            //}                            
                        }
                        else
                        {
                            p.SetValue(B, sourceProp.GetValue(A));
                        }                        
                    }
                }
            }
        }

        public static void ListToList<T, TU>(this List<T> ListA, List<TU> ListB)
        {
            TU b = (TU)Activator.CreateInstance(typeof(TU));

            foreach(var a in ListA)
            {
                Map.AtoB(a, b);
                ListB.Add(b);
            }
        }

        public static void IEnumableToIEnumable<T, TU>(this IEnumerable<T> ListA, IEnumerable<TU> ListB)
        {
            TU b = (TU)Activator.CreateInstance(typeof(TU));
            var list = new List<TU>();
            foreach (var a in ListA)
            {
                Map.AtoB(a, b);
                list.Add(b);
            }
            ListB = (IEnumerable<TU>)list;
        }
    }
}
