using System.Linq;
using System.Reflection;

namespace KurograneHammer.TransferDBTool
{
    class PropertyUpdateHelper
    {
        /// <summary>
        /// Updates the retVal object passed in.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="source"></param>
        /// <param name="retVal"></param>
        /// <returns></returns>
        public static void UpdateProperty<TSource, TRet>(TSource source, TRet retVal)
        {
            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var retValProps = retVal.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in retValProps)
            {
                var foundSourceProp = sourceProps.FirstOrDefault(p => p.Name == prop.Name);

                if (foundSourceProp != null) //values are different
                {
                    var sourcePropVal = foundSourceProp.GetValue(source);
                    var retPropVal = prop.GetValue(retVal);
                    if (sourcePropVal != null &&
                        !sourcePropVal.Equals(retPropVal))
                    {
                        //update this property on retVal
                        prop.SetValue(retVal, sourcePropVal);
                    }
                }
            }
        }
    }
}
