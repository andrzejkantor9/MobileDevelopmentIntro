using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

namespace SimpleDriving
{
    public class MainMenu : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private int _maxEnergy;
        [SerializeField]
        private int _energyRechargeDuration;
        #endregion

        #region Cache
        [Header("CACHE")]
        [Space(8f)]
        [SerializeField]
        private TMP_Text _highScoreText;
        [SerializeField]
        private TMP_Text _energyText;
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private AndroidNotifications _androidNotifications;
        [SerializeField]
        private IOSNotifications _iosNotifications;

        private int _energy;

        private const string ENERGY_KEY = "Energy";
        private const string ENERGY_READY_KEY = "EnergyReady";
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
            Assert.IsNotNull(_highScoreText, "_highScoreText is null");
            Assert.IsNotNull(_energyText, "_energyText is null");
            Assert.IsNotNull(_playButton, "_playButton is null");

            Assert.IsNotNull(_androidNotifications, "_energyText is null");
            Assert.IsNotNull(_iosNotifications, "_iosNotifications is null");
        }

        private void Start() 
        {
            OnApplicationFocus(true);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus)
                return;

            StopAllCoroutines();

            int highScore = PlayerPrefs.GetInt(Score.HIGH_SCORE_KEY, 0);

            _highScoreText.text = $"HIGH SCORE: {highScore}";

            _energy = PlayerPrefs.GetInt(ENERGY_KEY, _maxEnergy);

            if(_energy == 0)
            {
                string energyReadyString = PlayerPrefs.GetString(ENERGY_READY_KEY, string.Empty);

                if(energyReadyString == string.Empty)
                    return;

                DateTime energyReady = DateTime.Parse(energyReadyString);

                if(DateTime.Now > energyReady)
                {
                    _energy = _maxEnergy;
                    PlayerPrefs.SetInt(ENERGY_KEY, _energy);
                }
                else
                {
                    _playButton.interactable = false;
                    StartCoroutine(CallDelayedUnscaled(EnergyRecharged, (energyReady - DateTime.Now).Seconds));
                }
            }

            _energyText.text = $"Play ({_energy})";
        }
        #endregion

        #region PublicMethods
        public void Play()
        {
            if(_energy < 1)
                return;

            --_energy;
            PlayerPrefs.SetInt(ENERGY_KEY, _energy);

            if(_energy == 0)
            {
                DateTime energyReady = DateTime.Now.AddMinutes(_energyRechargeDuration);
                PlayerPrefs.SetString(ENERGY_READY_KEY, energyReady.ToString());
#if UNITY_ANDROID
                _androidNotifications.ScheduleNotification(energyReady);
#elif UNITY_IOS
                _iosNotifications.ScheduleNotification(_energyRechargeDuration);
#endif
            }

            SceneManager.LoadScene(1);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        private void EnergyRecharged()
        {
            _playButton.interactable = true;

            _energy = _maxEnergy;
            PlayerPrefs.SetInt(ENERGY_KEY, _energy);
            _energyText.text = $"Play ({_energy})";
        }

        private IEnumerator CallDelayedUnscaled(Action callback, float delaySeconds)
        {
            yield return new WaitForSecondsRealtime(delaySeconds);

            callback();
        }
        #endregion
    }
}
