using TioNerdAppXF.ViewModels;
using Xamarin.Forms;

namespace TioNerdAppXF.Views
{
    public partial class FacebookLoginPage : ContentPage
    {
        FacebookLoginViewModel ViewModel => BindingContext as FacebookLoginViewModel;

        public FacebookLoginPage()
        {
            InitializeComponent();
            BindingContext = new FacebookLoginViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                await ViewModel.SetFacebookUserProfileAsync();
            }
        }
    }
}