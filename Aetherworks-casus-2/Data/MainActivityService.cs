using Aetherworks_casus_2.MVVM.Models;
using SQLite;

namespace Aetherworks_casus_2.Data
{
    public class MainActivityService
    {
        private readonly LocalDbService _localDbService;

        public MainActivityService()
        {
            _localDbService = new LocalDbService();
        }

        public List<VictuzActivity> GetAllActivities()
        {
            return _localDbService._connection
                .Table<VictuzActivity>()
                .ToList();
        }

        public VictuzActivity? GetActivityById(int id)
        {
            return _localDbService._connection
                .Find<VictuzActivity>(id);
        }

        public int InsertActivity(VictuzActivity activity)
        {
            return _localDbService._connection.Insert(activity);
        }

        public int UpdateActivity(VictuzActivity activity)
        {
            return _localDbService._connection.Update(activity);
        }

        public int DeleteActivity(VictuzActivity activity)
        {
            return _localDbService._connection.Delete(activity);
        }
    }
}
