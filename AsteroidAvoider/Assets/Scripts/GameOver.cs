using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using System;

namespace AsteroidAvoider
{
    public class GameOver : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        private AsteroidSpawner _asteroidSpawner;
        [SerializeField]
        private GameObject _player;
        
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private GameObject _gameOverCanvas;
        [SerializeField]
        private TMP_Text _gameOverText;
        [SerializeField]
        private Score _score;
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
            Assert.IsNotNull(_asteroidSpawner, "_asteroidSpawner is null");
            Assert.IsNotNull(_player, "_player is null");

            Assert.IsNotNull(_continueButton, "_continueButton is null");
            Assert.IsNotNull(_gameOverCanvas, "_gameOverCanvas is null");
            Assert.IsNotNull(_gameOverText, "_gameOverText is null");
            Assert.IsNotNull(_score, "_score is null");
        }
        #endregion

        #region PublicMethods
        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowAd()
        {
            AdManager.s_Instance.ShowAd(this);
            _continueButton.interactable = false;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void EndGame()
        {
            _asteroidSpawner.enabled = false;

            int finalScore = _score.EndScoreCounting();
            _gameOverText.text = $"Your score: {finalScore}";

            _gameOverCanvas.SetActive(true);
        }

        public void AfterAdPlayed()
        {
            _score.ResumeScoreCounting();
            _asteroidSpawner.enabled = true;
            _gameOverCanvas.SetActive(false);

            _player.transform.position = Vector3.zero;
            _player.SetActive(true);
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
