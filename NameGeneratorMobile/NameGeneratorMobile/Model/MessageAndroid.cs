using Android.App;
using Android.Widget;
using NameGeneratorMobile.Model;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace NameGeneratorMobile.Model {
    public class MessageAndroid : IMessage {
        public void LongAlert(string message) {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Long);
            toast.SetGravity(Android.Views.GravityFlags.Center, 0, 0);
            toast.Show();
        }

        public void ShortAlert(string message) {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
            toast.SetGravity(Android.Views.GravityFlags.Center, 0, 0);
            toast.Show();
        }
    }
}