using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace MySqlConnectorWrapper
{
    public sealed class DBHelper : IDisposable
    {
        private MySqlConnection conn;

        public DBHelper(string connectionString)
        {
            Open(connectionString);
        }

        #region query logic
        public void ExecuteNonQuery<T>(T stat, DBVerb verb)
        {
            MySqlCommand command = default(MySqlCommand);

            string commandText = string.Empty;

            switch (verb)
            {
                case DBVerb.Create:
                    {
                        command = GetInsertCommand<T>(stat);
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        break;
                    }
                case DBVerb.Update:
                    {
                        command = GetUpdateCommand<T>(stat);
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        break;
                    }
                case DBVerb.Delete:
                    {
                        command = GetDeleteCommand<T>(stat);
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        break;
                    }
            }
        }

        public MySqlDataReader ExecuteReader<T>(T stat)
        {
            MySqlCommand command = default(MySqlCommand);
            string commandText = string.Empty;

            command = GetReadCommand<T>(stat);
            command.Connection = conn;
            return command.ExecuteReader();
        }
        #endregion

        #region query metadata
        private string GetTable<T>()
        {
            string table = typeof(T).GetCustomAttribute<TableId>().Value;
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentException("Unable to retrieve table type");
            }
            return table;
        }

        private List<MySqlParameter> GetCommandParameters<T>(MySqlCommand command, PropertyInfo[] statProperties, T stat)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var prop in statProperties)
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@" + prop.Name;
                parameter.Value = prop.GetValue(stat);
                parameters.Add(parameter);
            }
            return parameters;
        }

        private PropertyInfo[] GetProperties<T>()
        {
            var statProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance
               | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            return statProperties;
        }
        #endregion

        #region commands
        private MySqlCommand GetUpdateCommand<T>(T stat)
        {
            MySqlCommand command = new MySqlCommand();
            var properties = GetProperties<T>();
            command.CommandText = CreateUpdateCommandText(GetTable<T>(), properties);
            command.Parameters.AddRange(GetCommandParameters<T>(command, properties, stat).ToArray());
            return command;
        }

        private MySqlCommand GetInsertCommand<T>(T stat)
        {
            MySqlCommand command = new MySqlCommand();
            var properties = GetProperties<T>();
            command.CommandText = CreateInsertCommandText(GetTable<T>(), properties);
            command.Parameters.AddRange(GetCommandParameters<T>(command, properties, stat).ToArray());
            return command;
        }

        private MySqlCommand GetDeleteCommand<T>(T stat)
        {
            MySqlCommand command = new MySqlCommand();
            var properties = GetProperties<T>();
            command.CommandText = CreateDeleteCommandText(GetTable<T>(), properties);
            command.Parameters.AddRange(GetCommandParameters<T>(command, properties, stat).ToArray());
            return command;
        }

        private MySqlCommand GetReadCommand<T>(T stat)
        {
            MySqlCommand command = new MySqlCommand();
            var properties = GetProperties<T>();
            command.CommandText = CreateReadCommandText(GetTable<T>(), properties);
            command.Parameters.AddRange(GetCommandParameters<T>(command, properties, stat).ToArray());
            return command;
        }
        #endregion

        #region command text
        private string CreateInsertCommandText(string table, PropertyInfo[] statProperties)
        {
            StringBuilder sb = new StringBuilder();
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

            return sb.ToString();
        }

        private string CreateUpdateCommandText(string table, PropertyInfo[] statProperties)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + table + " SET ");

            foreach (var prop in statProperties)
            {
                sb.Append(prop.Name + " = ?" + prop.Name + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private string CreateDeleteCommandText(string table, PropertyInfo[] statProperties)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM " + table + " WHERE ");

            foreach (var prop in statProperties)
            {
                sb.Append(prop.Name + " = ?" + prop.Name + " AND ");
            }
            sb.Remove(sb.Length - 1, 5);
            return sb.ToString();
        }

        private string CreateReadCommandText(string table, PropertyInfo[] statProperties)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");

            foreach (var prop in statProperties)
            {
                sb.Append(prop.Name + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" FROM " + table + " WHERE ");

            foreach (var prop in statProperties)
            {
                sb.Append(prop.Name + " = ?" + prop.Name + " AND ");
            }
            sb.Remove(sb.Length - 1, 5);
            return sb.ToString();
        }
        #endregion

        #region db ops
        private MySqlConnection Open(string cs)
        {
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

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
