using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaderboardPage : ContentPage
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