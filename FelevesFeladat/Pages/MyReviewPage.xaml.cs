	
using FelevesFeladat.Pages;
using FelevesFeladat.ViewModels;

namespace FelevesFeladat.Pages;

public partial class MyReviewPage : ContentPage
{
	MyReviewPageViewModel viewModel;
	public MyReviewPage(MyReviewPageViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
       viewModel?.LoadFromDatabase();
    }
}