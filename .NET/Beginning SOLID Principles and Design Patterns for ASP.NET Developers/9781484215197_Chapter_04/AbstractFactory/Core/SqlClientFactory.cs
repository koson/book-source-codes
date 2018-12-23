using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AbstractFactory.Core
{
    public class SqlClientFactory:IDatabaseFactory
    {
        public DbConnection GetConnection()
        {
            return new SqlConnection();
        }

        public DbCommand GetCommand()
        {
            return new SqlCommand();
        }

        public DbParameter GetParameter()
        {
            return new SqlParameter();
        }

    }
}
