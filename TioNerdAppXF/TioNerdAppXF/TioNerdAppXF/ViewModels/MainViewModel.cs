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

        public ObservableCollection<Friend> FriendsCollection { get; set; }

        public Command GetFriendsCommand { get; }
        public Command LogOutCommand { get; }

        public Command UserInfoCommand { get; }

        public MainViewModel()
        {
            _azureService = DependencyService.Get<AzureService>();

            IsBusy = false;

            Title = "Lista de Amigos";

            UserId = Settings.UserId;
            Token = Settings.AuthToken;
            //IsLogged = Settings.IsLoggedIn;

            FriendsCollection = new ObservableCollection<Friend>();

            LogOutCommand = new Command(async () => await ExecuteLogOutCommandAsync(), () => !IsBusy);

            UserInfoCommand = new Command(async () => await ExecuteUserInfoCommandAsync(), () => !IsBusy);

            // Para o comando executar, "jogamos" uma condição que, somente poderá ser executado se a prop IsBusy não for true
            GetFriendsCommand = new Command(async () => await ExecuteGetFriendsCommandAsync(), () => !IsBusy);
        }

        private async Task ExecuteUserInfoCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new FacebookInfoPage());
        }

        async Task ExecuteGetFriendsCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }
            Exception error = null;
            try
            {
                IsBusy = true;

                var repositorio = new Repositorio();
                var items = await repositorio.GetFriends();
                FriendsCollection.Clear();
                foreach (var item in items)
                {
                    FriendsCollection.Add(item);
                }
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
                await DisplayAlert("OPS!!", $"Algo aconteceu ao carregar a lista! {error.Message}", "OK");

            return;
        }

        async Task ExecuteLogOutCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await _azureService.LogoutAsync();
                await Application.Current.MainPage.Navigation.PopAsync();
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Falha ao Sair!", $"O erro deu esta mensagem {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
