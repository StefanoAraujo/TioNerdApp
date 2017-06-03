using System.Threading.Tasks;
using TioNerdAppXF.Helpers;
using TioNerdAppXF.Services;
using Xamarin.Forms;

namespace TioNerdAppXF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            _azureService = DependencyService.Get<AzureService>();

            Title = "Página de Login";
            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (IsBusy || !await LoginAsync())
                return;

            await PushAsync<MainViewModel>();
            IsBusy = false;
        }

        public Task<bool> LoginAsync()
        {
            IsBusy = true;

            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return _azureService.LoginAsync();
        }
    }
}
