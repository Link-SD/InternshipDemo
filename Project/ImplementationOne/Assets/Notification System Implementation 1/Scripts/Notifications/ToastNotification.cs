using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SD.Notification.ImplementationOne
{
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

        private void OnDestroy() {
            _button.onClick.RemoveAllListeners();
        }
    }
}