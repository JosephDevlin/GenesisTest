using Android.App;
using Android.Content.PM;
using Android.OS;
using GenesisTest.Core;
using GenesisTest.Forms.UI;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using System.Threading.Tasks;

namespace GenesisTest.Forms.Droid
{
    [Activity(
        Label = "GenesisTest.Forms",
        MainLauncher = true,
        Theme = "@style/MyTheme.Splash",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));

            Finish();

            base.OnCreate(bundle);
        }
    }
}