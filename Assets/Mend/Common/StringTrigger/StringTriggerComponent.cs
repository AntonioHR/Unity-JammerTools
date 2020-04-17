using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace PointNSheep.Common.StringTrigger {
    public class StringTriggerComponent : MonoBehaviour
    {
        private static bool started = false;
        private static bool setDestroy = false;
        private static StringTriggerComponent _instance;
        public static StringTriggerComponent Instance {
            get {
                Setup();
                return _instance;
            } 
        }

        private Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

        private static void Setup() {
            if(!started) {
                started = true;
                StringTriggerComponent.Create();
            }
        }

        private void OnDestroy() {
            if(!setDestroy) {
                setDestroy = true;
                SceneManager.sceneLoaded += Create; 
            }
        }

        private static void Create(Scene scene, LoadSceneMode mode) {
            Create();
        }
        private static void Create() {
            if(Instance != null) {
                Destroy(Instance.gameObject);
            }
            if(Application.isPlaying) {
                GameObject trigger = new GameObject("String Trigger Component");
                _instance = trigger.AddComponent<StringTriggerComponent>();
            }
        }


        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Instance.events.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance.events.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            if (Instance.events == null) return;
            UnityEvent thisEvent;
            if (Instance.events.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Trigger(string eventName)
        {
            UnityEvent thisEvent;
            if (Instance.events.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}