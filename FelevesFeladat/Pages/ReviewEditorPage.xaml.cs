using FelevesFeladat.ViewModels;

namespace FelevesFeladat.Pages;

public partial class ReviewEditorPage : ContentPage
{
	ReviewEditorPageViewModel viewModel;
	public ReviewEditorPage(ReviewEditorPageViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}
}