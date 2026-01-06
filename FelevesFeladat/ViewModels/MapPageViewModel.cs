using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FelevesFeladat.Service;
using System.Text.Json;

namespace FelevesFeladat.ViewModels
{
    [QueryProperty(nameof(ReviewData), "reviewData")]
    public partial class MapPageViewModel : ObservableObject
    {
        private readonly DataBase database;

        [ObservableProperty]
        string mapUrl;

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        MenuItem currentReview;

        public MapPageViewModel(DataBase dbService)
        {
            this.database = dbService;
            currentReview = new MenuItem();
        }

        public MenuItem ReviewData
        {
            set
            {
                CurrentReview = value;
                if (CurrentReview != null)
                {
                    LoadMap();
                }
            }
        }

        public MapPageViewModel()
        {
            //LoadMap();
        }

        public async Task LoadMap()
        {
            IsBusy = true;

            // Alapértelmezett koordináták (Budapest)
            double lat = 47.4979;
            double lon = 19.0402;
            bool showMarker = false;
            string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(CurrentReview.LocationName)}&format=json&limit=1";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "FelevesFeladat");
            var json = await client.GetStringAsync(url);
            //var doci = JsonSerializer.Deserialize(json);
            //var lat = doci.RootElement[0].GetProperty("lat").GetDouble();
            //var lon = doci.RootElement[0].GetProperty("lon").GetDouble();
            // Ha VAN érvényes adatunk, felülírjuk a koordinátákat
            if (lat != 0 && lon != 0)
            {
                CurrentReview.Latitude = lat;
                CurrentReview.Longitude = lon;
                showMarker = true; // Ilyenkor kérjük a piros tűt
            }

            // --- INNENTŐL A KÓD KÖZÖS (Nem kell duplán írni) ---

            var culture = CultureInfo.InvariantCulture;
            double zoomOffset = 0.02; // Zoom szint

            double minLon = lon - zoomOffset;
            double minLat = lat - zoomOffset;
            double maxLon = lon + zoomOffset;
            double maxLat = lat + zoomOffset;

            // URL Generálás
            //string url = $"https://www.openstreetmap.org/export/embed.html" +
            //             $"?bbox={minLon.ToString(culture)}%2C{minLat.ToString(culture)}%2C{maxLon.ToString(culture)}%2C{maxLat.ToString(culture)}" +
            //             $"&layer=mapnik";

            // Ha van konkrét pont, rátesszük a markert is
            if (showMarker)
            {
                url += $"&marker={lat.ToString(culture)}%2C{lon.ToString(culture)}";
            }

            MapUrl = url;
            IsBusy = false;
        }
    }
}
