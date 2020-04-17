using UnityEngine;
using GameAnalyticsSDK;

namespace PointNSheep.Common.Analytics {
    public class GameAnalyticsInitializer : MonoBehaviour {
        void Awake() {
            GameAnalytics.Initialize();
        }
    }
}