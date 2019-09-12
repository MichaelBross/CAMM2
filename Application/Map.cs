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
                            Map.AtoB(sourceProp, p);
                        }
                        else
                        {
                            p.SetValue(B, sourceProp.GetValue(A));
                        }                        
                    }
                }

            }

        }
    }
}
