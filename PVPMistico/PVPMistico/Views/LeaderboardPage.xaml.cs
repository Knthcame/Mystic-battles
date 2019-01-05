using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaderboardPage : BasePage
	{
		public LeaderboardPage ()
		{
            try
            {
			    InitializeComponent ();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

		}
	}
}