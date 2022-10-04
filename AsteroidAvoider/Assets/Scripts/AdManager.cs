using UnityEngine;
using UnityEngine.Advertisements;

namespace AsteroidAvoider
{
    public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {

        public static AdManager s_Instance;

        #region Config
        [Header("CONFIG")]
        private bool _testMode = true;
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        
        private GameOver _gameOver;

        private const string PLACEMENT_ID = "rewardedVideo";
#if UNITY_ANDROID
        private const string _gameId = "4957145";
#elif UNITY_IOS
        private const string _gameId = "4957144";
#endif
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            if(s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                s_Instance = this;
                DontDestroyOnLoad(gameObject);

                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }
        #endregion

        #region PublicMethods
        public void ShowAd(GameOver gameOver)
        {
            _gameOver = gameOver;
            Advertisement.Show(PLACEMENT_ID, this);
        }
        #endregion

        #region Interfaces & Inheritance
        public void OnInitializationComplete()
        {
            Debug.Log("unity ads initialization complete");
            Advertisement.Load(PLACEMENT_ID, this);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"unity ads initialization failed: {message}, error: {error.ToString()}");
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("unity ads loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"unity ads LOAD error: {message}, placementId: {placementId}, erroo: {error}", this);
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("unity ads show clicked");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch(showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    _gameOver.AfterAdPlayed();
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogWarning("Ad completion state unknown");
                    break;
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"unity ads SHOW error: {message}, placementId: {placementId}, error: {error.ToString()}", this);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("unity ads show start");
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
