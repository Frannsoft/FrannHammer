using System;
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

            var caseInsensitiveDictionary = new Dictionary<string, object>(itemDict, StringComparer.OrdinalIgnoreCase);
            foreach (var field in fields)
            {
                Assert.That(caseInsensitiveDictionary.ContainsKey(field.ToLower()));
                Assert.That(caseInsensitiveDictionary[field], Is.Not.Null);
            }
        }
    }
}
