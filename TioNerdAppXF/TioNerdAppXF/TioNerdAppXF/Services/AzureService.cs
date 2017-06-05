using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TioNerdAppXF.Authentication;
using TioNerdAppXF.Helpers;
using TioNerdAppXF.Services;
using Xamarin.Forms;
using TioNerdAppXF.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[assembly: Dependency(typeof(AzureService))]
namespace TioNerdAppXF.Services
{
    public class AzureService
    {
        List<AppServiceIdentity> identities = null;

        private static readonly string AppUrl = "http://tionerdmobile.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;

        public MobileServiceUser User { get; set; }

        public FacebookProfile Profile { get; set; }

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
            User = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (User == null)
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
                Settings.AuthToken = User.MobileServiceAuthenticationToken;
                Settings.UserId = User.UserId;
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            if (Client.CurrentUser?.MobileServiceAuthenticationToken == null)
                return;

            // Invalidate the token on the mobile backend
            var authUri = new Uri($"{AppUrl}/.auth/logout");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", Client.CurrentUser.MobileServiceAuthenticationToken);
                await httpClient.GetAsync(authUri);
            }

            // Remove the token from the cache
            Client.CurrentUser = new MobileServiceUser(string.Empty) { MobileServiceAuthenticationToken = string.Empty };
            Settings.UserId = string.Empty;
            Settings.AuthToken = string.Empty;

            // Remove the token from the MobileServiceClient
            await Client.LogoutAsync();
        }

        public async Task<FacebookProfile> GetFacebookProfileAsync()
        {

            identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            //var completeName = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).Value;

            var userToken = identities[0].AcessToken;

            var requestUrl = "https://graph.facebook.com/v2.9/me/?fields=id,name,gender,picture.height(700){is_silhouette,url,width}&access_token=" + userToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            Settings.AuthToken = User.MobileServiceAuthenticationToken;
            Settings.UserId = User.UserId;

            return facebookProfile;
        }

    }
}
