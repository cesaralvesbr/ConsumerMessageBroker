using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsumerMessageBroker.INFRA
{
    public class MSSQLDB : IDB
    {
        public IDbConnection GetDb()
        {
            return new SqlConnection(@"Data Source=DESKTOP-FVILUGB\SQLEXPRESS; Database=ProjMessageBrokerRabbitMQ;User Id=sa; Password=cesar123");
        }
    }
}
