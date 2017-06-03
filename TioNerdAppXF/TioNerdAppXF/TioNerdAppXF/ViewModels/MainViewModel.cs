using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TioNerdAppXF.Helpers;
using TioNerdAppXF.Models;
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

        public ObservableCollection<Friend> Friends { get; set; }

        public Command LogOutCommand { get; }

        public Command GetFriendsCommand { get; set; }

        public MainViewModel()
        {
            _azureService = DependencyService.Get<AzureService>();
            Title = "Página Principal";

            Friends = new ObservableCollection<Friend>();

            UserId = Settings.UserId;
            Token = Settings.AuthToken;
            IsLogged = Settings.IsLoggedIn;

            LogOutCommand = new Command(async () => await ExecuteLogOutCommandAsync(), () => !IsBusy);

            // Para o comando executar, "jogamos" uma condição que, somente poderá ser executado se a prop IsBusy não for true
            GetFriendsCommand = new Command(async () => await GetFriends(), () => !IsBusy);
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

        async Task GetFriends()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;


                    var repository = new Repositorio();
                    var items = await repository.GetFriends();

                    Friends.Clear();
                    foreach (var friend in items)
                    {
                        Friends.Add(friend);
                    }

                }
                catch (Exception e)
                {
                    Error = e;
                }
                finally
                {
                    IsBusy = false;
                }

                if (Error != null)
                {
                    await DisplayAlert("Algo aconteceu!", $"O seguinte erro aconteceu {Error.Message}", "OK");
                }
            }
            return;
        }

    }
}
