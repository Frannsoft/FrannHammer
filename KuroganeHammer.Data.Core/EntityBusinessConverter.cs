using System;
using System.Linq;
using System.Reflection;

namespace KuroganeHammer.Data.Core
{
    public static class EntityBusinessConverter<TInitial>
    {
        private static BindingFlags _flags = BindingFlags.FlattenHierarchy |
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic
                        | BindingFlags.Public;

        public static TResult ConvertTo<TResult>(TInitial initialType)
        {
            var tResult = (TResult)Activator.CreateInstance(typeof(TResult), true);
            var resultTypeProps = typeof(TResult).GetProperties(_flags);

            foreach (var prop in resultTypeProps)
            {
                if (PropExistsInType(prop))
                {
                    //holds value from initial type.  We need to create a new propertyinfo because of case sensitive property names not being found
                    //when not matching (obviously).
                    var tempProp = typeof(TInitial).GetProperty(prop.Name, _flags);

                    var initialObjectValue = tempProp.GetValue(initialType);
                    prop.SetValue(tResult, initialObjectValue);
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
            var props = typeof(TInitial).GetProperties(_flags);
            return props.Any(p => p.Name.ToLower().Equals(prop.Name.ToLower()));
        }
    }
}
