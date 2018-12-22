using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PVPMistico.Views
{
    public partial class SignInPage : ContentPage
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
