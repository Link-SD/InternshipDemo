using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD.Notification.ImplementationTwo {
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

        protected override void Create() {
            transform.SetParent(GameObject.Find("Notification Manager").transform, false);
            transform.SetAsFirstSibling();
            gameObject.name = "Notification_" + NotificationData.Name;
            Setup();
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