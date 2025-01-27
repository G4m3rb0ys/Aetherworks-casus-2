using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;

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
                InitializeDatabaseAsync().Wait();
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

            GenerateDataAsync().Wait();
        }

        private async Task GenerateDataAsync()
        {
            // Add standard users
            await AddOrUpdateUserAsync(new User
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

            await AddOrUpdateUserAsync(new User
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

            StatusMessage = "Standard data generated.";
        }

        // -------------------- User Methods --------------------

        public async Task<User?> GetUserAsync(int id)
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

        public async Task<User?> GetUserAsync(string userNameOrEmail)
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

        public async Task AddOrUpdateUserAsync(User user)
        {
            try
            {
                if (user.Id != 0)
                {
                    await _connection.UpdateAsync(user);
                    StatusMessage = "User updated.";
                }
                else
                {
                    await _connection.InsertAsync(user);
                    StatusMessage = "User added.";
                }
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        // -------------------- Activity Methods --------------------

        public async Task<List<VictuzActivity>> GetAllActivitiesAsync()
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

        public async Task<VictuzActivity?> GetActivityByIdAsync(int id)
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

        public async Task InsertActivityAsync(VictuzActivity activity)
        {
            try
            {
                await _connection.InsertAsync(activity);
                StatusMessage = "Activity added.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        public async Task UpdateActivityAsync(VictuzActivity activity)
        {
            try
            {
                await _connection.UpdateAsync(activity);
                StatusMessage = "Activity updated.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        public async Task DeleteActivityAsync(VictuzActivity activity)
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

        public async Task AddParticipationAsync(Participation participation)
        {
            try
            {
                await _connection.InsertAsync(participation);
                StatusMessage = "Participation added.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        public async Task<List<Participation>> GetParticipationsByActivityIdAsync(int activityId)
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

        // -------------------- Location Methods --------------------

        public async Task<VictuzLocation?> GetLocationByIdAsync(int id)
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

        public async Task InsertLocationAsync(VictuzLocation location)
        {
            try
            {
                await _connection.InsertAsync(location);
                StatusMessage = "Location added.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }

        public async Task UpdateLocationAsync(VictuzLocation location)
        {
            try
            {
                await _connection.UpdateAsync(location);
                StatusMessage = "Location updated.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Error: {e.Message}";
            }
        }
    }
}
