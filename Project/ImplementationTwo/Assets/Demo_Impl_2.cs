using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using SD.Notification.ImplementationTwo;
using UnityEngine.Events;

/// <summary>
/// using the API 
/// </summary>
public class Demo_Impl_2 : MonoBehaviour {
    private bool _notificationToggle;

    private void Start() {
        ShowCustomNotification();
    }

    private void ShowCustomNotification() {
        UnityEvent specialEvent = new UnityEvent();
        specialEvent.AddListener(ShowOtherKindOfNotifications);
        Notification.Data data = new Notification.Data("", specialEvent, "Very Cool Title", "Best Description ever.");
        NotificationManager.ShowNotification<ModalNotification>(data);
    }

    private void ShowPredefinedNotification() {
        NotificationManager.ShowNotification<ModalNotification>("failed");
    }

    private void ShowOtherKindOfNotifications() {
        NotificationManager.ShowNotification<ToastNotification>("success");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            string key = _notificationToggle ? "failed" : "success";
            NotificationManager.ShowNotification<ToastNotification>(key);
            _notificationToggle = !_notificationToggle;
        }
    }
}
