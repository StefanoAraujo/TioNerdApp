using TioNerdAppXF.ViewModels;
using Xamarin.Forms;

namespace TioNerdAppXF.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}