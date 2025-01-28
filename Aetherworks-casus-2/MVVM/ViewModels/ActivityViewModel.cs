using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;
using static ZXing.RGBLuminanceSource;
using ZXing.Net.Maui;
using Microsoft.Maui.Graphics;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class ActivityViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        public ActivityViewModel(LocalDbService dbService) 
        {
            _dbService = dbService;
        }
    }
}
