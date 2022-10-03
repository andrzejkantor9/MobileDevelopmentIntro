using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleDriving
{
    public class Car : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private float _speedGainPerSecond = 0.3f;
        [SerializeField]
        private float _turnSpeed = 200f;
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        #endregion

        #region States
        private int _steerValue;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Update() 
        {
            _speed += _speedGainPerSecond * Time.deltaTime;

            transform.Rotate(0f, _steerValue * _turnSpeed * Time.deltaTime, 0f);
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Obstacle"))
            {
                SceneManager.LoadScene(0);
            }
        }
        #endregion

        #region PublicMethods
        public void Steer(int value)
        {
            _steerValue = value;
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