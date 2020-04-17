
using System;
using GameAnalyticsSDK;

namespace PointNSheep.Common.Analytics
{
    public static class AnalyticsHandler {
        private static class AnalyticsCategory {
            public const string CARD_DRAW = "card_draw";
            public const string LEVEL = "level";
            public const string SESSION = "session";
            public const string BATTLE = "battle";
        }
        private static class AnalyticsSubcategory {
            public const string CARD_SKIPPED = "card_skipped";
            public const string SKIP_PRESSED = "skip";
            public const string CARD_DISCARDED = "discarded";
            public const string CARD_PICKED = "picked";
            public const string LEVEL_COMPLETED = "completed";
            public const string LEVEL_LOST = "lost";
            public const string LEVEL_STARTED = "started";
            public const string DAMAGE_TAKEN = "damage_taken";
            public const string ENEMIES_KILLED = "enemies_killed";
            public const string SESSION_STARTED = "start";
            public const string SESSION_COMPLETED = "completed";
            public const string SESSION_LOST = "lost";
            public const string SESSION_FINISHED = "finished";
            public const string CARD_USED = "card_used";
            
        }

        private static string EventName(string[] eventNames) {
            return String.Join(":", eventNames);
        }
        private static void SendEvent(float content, params string[] eventNames) {
            string eventName = EventName(eventNames);
            GameAnalytics.NewDesignEvent(eventName, content);
        }
        private static void SendEvent(params string[] eventNames) {
            string eventName = EventName(eventNames);
            GameAnalytics.NewDesignEvent(eventName);
        }
        public static void SkippedPressed (params string[] cardIdentifiers) {
            SendEvent(AnalyticsCategory.CARD_DRAW, AnalyticsSubcategory.SKIP_PRESSED);
            foreach(string cardIdentifier in cardIdentifiers) {
                SendEvent(AnalyticsCategory.CARD_DRAW, AnalyticsSubcategory.CARD_SKIPPED, cardIdentifier);
            }
        }
        public static void CardDiscarded (string cardIdentifier) {
            SendEvent(AnalyticsCategory.CARD_DRAW, AnalyticsSubcategory.CARD_DISCARDED, cardIdentifier);
        }
        public static void CardPicked (string cardIdentifier) {
            SendEvent(AnalyticsCategory.CARD_DRAW, AnalyticsSubcategory.CARD_PICKED, cardIdentifier);
        }
        public static void LevelStarted (string levelName) {
            SendEvent(AnalyticsCategory.LEVEL, AnalyticsSubcategory.LEVEL_STARTED, levelName);
        }
        public static void LevelCompleted (string levelName, float duration) {
            SendEvent(duration, AnalyticsCategory.LEVEL, AnalyticsSubcategory.LEVEL_COMPLETED, levelName);
        }
        public static void LevelLost (string levelName, float duration) {
            SendEvent(duration, AnalyticsCategory.LEVEL, AnalyticsSubcategory.LEVEL_LOST, levelName);
        }
        public static void LevelDamageTaken(string levelName, float damage) {
            SendEvent(damage, AnalyticsCategory.LEVEL, AnalyticsSubcategory.DAMAGE_TAKEN, levelName);
        }
        public static void LevelEnemiesKilled(string levelName, float killedNumber) {
            SendEvent(killedNumber, AnalyticsCategory.LEVEL, AnalyticsSubcategory.ENEMIES_KILLED, levelName);
        }
        public static void SessionStart() {
            SendEvent(AnalyticsCategory.SESSION, AnalyticsSubcategory.SESSION_STARTED);
        }
        public static void SessionCompleted(float duration) {
            SendEvent(duration, AnalyticsCategory.SESSION, AnalyticsSubcategory.SESSION_COMPLETED);
            SendEvent(duration, AnalyticsCategory.SESSION, AnalyticsSubcategory.SESSION_FINISHED);
        }
        public static void SessionLost(float duration) {
            SendEvent(duration, AnalyticsCategory.SESSION, AnalyticsSubcategory.SESSION_LOST);
            SendEvent(duration, AnalyticsCategory.SESSION, AnalyticsSubcategory.SESSION_FINISHED);
        }
        public static void CardUsed(string cardIdentifier) {
            SendEvent(AnalyticsCategory.BATTLE, AnalyticsSubcategory.CARD_USED, cardIdentifier);
        }
    }
}