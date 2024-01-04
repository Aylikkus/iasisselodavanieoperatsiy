using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public enum Sign
    {
        Equal,
        GreaterThan,
        LessThan,
        GreaterThanEqual,
        LessThanEqual
    }

    public static class SignConverter
    {
        private static readonly Dictionary<Sign, string> signToString;
        private static readonly Dictionary<string, Sign> stringToSign;

        static SignConverter()
        {
            signToString = new Dictionary<Sign, string>(5)
            {
                { Sign.Equal, "=" },
                { Sign.GreaterThan, ">" },
                { Sign.LessThan, "<" },
                { Sign.GreaterThanEqual, ">=" },
                { Sign.LessThanEqual, "<=" },
            };
            stringToSign = signToString.ToDictionary(x => x.Value, x => x.Key);
        }

        public static string GetString(Sign sign)
        {
            return signToString[sign];
        }

        public static Sign GetSign(string sign)
        {
            return stringToSign[sign];
        }

        public static bool IsValidString(string sign)
        {
            return stringToSign.ContainsKey(sign);
        }
    }
}
