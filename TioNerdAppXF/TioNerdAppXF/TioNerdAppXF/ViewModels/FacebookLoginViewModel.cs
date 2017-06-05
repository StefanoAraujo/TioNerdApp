using System;
using System.Threading.Tasks;
using TioNerdAppXF.Models;
using TioNerdAppXF.Services;
using Xamarin.Forms;

namespace TioNerdAppXF.ViewModels
{
    public class FacebookLoginViewModel : BaseViewModel
    {
        private readonly AzureService facebookServices;

        private FacebookProfile _facebookProfile;
        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set { SetProperty(ref _facebookProfile, value); }
        }
        

        public FacebookLoginViewModel()
        {
            facebookServices = DependencyService.Get<AzureService>();
            Title = "Dados do Facebook";
        }

        public async Task SetFacebookUserProfileAsync()
        {
            if (IsBusy)
                return;
            
            try
            {
                IsBusy = true;
                FacebookProfile = await facebookServices.GetFacebookProfileAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Error", $"Aconteceu isto: {error.Message}", "ok");
            }
            finally
            {
                IsBusy = false;
            }
               
        }
    }
}
