using System;
using System.Diagnostics;

namespace PVPMistico.Views
{
    public partial class SignInPage : BasePage
    {
        public SignInPage()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
