using UnityEngine.Events;

namespace PointNSheep.Common.StringTrigger {
    public static class StringTrigger {
        public static void StartListening(string eventName, UnityAction listener) {
            StringTriggerComponent.StartListening(eventName, listener);
        }
        public static void StopListening(string eventName, UnityAction listener) {
            StringTriggerComponent.StopListening(eventName, listener);
        }
        public static void Trigger(string eventName) {
            StringTriggerComponent.Trigger(eventName);
        }
    }
}