using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup;

namespace PVPMistico.Droid
{
    [Activity(Label = "Mystic Battles", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);
            Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);

            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
        }
    }
}

