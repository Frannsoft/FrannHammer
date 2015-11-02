using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core.Model.Stats.dbentity;
using System;
using System.Linq;
using System.Collections.Generic;
using MySqlConnectorWrapper;

namespace KuroganeHammer.Data.Core.D
{
    public class Sm4shDB : IDisposable
    {
        private class StatContainer
        {
            internal List<MovementStatDB> MovementStats;
            internal List<GroundStatDB> GroundStats;
            internal List<AerialStatDB> AerialStats;
            internal List<SpecialStatDB> SpecialStats;
            internal CharacterDB CharacterData;

            internal StatContainer(Character character)
            {
                MovementStats = ConvertStats<MovementStat, MovementStatDB>(character.FrameData);
                GroundStats = ConvertStats<GroundStat, GroundStatDB>(character.FrameData);
                AerialStats = ConvertStats<AerialStat, AerialStatDB>(character.FrameData);
                SpecialStats = ConvertStats<SpecialStat, SpecialStatDB>(character.FrameData);
                CharacterData = new CharacterDB(character.Name, character.OwnerId, character.FullUrl);
            }

            private List<TOut> ConvertStats<Tin, TOut>(Dictionary<string, Stat> items)
          where Tin : Stat
          where TOut : StatDB
            {
                var convertedItemsList = (from item in items.Values
                                          where item.GetType() == typeof(Tin)
                                          select EntityBusinessConverter<Tin>.ConvertTo<TOut>((Tin)item))
                           .ToList();

                return convertedItemsList;
            }
        }
        private DBHelper dbHelper;

        private const string k = "Database=Sm4sh;Data Source=us-cdbr-azure-east-a.cloudapp.net;User Id=b95624cb341584;Password=a3e4a4a7";
        public Sm4shDB()
        {
            dbHelper = new DBHelper(k);
        }

        public void Save(Character character)
        {
            StatContainer container = new StatContainer(character);

            Save(container.MovementStats);
            Save(container.GroundStats);
            Save(container.AerialStats);
            Save(container.SpecialStats);
            Save(container.CharacterData);
        }

        public void SaveAs(Character character)
        {
            StatContainer container = new StatContainer(character);
            SaveAs(container.MovementStats);
            SaveAs(container.GroundStats);
            SaveAs(container.AerialStats);
            SaveAs(container.SpecialStats);
            Save(container.CharacterData);
        }

        /// <summary>
        /// Use when updating existing data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stats"></param>
        private void Save<T>(List<T> stats)
        {
            foreach (var stat in stats)
            {
                dbHelper.ExecuteNonQuery<T>(stat, DBVerb.Update);
            }
        }

        /// <summary>
        /// Use when updating existing data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stat"></param>
        private void Save<T>(T stat)
        {
            dbHelper.ExecuteNonQuery<T>(stat, DBVerb.Update);
        }

        /// <summary>
        /// Use when creating data for the first time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stats"></param>
        private void SaveAs<T>(List<T> stats)
        {
            foreach (var stat in stats)
            {
                dbHelper.ExecuteNonQuery<T>(stat, DBVerb.Create);
            }
        }

        /// <summary>
        /// Use when creating data for the first time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stat"></param>
        private void SaveAs<T>(T stat)
        {
            dbHelper.ExecuteNonQuery<T>(stat, DBVerb.Create);
        }

        #region IDisposable
        public void Dispose()
        {
            if (dbHelper != null)
            {
                dbHelper.Dispose();
            }
        }
        #endregion
    }
}
