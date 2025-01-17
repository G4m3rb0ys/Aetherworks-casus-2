﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class ParticipationViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        public ParticipationViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }
    }
}