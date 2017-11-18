using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD.Notification.ImplementationOne {
    [AddComponentMenu("Notifications/Modal Notification")]
    public class ModalNotification : Notification {
        public Text TitleText;
        public Text DescriptionText;

        public Button ConfirmButton;
        public Button CancelButton;

        public override void Setup() {
            TitleText.text = NotificationData.Title;
            DescriptionText.text = NotificationData.Description;

            ConfirmButton.onClick.AddListener(Dismiss);
            CancelButton.onClick.AddListener(Dismiss);

            if (NotificationData.Action != null) {
                ConfirmButton.onClick.AddListener(NotificationData.Action.Invoke);
            }
        }

        private void OnDestroy() {
            CancelButton.onClick.RemoveAllListeners();
            ConfirmButton.onClick.RemoveAllListeners();
        }

        public void Confirmed() {
            print("Confirmed");
        }
    }
}