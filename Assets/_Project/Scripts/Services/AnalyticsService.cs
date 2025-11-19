using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System.Threading.Tasks;
using System;

public class AnalyticsService
{
        private bool _isInitialized = false;
        
        public async Task<bool> Initialize()
        {
            try
            {
                var options = new AppOptions
                {
                    ProjectId = "asteroids-47e80",
                    AppId = "1:230078761977:android:ed1b8b7942c15cd0c7cb28", 
                    ApiKey = "AIzaSyAmG6maUR2g_YcomzP7dKVe8Nf30bkiHxU"
                };
                
                if (IsValidConfig(options))
                {
                    return await InitializeWithCustomConfig(options);
                }
                else
                {
                    Debug.LogWarning("Invalid Firebase config. Using mock analytics for Windows.");
                    return InitializeMock();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Firebase initialization failed: {e.Message}");
                return InitializeMock();
            }
        }
        
        private void SetFirebaseDebugMode()
        {
#if UNITY_EDITOR
            string debugUserId = "unity_editor_" + System.DateTime.Now.Ticks;
            FirebaseAnalytics.SetUserId(debugUserId);
            
            FirebaseAnalytics.SetUserProperty("debug_mode", "true");
            FirebaseAnalytics.SetUserProperty("unity_editor", "true");
            FirebaseAnalytics.SetUserProperty("platform", "editor");
#endif
        }
        
        private async Task<bool> InitializeWithCustomConfig(AppOptions options)
        {
            try
            {
                FirebaseApp app = FirebaseApp.Create(options, "WindowsApp");
                
                if (app != null)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    _isInitialized = true;
                    
                    Debug.Log("Firebase Analytics initialized for Windows");
                    LogEvent("game_start", "platform", "windows");
                    
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Custom config failed: {e.Message}");
            }
            
            return false;
        }
        
        private bool InitializeMock()
        {
            Debug.Log("Analytics running in MOCK mode (events logged to console only)");
            _isInitialized = true;
            return true;
        }
        
        private bool IsValidConfig(AppOptions options)
        {
            return !string.IsNullOrEmpty(options.ProjectId) && 
                   !string.IsNullOrEmpty(options.AppId) && 
                   !string.IsNullOrEmpty(options.ApiKey) &&
                   options.ProjectId != "your-project-id";
        }
        
        public void LogEvent(string eventName, string parameterName = null, string parameterValue = null)
        {
            if (!_isInitialized) return;
            
            try
            {
                #if UNITY_STANDALONE_WIN || UNITY_EDITOR
                
                if (FirebaseApp.DefaultInstance == null || string.IsNullOrEmpty(FirebaseApp.DefaultInstance.Options.ProjectId))
                {
                    Debug.Log($"[ANALYTICS MOCK] {eventName} - {parameterName}: {parameterValue}");
                    return;
                }
                #endif
                
                if (string.IsNullOrEmpty(parameterName))
                {
                    FirebaseAnalytics.LogEvent(eventName);
                }
                else
                {
                    FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
                }
                
                Debug.Log($"[ANALYTICS] {eventName} - {parameterName}: {parameterValue}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Analytics error: {e.Message}");
                Debug.Log($"[ANALYTICS MOCK] {eventName} - {parameterName}: {parameterValue}");
            }
        }
        
        public void LogGameStart()
        {
            LogEvent("game_start", "platform", "windows");
        }
        
        public void LogEnemyKilled(string enemyType, int totalScore)
        {
            LogEvent("enemy_killed", "enemy_type", enemyType);
            LogEvent("score_update", "total_score", totalScore.ToString());
        }
        
        public void LogGameOver(int finalScore, int sessionTime)
        {
            LogEvent("game_over", 
                new Parameter("final_score", finalScore),
                new Parameter("session_time", sessionTime),
                new Parameter("platform", "windows"));
        }
        
        public void LogEvent(string eventName, params Parameter[] parameters)
        {
            if (!_isInitialized) return;
            
            try
            {
                #if UNITY_STANDALONE_WIN || UNITY_EDITOR
                if (FirebaseApp.DefaultInstance == null)
                {
                    string paramString = parameters.Length > 0 ? $" - {parameters.Length} params" : "";
                    Debug.Log($"[ANALYTICS MOCK] {eventName}{paramString}");
                    return;
                }
                #endif
                
                FirebaseAnalytics.LogEvent(eventName, parameters);
                Debug.Log($"[ANALYTICS] {eventName} with {parameters.Length} parameters");
            }
            catch (Exception e)
            {
                Debug.LogError($"Analytics error: {e.Message}");
            }
        }
}
