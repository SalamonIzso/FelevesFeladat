using FelevesFeladat.Pages;

namespace FelevesFeladat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ReviewEditorPage), typeof(ReviewEditorPage));
        }
    }
}
