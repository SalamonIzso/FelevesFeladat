using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FelevesFeladat.Pages;
using FelevesFeladat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.ViewModels
{
    public partial class MyReviewPageViewModel : ObservableObject
    {
        private readonly DataBase database;
        [ObservableProperty]
        MenuItem selectedReview;
        public ObservableCollection<MenuItem> Reviews { get; set; }
            
        public MyReviewPageViewModel(DataBase dataBase) { 
            Reviews = new ObservableCollection<MenuItem>();
            this.database = dataBase;
        }

        [RelayCommand]
        async Task AddNew() {
            await Shell.Current.GoToAsync(nameof(ReviewEditorPage));
        }

        [RelayCommand]
        async Task Delete(MenuItem menuItem) {
            if (menuItem != null) {
                await database.DeleteReviewAsync(menuItem);
                Reviews.Remove(menuItem);
            }
        }

        [RelayCommand]
        public async Task Edit(MenuItem menuItem)
        {
            if (menuItem == null) return;
            var navigationParameter = new Dictionary<string, object>
            {
                { "menuItemToEdit", menuItem }
            };
            await Shell.Current.GoToAsync(nameof(ReviewEditorPage), navigationParameter);
        }

        [RelayCommand]
        public async Task Publish(MenuItem menuitem)
        {
            if (menuitem == null) return;

            
            if (menuitem.IsPublished == false)
            {

                    menuitem.IsPublished = true;
                    await database.SaveReviewAsync(menuitem);

                    await Shell.Current.DisplayAlert("Siker", "Az értékelés felkerült a toplistára!", "OK");
            }
            else
            {
                menuitem.IsPublished = false;
                await database.SaveReviewAsync(menuitem);

                await Shell.Current.DisplayAlert("Info", "Az értékelést levetted a listáról (helyi módosítás).", "OK");
            }
        }


        public async Task LoadFromDatabsae() {
            Reviews.Clear();
            var itemsFromData = await database.GetReviewsAsync();
            foreach (var item in itemsFromData)
            {
                Reviews.Add(item);
            }
        }
    }
}
