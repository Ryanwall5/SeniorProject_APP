using Android.Views;
using Android.Widget;
using SA_APP.CustomControls;
using SA_APP.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidToast))]
namespace SA_APP.Droid
{
    public class AndroidToast : IAndroidToast
    {
        public void MakeLongToast(string message, string type)
        {
            Toast toast = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);
            var v = (ViewGroup)toast.View;

            if (v.ChildCount > 0 && v.GetChildAt(0) is TextView)
            {

                if (type == "error")
                {
                    v.SetBackgroundColor(Android.Graphics.Color.Black);

                    TextView textView = (TextView)v.GetChildAt(0);

                    textView.SetTextColor(Android.Graphics.Color.Red);
                    toast.Show();
                }
                else if (type == "submit")
                {
                    v.SetBackgroundColor(Android.Graphics.Color.LawnGreen);

                    TextView textView = (TextView)v.GetChildAt(0);
                    textView.SetTextColor(Android.Graphics.Color.Black);
                    toast.Show();
                }                
            }

            
        }

        public void MakeShortToast(string message, string type)
        {
            Toast toast = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            var v = (ViewGroup)toast.View;
            if (v.ChildCount > 0 && v.GetChildAt(0) is TextView)
            {

                if (type == "error")
                {
                    v.SetBackgroundColor(Android.Graphics.Color.Black);

                    TextView textView = (TextView)v.GetChildAt(0);
                    textView.SetTextColor(Android.Graphics.Color.Red);
                }
                else if (type == "submit")
                {
                    v.SetBackgroundColor(Android.Graphics.Color.LawnGreen);

                    TextView textView = (TextView)v.GetChildAt(0);
                    textView.SetTextColor(Android.Graphics.Color.Black);
                }
            }

            toast.Show();
        }
    }
}