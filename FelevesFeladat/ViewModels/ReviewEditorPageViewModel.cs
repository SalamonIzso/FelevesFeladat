using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FelevesFeladat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.ViewModels
{
    [QueryProperty(nameof(MenuItemToEdit), "menuItemToEdit")]
    public partial class ReviewEditorPageViewModel : ObservableObject
    {
        private readonly DataBase dbService;

        [ObservableProperty]
        MenuItem currentReview;

        public ReviewEditorPageViewModel(DataBase dbService)
        {
            this.dbService = dbService;
            currentReview = new MenuItem();
        }
        public MenuItem MenuItemToEdit
        {
            set
            {
                if (value != null)
                {
                    CurrentReview = value;
                }
            }
        }

        [RelayCommand]
        async Task SaveNewFood()
        {
            if (string.IsNullOrWhiteSpace(CurrentReview.NameOfItem))
            {
                await Shell.Current.DisplayAlert("Hiba", "Adj nevet a kajának!", "OK");
                return;
            }
            try
            {

                var locations = await Geocoding.Default.GetLocationsAsync(CurrentReview.LocationName);
                var location = locations?.FirstOrDefault();

                if (location != null)
                {
                    // Ha megvan, beírjuk a Review objektumba
                    CurrentReview.Latitude = location.Latitude;
                    CurrentReview.Longitude = location.Longitude;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Geocoding hiba: {ex.Message}");
            }
            await dbService.SaveReviewAsync(CurrentReview);
            await Shell.Current.GoToAsync("..");
        }
     



        [RelayCommand]
        async Task TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {

                    // Elmentjük a képet a belső tárhelyre, hogy később is meglegyen
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    CurrentReview.ImagePath = localFilePath;
                }
            }
        }

        [RelayCommand]
        async Task ChoosePhoto() {
                FileResult? photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    CurrentReview.ImagePath = localFilePath;
                }
        }     
    }
}

