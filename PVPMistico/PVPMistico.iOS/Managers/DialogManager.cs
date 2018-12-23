using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Foundation;
using PVPMistico.Managers.Interfaces;
using UIKit;

namespace PVPMistico.iOS.Managers
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
            UserDialogs.Instance.Toast(message);
        }
    }
}