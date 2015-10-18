using System;
using System.Collections.Generic;

namespace KuroganeHammer.Data.Core.D
{
    public class Sm4shDB : IDisposable
    {
        private DBHelper dbHelper;

        public Sm4shDB()
        {
            dbHelper = new DBHelper();
        }

        /// <summary>
        /// Use when updating existing data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stats"></param>
        internal void Save<T>(List<T> stats)
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
        internal void Save<T>(T stat)
        {
            dbHelper.ExecuteNonQuery<T>(stat, DBVerb.Update);
        }

        /// <summary>
        /// Use when creating data for the first time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stats"></param>
        internal void SaveAs<T>(List<T> stats)
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
        internal void SaveAs<T>(T stat)
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
