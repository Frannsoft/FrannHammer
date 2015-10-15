using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace KuroganeHammer.Data.Core.D
{
    public class Sm4shDB : IDisposable
    {
        private MySqlConnection conn;

        public Sm4shDB()
        { }

        //TODO: SELECT method

        #region private
        private MySqlConnection Open()
        {
            string cs = ConfigurationManager.AppSettings["connstring"];
            conn = new MySqlConnection(cs);
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
