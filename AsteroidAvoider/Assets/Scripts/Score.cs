using UnityEngine;
using UnityEngine.Assertions;

using TMPro;

namespace AsteroidAvoider
{
    public class Score : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        private TMP_Text _scoreText;
        [SerializeField]
        private float _scoreMultiplier;
        #endregion

        #region States
        private float _score;
        private bool _shouldCount = true;
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
            if(_shouldCount)
            {
                _score += Time.deltaTime * _scoreMultiplier;
                _scoreText.text = Mathf.FloorToInt(_score).ToString();
            }
        }
        #endregion

        #region PublicMethods
        public void ResumeScoreCounting()
        {
            _shouldCount = true;
        }

        public int EndScoreCounting()
        {
            _shouldCount = false;
            _scoreText.text = string.Empty;

            return Mathf.FloorToInt(_score);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
