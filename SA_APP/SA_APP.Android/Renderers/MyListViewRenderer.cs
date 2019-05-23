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

[assembly: ExportRenderer(typeof(StoreListView), typeof(StoreListViewRenderer))]
namespace SA_APP.Droid.Renderers
{
    public class StoreListViewRenderer : ListViewRenderer
    {
        public StoreListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
        }

        //public override bool OnInterceptTouchEvent(MotionEvent ev)
        //{

        //    var storeListView = Element as StoreListView;
        //    if (storeListView.IsScrollingEnabled)
        //    {
        //        return base.OnInterceptTouchEvent(ev);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

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

        //public override bool OnTouchEvent(MotionEvent ev)
        //{
        //    var storeListView = Element as StoreListView;
        //    if (storeListView.IsScrollingEnabled)
        //    {
        //        return base.OnTouchEvent(ev);
        //    }
        //    else
        //    {
        //        RequestDisallowInterceptTouchEvent(false);
        //        return false;
        //    }
        //}
    }
}