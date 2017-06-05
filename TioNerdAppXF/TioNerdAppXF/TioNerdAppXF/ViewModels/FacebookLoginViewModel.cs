using System;
using System.Threading.Tasks;
using TioNerdAppXF.Helpers;
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

        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Sexo { get; set; }

        public FacebookLoginViewModel()
        {
            facebookServices = DependencyService.Get<AzureService>();
            Title = "Dados do Facebook";
            Nome = Settings.Nome;            

        }

        public async Task SetFacebookUserProfileAsync()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;
                FacebookProfile = await facebookServices.GetFacebookProfileAsync();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }
            if (error != null)
               await DisplayAlert("Error", $"Aconteceu isto: {error.Message}", "ok");
                
            
        }
    }
}
