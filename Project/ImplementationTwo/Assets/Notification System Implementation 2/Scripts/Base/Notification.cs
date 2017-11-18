using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SD.Notification.ImplementationTwo {
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

        public static Notification Create(Notification notification, Data data) {
            Notification instance = Instantiate(notification);
            instance.NotificationData = data;
            instance.Create();

            return instance;
        }

        protected abstract void Create();

        public abstract void Setup();

        public virtual void Dismiss() {
            Destroy(gameObject);
            NotificationManager.InvokeOnNotificationClosed(this);
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