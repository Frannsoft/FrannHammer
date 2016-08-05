using FrannHammer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FrannHammer.Core.Tests
{
    [TestFixture]
    public class DtoBuilderTests
    {
        private void AssertObjectBuiltAsExpected<TEntity, TDto>(TEntity entity, string rawFields)
        {
            var builder = new DtoBuilder();

            string[] fieldsArr = rawFields.Split(',');
            string splitFields = string.Join(",", fieldsArr);

            var result = builder.Build<TEntity, TDto>(entity, splitFields);

            var exValues = result as IDictionary<string, object>;

            Debug.Assert(exValues != null, "exValues != null");

            CollectionAssert.AllItemsAreNotNull(exValues);
            CollectionAssert.AllItemsAreUnique(exValues);
            CollectionAssert.IsNotEmpty(exValues);

            var entityValuesThatExist = (from value in entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                         join name in fieldsArr
                                             on value.Name.ToLower() equals name
                                         select value).ToList();

            var entityValuesThatDoNotExistOnEo = (from value in entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                  where !fieldsArr.Any(f => f.Equals(value.Name.ToLower()))
                                                  select value).ToList();

            CollectionAssert.AllItemsAreNotNull(entityValuesThatExist);
            CollectionAssert.AllItemsAreUnique(entityValuesThatExist);
            CollectionAssert.IsNotEmpty(entityValuesThatExist);
            Assert.That(entityValuesThatExist.Count, Is.EqualTo(fieldsArr.Length));

            //assert that the specified field values are present and equal to the entity object's values
            foreach (var val in entityValuesThatExist)
            {
                Assert.That(exValues[val.Name], Is.EqualTo(val.GetValue(entity)));
            }

            //assert that all fields not specified in the fields array are not present on the dynamic object
            foreach (var val in entityValuesThatDoNotExistOnEo)
            {
                Assert.That(!exValues.ContainsKey(val.Name));
            }
        }

        [Test]
        [TestCase("name")]
        [TestCase("style,name")]
        [TestCase("id,style,description,colortheme")]
        public void ShouldBuildObjectWithSpecificProps(string rawFields)
        {
            var entity = new Character
            {
                Id = 1,
                ColorTheme = string.Empty,
                Description = "desc",
                DisplayName = string.Empty,
                LastModified = DateTime.Now,
                MainImageUrl = "url",
                Name = "test",
                Style = "thestyle",
                ThumbnailUrl = "thumburl"
            };

            AssertObjectBuiltAsExpected<Character, CharacterDto>(entity, rawFields);
        }
    }
}
