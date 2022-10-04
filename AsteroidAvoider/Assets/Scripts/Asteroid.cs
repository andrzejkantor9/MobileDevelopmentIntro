using UnityEngine;

namespace AsteroidAvoider
{
    public class Asteroid : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void OnTriggerEnter(Collider other) 
        {
            Health health = other.GetComponent<Health>();

            if(health)
            {
                health.Crash();
            }
        }

        private void OnBecameInvisible() 
        {
            Destroy(gameObject);
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
