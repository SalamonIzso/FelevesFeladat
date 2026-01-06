using FelevesFeladat.ViewModels;

namespace FelevesFeladat.Pages;

public partial class MapPage : ContentPage
{
    MapPageViewModel viewModel;
	public MapPage(MapPageViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LoadMap();
    }
}