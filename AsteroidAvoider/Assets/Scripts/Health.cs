using UnityEngine;
using UnityEngine.Assertions;

namespace AsteroidAvoider
{
    public class Health : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        private GameOver _gameOver;
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
            Assert.IsNotNull(_gameOver, "_gameOver is null");
        }
        #endregion

        #region PublicMethods
        public void Crash()
        {
            _gameOver.EndGame();
            gameObject.SetActive(false);
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
