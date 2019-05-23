using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.CustomControls
{
    public interface IAndroidToast
    {
        void MakeLongToast(string message, string type);
        void MakeShortToast(string message, string type);
    }
}
