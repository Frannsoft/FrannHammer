using System.Collections.Generic;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.Harnesses
{
    internal class HarnessAsserts
    {
        internal static void ExpandoObjectIsCorrect(IEnumerable<dynamic> items, string fields)
        {
            var splitFields = fields.Split(',');

            foreach (var item in items)
            {
                AssertExpandoObjectIsCorrectCore(item, splitFields);
            }
        }

        internal static void ExpandoObjectIsCorrect(dynamic item, string fields)
        {
            var splitFields = fields.Split(',');
            AssertExpandoObjectIsCorrectCore(item, splitFields);
        }

        private static void AssertExpandoObjectIsCorrectCore(dynamic item, string[] fields)
        {
            var itemDict = (IDictionary<string, object>)item;

            foreach (var field in fields)
            {
                Assert.That(itemDict.ContainsKey(field));
                Assert.That(itemDict[field], Is.Not.Null);
            }
        }
    }
}
