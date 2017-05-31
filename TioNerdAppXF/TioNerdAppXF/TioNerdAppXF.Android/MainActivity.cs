using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TioNerdAppXF.Droid
{
    [Activity(Label = "TioNerdAppXF", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Chamada para o SDK do Azure
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new Application());
        }
    }
}

