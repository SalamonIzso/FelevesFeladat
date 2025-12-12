using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.ViewModels
{
    public partial class MapPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string mapUrl;

        [ObservableProperty]
        bool isBusy;

        public MapPageViewModel()
        {
            // Alapértelmezett betöltés (pl. Budapest)
            LoadMap(47.4979, 19.0402);
        }

        public void LoadMap(double lat, double lon)
        {
            //Ezt csak kiprobálom mert állítólag jót tesz
            IsBusy = true;

            //Gemini segített mert idk mi ez
            var culture = CultureInfo.InvariantCulture;
            double zoomOffset = 0.02;

            // Kiszámolja ezt a szart
            double minLon = lon - zoomOffset;
            double minLat = lat - zoomOffset;
            double maxLon = lon + zoomOffset;
            double maxLat = lat + zoomOffset;
            // Fontos: Az URL-ben is ponttal (.) kell lennie a számoknak!
            MapUrl = $"https://www.openstreetmap.org/export/embed.html" +
                     $"?bbox={minLon.ToString(culture)}%2C{minLat.ToString(culture)}%2C{maxLon.ToString(culture)}%2C{maxLat.ToString(culture)}" +
                     $"&layer=mapnik";
            IsBusy = false;
        }
    }
}
