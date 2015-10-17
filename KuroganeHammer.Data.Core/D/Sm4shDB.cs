using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats.dbentity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace KuroganeHammer.Data.Core.D
{
    public class Sm4shDB : IDisposable
    {
        private MySqlConnection conn;

        public Sm4shDB()
        {
            conn = Open();
        }

        internal void Save<T>(List<T> stats)
        {
            foreach (var stat in stats)
            {
                MySqlCommand command = GetCommand<T>(stat);
                command.ExecuteNonQuery();
            }
        }

        internal void Save<T>(T stat)
        {
            MySqlCommand command = GetCommand<T>(stat);
            command.ExecuteNonQuery();
        }

        private MySqlCommand GetCommand<T>(T stat)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;

            var statProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance
                | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            MySqlParameter parameter = default(MySqlParameter);

            StringBuilder sb = new StringBuilder();

            string table = typeof(T).GetCustomAttribute<TableId>().Value;
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentException("Unable to retrieve table type");
            }

            sb.Append("INSERT INTO " + table + " (");

            foreach (var prop in statProperties)
            {
                sb.Append(prop.Name + ",");
            }
            sb.Remove(sb.Length - 1, 1);

            sb.Append(") VALUES (");

            foreach (var prop in statProperties)
            {
                sb.Append("?" + prop.Name + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            command.CommandText = sb.ToString();

            foreach (var prop in statProperties)
            {
                parameter = new MySqlParameter();
                parameter.ParameterName = "@" + prop.Name;
                parameter.Value = prop.GetValue(stat);
                command.Parameters.Add(parameter);
            }
            return command;
        }

        #region private
        private MySqlConnection Open()
        {
            string cs = ConfigurationManager.AppSettings["connstring"];
            conn = new MySqlConnection(cs);
            conn.Open();
            return conn;
        }

        private void Close()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        #endregion
    }
}
