using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SD.Notification.ImplementationTwo {
    [AddComponentMenu("Notifications/Toast Notification")]
    [RequireComponent(typeof(Button))]
    public class ToastNotification : Notification {
        public Text TitleText;
        public Text DescriptionText;
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            transform.SetAsFirstSibling();
        }

        public override void Setup() {
            TitleText.text = NotificationData.Title;
            DescriptionText.text = NotificationData.Description;
            _button.onClick.AddListener(Dismiss);

            if (NotificationData.Action != null) {
                _button.onClick.AddListener(NotificationData.Action.Invoke);
            }
        }

        protected override void Create() {
            transform.SetParent(GameObject.Find("Notification Stack").transform, false);
            transform.SetAsFirstSibling();
            gameObject.name = "Notification_" + NotificationData.Name;
            Setup();
        }

        private void OnDestroy() {
            _button.onClick.RemoveAllListeners();
        }
    }
}