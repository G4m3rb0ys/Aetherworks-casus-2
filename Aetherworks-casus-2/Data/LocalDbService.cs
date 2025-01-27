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
        public readonly SQLiteConnection _connection;
        public string? statusMessage;

        public LocalDbService()
        {
            _connection = new SQLiteConnection(
                DataConstanst.DatabasePath,
                DataConstanst.flags
            );

            // Create tables
            _connection.CreateTable<User>();
            _connection.CreateTable<Participation>();
            _connection.CreateTable<Penalty>();
            _connection.CreateTable<Suggestion>();
            _connection.CreateTable<SuggestionLiked>();
            _connection.CreateTable<VictuzActivity>();
            _connection.CreateTable<VictuzLocation>();
        }

        // -------------------- User Methods --------------------
        public async Task<User?> GetUserAsync(int id)
        {
            try
            {
                return _connection.Table<User>().FirstOrDefault(t => t.Id == id);
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        // -------------------- Activity Methods --------------------
        public List<VictuzActivity> GetAllActivities()
        {
            try
            {
                return _connection.Table<VictuzActivity>().ToList();
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return new List<VictuzActivity>();
            }
        }

        public VictuzActivity? GetActivityById(int id)
        {
            try
            {
                return _connection.Find<VictuzActivity>(id);
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public void InsertActivity(VictuzActivity activity)
        {
            try
            {
                _connection.Insert(activity);
                statusMessage = "Activity successfully added.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }

        public void UpdateActivity(VictuzActivity activity)
        {
            try
            {
                _connection.Update(activity);
                statusMessage = "Activity successfully updated.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }

        public void DeleteActivity(VictuzActivity activity)
        {
            try
            {
                _connection.Delete(activity);
                statusMessage = "Activity successfully deleted.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }

        // -------------------- Participation Methods --------------------
        public void AddParticipation(Participation participation)
        {
            try
            {
                _connection.Insert(participation);
                statusMessage = "Participation successfully added.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }

        public List<Participation> GetParticipationsByActivityId(int activityId)
        {
            try
            {
                return _connection.Table<Participation>().Where(p => p.ActivityId == activityId).ToList();
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return new List<Participation>();
            }
        }

        // -------------------- Location Methods --------------------
        public VictuzLocation? GetLocationById(int id)
        {
            try
            {
                return _connection.Find<VictuzLocation>(id);
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
                return null;
            }
        }

        public void InsertLocation(VictuzLocation location)
        {
            try
            {
                _connection.Insert(location);
                statusMessage = "Location successfully added.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }

        public void UpdateLocation(VictuzLocation location)
        {
            try
            {
                _connection.Update(location);
                statusMessage = "Location successfully updated.";
            }
            catch (Exception e)
            {
                statusMessage = $"Error: {e.Message}";
            }
        }
    }
}
