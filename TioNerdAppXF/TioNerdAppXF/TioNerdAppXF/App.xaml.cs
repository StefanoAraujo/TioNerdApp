using TioNerdAppXF.Helpers;
using TioNerdAppXF.Views;
using Xamarin.Forms;

namespace TioNerdAppXF
{
    public partial class Application : Xamarin.Forms.Application
    {
        public Application()
        {
            InitializeComponent();

            MainPage = Settings.IsLoggedIn ? new NavigationPage(new MainPage()) : new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
