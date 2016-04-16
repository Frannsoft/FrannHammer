using System;

namespace KuroganeHammer.Data.Core
{
    public static class Guard
    {
        public static void VerifyObjectNotNull(object objToVerify, string argName)
        {
            if (objToVerify == null)
            { throw new ArgumentNullException($"Object {argName} is null."); }
        }

        public static void VerifyStringIsNotNullOrEmpty(string stringToVerify, string argName)
        {
            if (string.IsNullOrEmpty(stringToVerify))
            { throw new ArgumentNullException($"String {argName} is null or empty."); }
        }
    }
}
