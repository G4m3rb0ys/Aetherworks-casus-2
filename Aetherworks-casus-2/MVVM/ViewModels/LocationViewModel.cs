using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class LocationViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        public LocationViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }
    }
}
