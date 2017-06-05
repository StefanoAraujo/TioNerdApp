using TioNerdAppXF.ViewModels;
using Xamarin.Forms;

namespace TioNerdAppXF.Views
{
    public partial class FacebookInfoPage : ContentPage
    {
        private FacebookLoginViewModel ViewModel => BindingContext as FacebookLoginViewModel;

        public FacebookInfoPage()
        {
            InitializeComponent();
            BindingContext = new FacebookLoginViewModel();            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
                await ViewModel.SetFacebookUserProfileAsync();
        }
    }
}