using System;

namespace PVPMistico.Managers.Interfaces
{
    public interface IDialogManager
    {
        void ShowToast(string message);

        void ShowAlert(string Title, string message, string okButton);

        void ShowConfirmationDialog(string title, string message, Action<bool> onAction, string okButton, string cancelText, Action cancelAction = null);
    }
}
