using System.Collections.Generic;
using UnityEngine;

namespace SD.Notification.ImplementationOne {
    /// <summary>
    /// Front end API for the user
    /// </summary>
    public class NotificationManager : Singleton<NotificationManager> {
        public const string NOTIFICATION_PATH = "Prefabs/Notifications";

        public int MaxNotificationsOnScreen = 5;

        public Notification.Data[] Notifications;

        public bool IsNotificationOpen { get; private set; }

        public static OnNotificationAction OnNotificationOpened;
        public static OnNotificationAction OnNotificationClosed;

        private readonly IList<Notification> _showingNotifications = new List<Notification>();

        private readonly Queue<KeyValuePair<Notification, Notification.Data>> _notificationQueue =
            new Queue<KeyValuePair<Notification, Notification.Data>>();

        private Dictionary<string, Notification.Data> _notificationEntries;

        private Transform _transform;

        private void Awake() {
            _transform = transform;
            OnNotificationOpened += HandleOpenedNotification;
            OnNotificationClosed += HandleClosedNotification;
            ScanForNotificationEntries();
        }

        public static void ShowNotification<T>(string key = "") where T : Notification {
            ShowNotification<T>(key, Notification.Data.Empty);
        }

        public static void ShowNotification<T>(Notification.Data data) where T : Notification {
            ShowNotification<T>("", data);
        }

        public static void ShowNotification<T>(string key, Notification.Data data) where T : Notification {
            string path = GetPrefabPath<T>();

            if (!IsKeyValid(key, out data, data)) {
                return;
            }

            Notification notification = Resources.Load<T>(path);

            if (notification == null) {
                Debug.LogError("The notification prefab at path '" + path + "' of type '" + typeof(T) +
                               "' could not be found. Did you forget to create it? Hint: The prefab name and the type should always be the same");
                return;
            }

            GenerateNotification(notification, data);
        }

        private static void GenerateNotification(Notification notification, Notification.Data data) {
            if (Instance._showingNotifications.Count >= Instance.MaxNotificationsOnScreen) {
                Instance._notificationQueue.Enqueue(
                    new KeyValuePair<Notification, Notification.Data>(notification, data));
                return;
            }

            notification = Instantiate(notification, Instance._transform, false);
            notification.NotificationData = data;
            notification.transform.SetAsFirstSibling();
            notification.Setup();
        }

        public static void InvokeOnNotificationOpened(Notification notification) {
            if (OnNotificationOpened != null) {
                OnNotificationOpened(notification);
            }
        }

        public static void InvokeOnNotificationClosed(Notification notification) {
            if (OnNotificationClosed != null) {
                OnNotificationClosed(notification);
            }
        }

        public static void DismissShowingNotifications() {
            foreach (Notification notification in Instance._showingNotifications) {
                Instance._showingNotifications.Remove(notification);
                notification.Dismiss();
            }
        }

        private static string GetPrefabPath<T>() {
            return NOTIFICATION_PATH + "/" + typeof(T).Name;
        }

        private static bool IsKeyValid(string key, out Notification.Data data, Notification.Data originalData) {
            data = originalData;
            if (string.IsNullOrEmpty(key)) {
                Debug.LogWarning("You did not enter a key. Showing empty or custom data type now..");
            }
            else if (!Instance._notificationEntries.TryGetValue(key, out data)) {
                Debug.LogError("The key '" + key + "' does not exist in the Notification Entries list");
                return false;
            }
            return true;
        }

        private void ScanForNotificationEntries() {
            _notificationEntries = new Dictionary<string, Notification.Data>();
            foreach (Notification.Data notification in Notifications) {
                _notificationEntries.Add(notification.Name, notification);
            }
        }

        private void HandleOpenedNotification(Notification notification) {
            _showingNotifications.Add(notification);
        }

        private void HandleClosedNotification(Notification notification) {
            _showingNotifications.Remove(notification);

            if (_notificationQueue.Count > 0) {
                KeyValuePair<Notification, Notification.Data> keyValuePair = _notificationQueue.Dequeue();
                GenerateNotification(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public delegate void OnNotificationAction(Notification notification);
    }
}