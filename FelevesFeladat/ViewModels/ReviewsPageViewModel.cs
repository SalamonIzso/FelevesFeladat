using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.ViewModels
{
    public partial class ReviewsPageViewModel : ObservableObject
    {
        private readonly MyReviewPageViewModel myreview;

        [ObservableProperty]
        ObservableCollection<MenuItem> publishedReviews;
        public ReviewsPageViewModel(MyReviewPageViewModel myreview)
        {
            this.myreview = myreview;
            publishedReviews = new ObservableCollection<MenuItem>();
        }
        public void RefreshList()
        {
            publishedReviews.Clear();
            var publicItems = myreview.Reviews.Where(r => r.IsPublished == true).OrderByDescending(r => r.Rating);
            foreach (var item in publicItems)
            {
                publishedReviews.Add(item);
            }
        }
    }
}
