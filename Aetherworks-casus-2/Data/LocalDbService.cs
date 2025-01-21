using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;

namespace Aetherworks_casus_2.Data
{
    public class LocalDbService
    {
        public readonly SQLiteConnection _connection;
        public string? statusMessage;

        public LocalDbService()
        {
            _connection = new SQLiteConnection(
                DataConstanst.DatabasePath,
                DataConstanst.flags
            );
            
            _connection.CreateTable<User>();
            _connection.CreateTable<Participation>();
            _connection.CreateTable<Penalty>();
            _connection.CreateTable<Suggestion>();
            _connection.CreateTable<SuggestionLiked>();
            _connection.CreateTable<VictuzActivity>();
            _connection.CreateTable<VictuzLocation>();
        }
        public async Task<User> GetUser(int id)
        {
            try
            {
                return _connection.Table<User>().FirstOrDefault(t => t.Id == id);
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
            return null;
        }
    }
}
