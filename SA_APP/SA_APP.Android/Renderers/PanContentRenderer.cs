using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SA_APP.CustomControls;
using SA_APP.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PanContentView), typeof(PanContentRenderer))]
namespace SA_APP.Droid.Renderers
{
    public class PanContentRenderer : ViewRenderer
    {
        public PanContentRenderer(Context context) : base(context) { }
        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:              
                    RequestDisallowInterceptTouchEvent(true);
                    break;
                case MotionEventActions.Up:
                    RequestDisallowInterceptTouchEvent(false);
                    break;
            }

            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            BringToFront();
            return true;
        }
    }
}