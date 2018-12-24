using Acr.UserDialogs;
using System;

namespace PVPMistico.Managers.Interfaces
{
    public interface IDialogManager
    {
        void ShowToast(ToastConfig config);

        void ShowAlert(AlertConfig config);

        void ShowConfirmationDialog(ConfirmConfig config);
    }
}
