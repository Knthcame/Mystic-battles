using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationFrame : BaseValidationFrame
    {
        public ValidationFrame()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}