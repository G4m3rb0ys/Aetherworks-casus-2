using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.Data
{
    public class LocalDbService
    {
        private readonly SQLiteConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteConnection(
                DataConstanst.DatabasePath,
                DataConstanst.flags
            );
            //_connection.CreateTable<User>();
        }
    }
}
