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
        public readonly SQLiteAsyncConnection _connection;
        public string? statusMessage;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(
                DataConstants.DatabasePath,
                DataConstants.flags
            );
            if (!File.Exists(DataConstants.DatabasePath))
            {
                InitializeDatabase();
                statusMessage = "Database created";
            }

        }
        public async void InitializeDatabase()
        {
            await _connection.CreateTableAsync<User>();
            await _connection.CreateTableAsync<Participation>();
            await _connection.CreateTableAsync<Penalty>();
            await _connection.CreateTableAsync<Suggestion>();
            await _connection.CreateTableAsync<SuggestionLiked>();
            await _connection.CreateTableAsync<VictuzActivity>();
            await _connection.CreateTableAsync<VictuzLocation>();
            statusMessage = "Tables Initialized";
            GenerateData();
        }

        public async void GenerateData()
        {
            var user1 = await AddOrUpdateUser(new User 
            { 
                IsAdmin = true, 
                Email = "ravismeets@gmail.com", 
                CapitalizedEmail = "RAVISMEETS@GMAIL.COM", 
                Name = "Ravi", 
                Password = "123", 
                PhoneNumber = "0639833440", 
                Username = "SpaceBaker", 
                CapitalizedUsername = "SPACEBAKER" });
            var user2 = await AddOrUpdateUser(new User 
            { 
                IsAdmin = true, 
                Email = "stanbormans@gmail.com", 
                CapitalizedEmail = "STANBORMANS@GMAIL.COM", 
                Name = "Stan", 
                Password = "123", 
                PhoneNumber = "0645678945", 
                Username = "LostMeta", 
                CapitalizedUsername = "LOSTMETA" });
            var user3 = await AddOrUpdateUser(new User 
            { 
                IsAdmin = true, 
                Email = "timtigelaar@gmail.com", 
                CapitalizedEmail = "TIMTIGELAAR@GMAIL.COM", 
                Name = "Tim", 
                Password = "123", 
                PhoneNumber = "0615988542", 
                Username = "Maverick", 
                CapitalizedUsername = "MAVERICK" });
            var user4 = await AddOrUpdateUser(new User
            {
                IsAdmin = false,
                Email = "johndoe@gmail.com",
                CapitalizedEmail = "JOHNDOE@GMAIL.COM",
                Name = "John",
                Password = "123",
                PhoneNumber = "0612345678",
                Username = "John",
                CapitalizedUsername = "JOHN"
            });
            var user5 = await AddOrUpdateUser(new User
            {
                IsAdmin = false,
                Email = "janedoe@gmail.com",
                CapitalizedEmail = "JANEDOE@GMAIL.COM",
                Name = "Jane",
                Password = "123",
                PhoneNumber = "0698765432",
                Username = "Jane",
                CapitalizedUsername = "JANE"
            });
            var user6 = await AddOrUpdateUser(new User
            {
                IsAdmin = false,
                Email = "sam.smith@gmail.com",
                CapitalizedEmail = "SAM.SMITH@GMAIL.COM",
                Name = "Sam",
                Password = "123",
                PhoneNumber = "0654321876",
                Username = "Sam",
                CapitalizedUsername = "SAM"
            });

            statusMessage = "Standard Data Generated";
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                return await _connection.Table<User>().FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception e) 
            {
                statusMessage = $"Error: {e.Message}";
            }
            return null;
        }
        public async Task<User> GetUser(string userNameOrEmail)
        {
            try
            {
                return await _connection.Table<User>().FirstOrDefaultAsync(u => u.CapitalizedUsername == userNameOrEmail.ToUpper() || u.CapitalizedEmail == userNameOrEmail.ToUpper());
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
            return null;
        }
        public async Task<User> AddOrUpdateUser(User newUser)
        {
            int result = 0;
            try
            {
                if (newUser.Id != 0)
                {
                    result = await _connection.UpdateAsync(newUser);
                    statusMessage = $"{result} row(s) updated :)";
                    return newUser;
                }
                else
                {
                    result = await _connection.InsertAsync(newUser);
                    statusMessage = $"{result} row(s) added :)";
                    return newUser;
                }
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return null;
            }
        }
    }
}
