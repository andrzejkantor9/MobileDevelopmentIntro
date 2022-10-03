using UnityEngine;
using UnityEngine.Assertions;

using TMPro;

namespace SimpleDriving
{
    public class Score : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private float _scoreMultiplier = 5f;
        #endregion

        #region Cache
        [Header("CACHE")]
        [Space(8f)]
        [SerializeField]
        private TMP_Text _scoreText;

        public const string HIGH_SCORE_KEY = "HighScore";
        #endregion

        #region States
        private float _score;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            Assert.IsNotNull(_scoreText, "_scoreText is null");
        }

        private void Update() 
        {
            _score += Time.deltaTime * _scoreMultiplier;

            _scoreText.text = Mathf.FloorToInt(_score).ToString();
        }

        private void OnDestroy() 
        {
            int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

            if(_score > currentHighScore)
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, Mathf.FloorToInt(_score));
            }
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }

}