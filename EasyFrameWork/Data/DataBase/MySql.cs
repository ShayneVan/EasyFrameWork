﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Easy.Data.DataBase
{
    class MySql : DataBasic
    {
        public MySql()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }

        public MySql(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new MySqlDataAdapter(command as MySqlCommand);
        }

        protected override DbConnection GetDbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        protected override DbCommand GetDbCommand()
        {
            return new MySqlCommand();
        }

        protected override DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter)
        {
            return new SqlCommandBuilder(adapter as SqlDataAdapter);
        }

        protected override DbParameter GetDbParameter(string key, object value)
        {
            return new MySqlParameter(key, value);
        }

        public override bool IsExistTable(string tableName)
        {
            return CustomerSql("SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tableName")
                 .AddParameter("@tableName", tableName)
                 .To<int>() != 0;
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            return CustomerSql("SELECT COUNT(*) FROM INFORMATION_SCHEMA.[COLUMNS]  WHERE TABLE_NAME=@tableName AND COLUMN_NAME=@columnName")
                .AddParameter("@tableName", tableName)
                .AddParameter("@columnName", columnName)
                .To<int>() != 0;
        }
    }
}
