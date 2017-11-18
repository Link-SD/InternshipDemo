using UnityEngine;

namespace SD.Notification.ImplementationOne {
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        private static T _instance;

        public static T Instance {
            get {
                if (_instance == null)
                    _instance = CreateOrFind();
                return _instance;
            }
        }

        private void Awake() {
            if (!Initialize()) {
                Destroy(gameObject);
            }
        }

        private bool Initialize() {
            if (_instance != null) {
                return false;
            }
            _instance = CreateOrFind();
            return true;
        }

        private static T CreateOrFind() {
            var objectsOfType = FindObjectsOfType<T>();
            if (objectsOfType.Length > 1)
                Debug.LogWarning("Too many instances of " + typeof(T).Name + ".");
            if (objectsOfType.Length > 0)
                return objectsOfType[0];
            var obj = new GameObject("SingleInstance<" + typeof(T).Name + "> temp").AddComponent<T>();
            obj.gameObject.name = obj.name;
            return obj;
        }
    }
}