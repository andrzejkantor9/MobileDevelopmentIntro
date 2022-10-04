using UnityEngine;
using UnityEngine.Assertions;

using UnityEngine.Purchasing;

namespace SimpleDriving
{
    public class Store : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        private GameObject _restoreButton;
        
        private const string NEW_CAR_ID = "com.berserkerbanana.SimpleDriving.NewCar";
        public const string NEW_CAR_UNLOCKED_KEY = "NewCarUnlocked";
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
            Assert.IsNotNull(_restoreButton, "_restoreButton is null");

            if(Application.platform != RuntimePlatform.IPhonePlayer)
            {
                _restoreButton.SetActive(false);
            }
        }
        #endregion

        #region PublicMethods
        public void OnPurchaseComplete(Product product)
        {
            if(product.definition.id == NEW_CAR_ID)
            {
                PlayerPrefs.SetInt(NEW_CAR_UNLOCKED_KEY, 1);
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            Debug.LogWarning($"Failed to purchase product: {product.definition.id}, because: {reason}");
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
