using Acr.UserDialogs;
using Android.Widget;
using Plugin.CurrentActivity;
using PVPMistico.Managers.Interfaces;

namespace PVPMistico.Droid.Managers
{
    public class DialogManager : IDialogManager
    {
        public void ShowAlert(AlertConfig config)
        {
            UserDialogs.Instance.Alert(config);
        }

        public void ShowConfirmationDialog(ConfirmConfig config)
        {
            UserDialogs.Instance.Confirm(config);
        }

        public void ShowToast(ToastConfig config)
        {
            if (config == null || config.Message == null)
                return;

            Toast.MakeText(CrossCurrentActivity.Current.AppContext, config.Message, ToastLength.Short).Show();
        }
    }
}