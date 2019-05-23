using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Xamarin.Forms;
using SA_APP.Views;

namespace SA_APP.Droid
{
    [Activity(Label = "SA_APP", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<StoreMapPage>(this, "Landscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<StoreMapPage>(this, "Unspecified", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            MessagingCenter.Subscribe<AislesPage>(this, "Landscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<AislesPage>(this, "Unspecified", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            MessagingCenter.Subscribe<LowerDepartmentsPage>(this, "Landscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<LowerDepartmentsPage>(this, "Unspecified", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            MessagingCenter.Subscribe<SectionPage>(this, "Landscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<SectionPage>(this, "Unspecified", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);


        }
    }
}