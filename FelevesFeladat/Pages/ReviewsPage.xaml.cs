using FelevesFeladat.ViewModels;

namespace FelevesFeladat.Pages;

public partial class ReviewsPage : ContentPage
{
    ReviewsPageViewModel viewModel;
	public ReviewsPage(ReviewsPageViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.RefreshList();
    }

}