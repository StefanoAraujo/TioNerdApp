using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioNerdAppXF.Helpers;
using TioNerdAppXF.Services;
using TioNerdAppXF.Views;
using Xamarin.Forms;

namespace TioNerdAppXF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        public string UserId { get; private set; }
        public string Token { get; private set; }
        public bool IsLogged { get; private set; }

        public Command LogOutCommand { get; }

        public MainViewModel()
        {
            _azureService = DependencyService.Get<AzureService>();
            Title = "Página Principal";

            UserId = Settings.UserId;
            Token = Settings.AuthToken;
            IsLogged = Settings.IsLoggedIn;

            LogOutCommand = new Command(async () => await ExecuteLogOutCommandAsync());
        }

        async Task ExecuteLogOutCommandAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await _azureService.LogoutAsync();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Logout Failed", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
