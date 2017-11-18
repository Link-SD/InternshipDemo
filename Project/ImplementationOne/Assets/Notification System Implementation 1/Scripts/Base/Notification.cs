using System;
using UnityEngine;
using UnityEngine.Events;

namespace SD.Notification.ImplementationOne {
    public abstract class Notification : MonoBehaviour {
        public Data NotificationData { get; set; }

        private float _selfDestructTimer = 0.0f;

        protected void Start() {
            NotificationManager.InvokeOnNotificationOpened(this);
        }

        protected void Update() {
            if (NotificationData.LifeTime <= 0f) {
                return;
            }
            _selfDestructTimer += Time.deltaTime;
            if (_selfDestructTimer >= NotificationData.LifeTime) {
                Dismiss();
            }
        }

        public abstract void Setup();

        public virtual void Dismiss() {
            NotificationManager.InvokeOnNotificationClosed(this);
            Destroy(gameObject);
        }

        [Serializable]
        public struct Data {
            [Tooltip("Name functions as a key")] public string Name;
            public string Title;
            public string Description;
            public float LifeTime;
            public UnityEvent Action;

            public Data(string name, UnityEvent action, string title, string description) : this() {
                Name = name;
                Action = action;
                Title = title;
                Description = description;
                LifeTime = -1;
            }

            public Data(string name, UnityEvent action, string title, string description, float lifeTime) : this() {
                Name = name;
                Action = action;
                Title = title;
                Description = description;
                LifeTime = lifeTime;
            }

            public static Data Empty {
                get { return new Data("", null, "", ""); }
            }
        }
    }
}