using TioNerdAppXF.ViewModels;
using Xamarin.Forms;

namespace TioNerdAppXF
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
