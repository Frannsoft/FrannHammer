using System;
using System.Linq;
using System.Reflection;

namespace KuroganeHammer.Data.Core.D
{
    public static class EntityBusinessConverter<TInitial>
    {
        private static BindingFlags flags = BindingFlags.FlattenHierarchy |
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic
                        | BindingFlags.Public;

        public static TResult ConvertTo<TResult>(TInitial initialType)
        {
            TResult tResult = (TResult)Activator.CreateInstance(typeof(TResult), true);
            var resultTypeProps = typeof(TResult).GetProperties(flags);

            foreach (PropertyInfo prop in resultTypeProps)
            {
                if (PropExistsInType(prop))
                {
                    var propr = typeof(TInitial).GetProperties(flags);

                    //holds value from initial type.  We need to create a new propertyinfo because of case sensitive property names not being found
                    //when not matching (obviously).
                    PropertyInfo tempProp = typeof(TInitial).GetProperty(prop.Name, flags);

                    object initialObjectValue = tempProp.GetValue(initialType);
                    if (initialObjectValue != null)
                    {
                        prop.SetValue(tResult, initialObjectValue);
                    }
                    else
                    {
                        prop.SetValue(tResult, null);
                    }
                }
            }

            return tResult;
        }

        /// <summary>
        /// Checks to see if the passed in property exists on the initial Type.  Returns true if it does.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static bool PropExistsInType(PropertyInfo prop)
        {
            //converting everything to lower case prior to comparison since that 
            //seems to be the standard naming convention for this db.
            var props = typeof(TInitial).GetProperties(flags);
            return props.Any(p => p.Name.ToLower().Equals(prop.Name.ToLower()));
        }
    }
}
