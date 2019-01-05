using Acr.UserDialogs;
using PVPMistico.Enums;

namespace PVPMistico.Managers.Interfaces
{
    public interface IDialogManager
    {
        void ShowToast(ToastConfig config, ToastModes mode = ToastModes.Info);

        void ShowToast(string message, ToastModes mode = ToastModes.Info);

        void ShowAlert(AlertConfig config);

        void ShowConfirmationDialog(ConfirmConfig config);

        void ShowLoading(string title);

        void ShowLoading();

        void EndLoading();
    }
}
