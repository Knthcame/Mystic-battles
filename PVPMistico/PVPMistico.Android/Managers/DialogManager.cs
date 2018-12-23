using System;
using Acr.UserDialogs;
using Android.Widget;
using Plugin.CurrentActivity;
using PVPMistico.Managers.Interfaces;

namespace PVPMistico.Droid.Managers
{
    public class DialogManager : IDialogManager
    {
        public void ShowAlert(string title, string message, string okText)
        {
            UserDialogs.Instance.Alert(message, title, okText);
        }

        public void ShowConfirmationDialog(string title, string message, Action<bool> onAction, string okButton, string cancelText, Action cancelAction = null)
        {
            ConfirmConfig confirmConfig = new ConfirmConfig()
            {
                Title = title,
                Message = message,
                OkText = okButton,
                CancelText = cancelText,
                OnAction = onAction
            };
            UserDialogs.Instance.Confirm(confirmConfig);
        }

        public void ShowToast(string message)
        {
            if (message == null)
                return;

            Toast.MakeText(CrossCurrentActivity.Current.AppContext, message, ToastLength.Short).Show();
        }
    }
}