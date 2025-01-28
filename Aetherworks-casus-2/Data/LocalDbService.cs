using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;
using System.Diagnostics;

namespace Aetherworks_casus_2.Data
{
    public class LocalDbService
    {
        public readonly SQLiteAsyncConnection _connection;
        public string? StatusMessage { get; private set; }

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(
                DataConstants.DatabasePath,
                DataConstants.flags
            );

            if (!File.Exists(DataConstants.DatabasePath))
            {
                InitializeDatabaseAsync();
                StatusMessage = "Database created.";
            }
        }

        private async Task InitializeDatabaseAsync()
        {
            await _connection.CreateTableAsync<User>();
            await _connection.CreateTableAsync<Participation>();
            await _connection.CreateTableAsync<Penalty>();
            await _connection.CreateTableAsync<Suggestion>();
            await _connection.CreateTableAsync<SuggestionLiked>();
            await _connection.CreateTableAsync<VictuzActivity>();
            await _connection.CreateTableAsync<VictuzLocation>();

            StatusMessage = "Tables Initialized.";

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
                CapitalizedUsername = "SPACEBAKER"
            });
            var user2 = await AddOrUpdateUser(new User
            {
                IsAdmin = true,
                Email = "stanbormans@gmail.com",
                CapitalizedEmail = "STANBORMANS@GMAIL.COM",
                Name = "Stan",
                Password = "123",
                PhoneNumber = "0645678945",
                Username = "LostMeta",
                CapitalizedUsername = "LOSTMETA"
            });
            var user3 = await AddOrUpdateUser(new User
            {
                IsAdmin = true,
                Email = "timtigelaar@gmail.com",
                CapitalizedEmail = "TIMTIGELAAR@GMAIL.COM",
                Name = "Tim",
                Password = "123",
                PhoneNumber = "0615988542",
                Username = "Maverick",
                CapitalizedUsername = "MAVERICK"
            });
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
            var Activity1 = await AddOrUpdateActivity(new VictuzActivity
            {
                Category = ActivityCategory.MemOnlyFree,
                Name = "Football",
                Description = "Football match",
                LocationId = 1,
                ActivityDate = new DateTime(2025, 2, 15, 14, 30, 0),
                HostId = 1,
                Price = 10,
                MemberPrice = 5,
                ParticipationLimit = 10
            });
            var Activity2 = await AddOrUpdateActivity(new VictuzActivity
            {
                Category = ActivityCategory.MemOnlyPay,
                Name = "Basketball",
                Description = "Basketball match",
                LocationId = 2,
                ActivityDate = DateTime.Now,
                HostId = 2,
                Price = 15,
                MemberPrice = 10,
                ParticipationLimit = 10
            });

            StatusMessage = "Standard Data Generated";
        }

        // -------------------- User Methods --------------------

        public async Task<User?> GetUser(int id)
        {
            try
            {
                return await _connection.Table<User>().FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public async Task<User?> GetUser(string userNameOrEmail)
        {
            try
            {
                return await _connection.Table<User>().FirstOrDefaultAsync(u =>
                    u.CapitalizedUsername == userNameOrEmail.ToUpper() ||
                    u.CapitalizedEmail == userNameOrEmail.ToUpper());
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public async Task<User> AddOrUpdateUser(User newUser)
        {
            int result = 0;
            try
            {
                if (newUser.Id != 0)
                {
                    result = await _connection.UpdateAsync(newUser);
                    StatusMessage = $"{result} row(s) updated :)";
                    return newUser;
                }
                else
                {
                    result = await _connection.InsertAsync(newUser);
                    StatusMessage = $"{result} row(s) added :)";
                    return newUser;
                }
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        // -------------------- Activity Methods --------------------

        public async Task<List<VictuzActivity>> GetAllActivities()
        {
            try
            {
                return await _connection.Table<VictuzActivity>().ToListAsync();
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return new List<VictuzActivity>();
            }
        }

        public async Task<VictuzActivity?> GetActivity(int id)
        {
            try
            {
                return await _connection.FindAsync<VictuzActivity>(id);
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public async Task<VictuzActivity> AddOrUpdateActivity(VictuzActivity activity)
        {
            int result = 0;
            try
            {
                if (activity.Id != 0)
                {
                    result = await _connection.UpdateAsync(activity);
                    StatusMessage = $"{result} row(s) updated :)";
                    return activity;
                }
                else
                {
                    result = await _connection.InsertAsync(activity);
                    StatusMessage = $"{result} row(s) added :)";
                    return activity;
                }
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public async Task DeleteActivity(VictuzActivity activity)
        {
            try
            {
                await _connection.DeleteAsync(activity);
                StatusMessage = "Activity deleted.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        // -------------------- Participation Methods --------------------

        public async Task<List<Participation>> GetParticipations(int activityId)
        {
            try
            {
                return await _connection.Table<Participation>().Where(p => p.ActivityId == activityId).ToListAsync();
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return new List<Participation>();
            }
        }
        public async Task<Participation> AddOrUpdateParticipation(Participation participation)
        {
            int result = 0;
            try
            {
                if (participation.Id != 0)
                {
                    result = await _connection.UpdateAsync(participation);
                    StatusMessage = $"{result} row(s) updated :)";
                    return participation;
                }
                else
                {
                    result = await _connection.InsertAsync(participation);
                    StatusMessage = $"{result} row(s) added :)";
                    return participation;
                }
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }


        // -------------------- Location Methods --------------------

        public async Task<VictuzLocation?> GetLocation(int id)
        {
            try
            {
                return await _connection.FindAsync<VictuzLocation>(id);
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public async Task<VictuzLocation> AddOrUpdateLocation(VictuzLocation location)
        {
            int result = 0;
            try
            {
                if (location.Id != 0)
                {
                    result = await _connection.UpdateAsync(location);
                    StatusMessage = $"{result} row(s) updated :)";
                    return location;
                }
                else
                {
                    result = await _connection.InsertAsync(location);
                    StatusMessage = $"{result} row(s) added :)";
                    return location;
                }
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        /*public async Task AddParticipationAsync(Participation participation)
        {
            try
            {
                // Check if the user is already participating in the activity
                var existing = _connection.Table<Participation>()
                    .FirstOrDefaultAsync(p => p.UserId == participation.UserId && p.ActivityId == participation.ActivityId);

                if (existing != null)
                {
                    StatusMessage = "User is already participating in this activity.";
                    return;
                }

                // Add participation
                _connection.Insert(participation);
                StatusMessage = "Participation successfully added.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
                throw;
            }
        }*/
    }
}
