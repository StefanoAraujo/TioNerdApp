using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TioNerdAppXF.Authentication;
using TioNerdAppXF.Helpers;
using TioNerdAppXF.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace TioNerdAppXF.Services
{
    public class AzureService
    {
        private static readonly string AppUrl = "http://tionerdmobile.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                // Estamos falando que o usuário já existe, só ir verificar nos nossos Helpers
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Ops!",
                        "Não foi possível logar na aplicação. Tente novamente!", "ok");
                });

                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            Initialize();

            if (Client.CurrentUser?.MobileServiceAuthenticationToken == null)
                return;

            // Remove the token from the cache
            Client.CurrentUser = new MobileServiceUser(string.Empty) {MobileServiceAuthenticationToken = string.Empty};
            Settings.UserId = string.Empty;
            Settings.AuthToken = string.Empty;

            // Remove the token from the MobileServiceClient
            await Client.LogoutAsync();
        }

    }
}
