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

        private readonly IMapService _mapService;
        private readonly IGeocodingService _geoService;
        [ObservableProperty] string mapUrl;
        [ObservableProperty] MenuItem currentReview;
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

        public MapPageViewModel(IMapService mapService) => _mapService = mapService;

        public async Task LoadMap()
        {
            if (CurrentReview == null)
            {
                CurrentReview = new MenuItem();
            }

            // 1. Ha van cím, de nincs koordináta, akkor lekérjük
            if (!string.IsNullOrWhiteSpace(CurrentReview.LocationName) && CurrentReview.Latitude == 0)
            {
                var location = await _geoService.GetLocationAsync(CurrentReview.LocationName);
                if (location != null)
                {
                    CurrentReview.Latitude = location.Latitude;
                    CurrentReview.Longitude = location.Longitude;
                }
            }

            // 2. Legyártjuk az URL-t a már meglévő (vagy frissen lekért) koordinátákkal
            MapUrl = await _mapService.GetMapUrlAsync(CurrentReview);
        }
    }
}
