using CommunityToolkit.Mvvm.ComponentModel;
using SQLitePCL;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FelevesFeladat
{
    public partial class MenuItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [ObservableProperty]
        string nameOfItem;

        [ObservableProperty]
        string restaurantName;

        [ObservableProperty]
        int rating;

        [ObservableProperty]
        bool isPublished;

        [ObservableProperty]
        string imagePath;

        [ObservableProperty]
        string locationName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationCoordinates))]
        double latitude;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationCoordinates))] 
        double longitude;

        [Ignore] 
        public Location LocationCoordinates => new Location(latitude, longitude);

    }
}
