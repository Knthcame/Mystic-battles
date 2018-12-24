using Acr.UserDialogs;
using PVPMistico.Managers.Interfaces;

namespace PVPMistico.iOS.Managers
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
            UserDialogs.Instance.Toast(config);
        }
    }
}