using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TioNerdAppXF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _propTitle = string.Empty;
        bool _propIsBusy;

        public string Title
        {
            get { return _propTitle; }
            set { SetProperty(ref _propTitle, value); }
        }
        
        public bool IsBusy
        {
            get { return _propIsBusy; }
            set { SetProperty(ref _propIsBusy, value); }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // Comparando os valores do tipo Genérico.
            // Se os valores forem iguais, return false.
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        // Método Genérico que pode receber qualquer quantidade de parâmetros, mas trabalha com o Tipo ViewModel que herda de BaseViewModel
        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel);

            var viewModelTypeName = viewModelType.Name;

            // Pegando o tamanho da string 'ViewModel'. Que é igual a 9 caractéres.
            var viewModelWordLenght = "ViewModel".Length;

            // Está tirando a palavra ViewModel da classe. Ficando apenas: 'MonkeyHub.AboutPage'.
            var viewTypeName = $"TioNerdAppXF.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLenght)}Page";
            var viewType = Type.GetType(viewTypeName);

            var page = Activator.CreateInstance(viewType) as Page;

            var viewModel = Activator.CreateInstance(viewModelType, args);
            if (page != null)
            {
                page.BindingContext = viewModel;
            }
            
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
