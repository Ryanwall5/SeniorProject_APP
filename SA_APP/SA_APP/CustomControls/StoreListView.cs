using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SA_APP.CustomControls
{
    public class StoreListView : ListView
    {
        public static readonly BindableProperty IsScrollingEnableProperty =
    BindableProperty.Create(nameof(IsScrollingEnabled),
                            typeof(bool),
                            typeof(StoreListView),
                            true);

        public bool IsScrollingEnabled
        {
            get { return (bool)GetValue(IsScrollingEnableProperty); }
            set { SetValue(IsScrollingEnableProperty, value); }
        }

       

        
    }
}
