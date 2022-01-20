using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Connection
{
    public class Connectionstrings
    {
        private SqlConnectionStringBuilder builder;

        private static Connectionstrings _intance;
        protected Connectionstrings()
        {
            builder = new SqlConnectionStringBuilder();
        }

        public static Connectionstrings Intances()
        {
            if (_intance == null)
                _intance = new Connectionstrings();


            //  _intance = _intance ?? new DataSource();

            return _intance;
        }
        public string GetDataSourceSever()
        {
            builder.DataSource = @"DESKTOP-B18MHUK\SQLEXPRESS";

            builder.InitialCatalog = "Running";

            builder.IntegratedSecurity = true;

            return builder.ConnectionString;
        }
    }
}
