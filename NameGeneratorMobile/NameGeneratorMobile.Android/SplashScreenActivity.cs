using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace NameGeneratorMobile.Droid {
    [Activity(Label = "Baby Name Generator", Icon = "@mipmap/NGicon", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : AppCompatActivity {

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState) {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume() {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));

        }
    }
}