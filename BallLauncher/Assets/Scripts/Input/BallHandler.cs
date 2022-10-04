using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Assertions;

using UnityEngine.InputSystem;
#if MULTITOUCH
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
#endif

//MULTITOUCH is not working
namespace BallLauncher.Input
{
    public class BallHandler : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private float _delayDuration = .15f;
        [SerializeField]
        private float _respawnDelay;
        #endregion

        #region Cache
        [Header("CACHE")]
    	[Space(8f)]
        [SerializeField]
        private GameObject _ballPrefab;
        [SerializeField]
        private Rigidbody2D _pivot;

        private Camera _camera;
        private bool _isDragging;

        
        private Rigidbody2D _currentBallRigidbody;
        private SpringJoint2D _currentBallSpringJoint;
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
            Assert.IsNotNull(_ballPrefab, "_ballPrefab is null");
            Assert.IsNotNull(_pivot, "_pivot is null");
        }

        private void Start() 
        {
            _camera = Camera.main;

            SpawnNewBall();
        }

        private void OnEnable() 
        {
#if MULTITOUCH
            EnhancedTouchSupport.Enable();
#endif
        }

        private void OnDisable() 
        {
#if MULTITOUCH
            EnhancedTouchSupport.Disable();
#endif
        }

        private void Update() 
        {
            if(!_currentBallRigidbody)
                return;

#if !MULTITOUCH
            if(!Touchscreen.current.primaryTouch.press.isPressed)
            {
                if(_isDragging)
                {
                    LaunchBall();
                }

                _isDragging = false;
            }
            else
            {
                _currentBallRigidbody.isKinematic = true;
                _isDragging = true;

                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Vector3 worldPosition = _camera.ScreenToWorldPoint(touchPosition);
                
                _currentBallRigidbody.position = worldPosition;
                Debug.Log($"touchPosition: {touchPosition}, worldPosition: {worldPosition}");
            }
#else
            if(Touch.activeTouches.Count != 0)
            {
                Vector2 touchPosition = new Vector2();

                foreach(Touch touch in Touch.activeTouches)
                {
                     if(new Rect(0,0,Screen.width, Screen.height).Contains(touch.screenPosition))
                        touchPosition += touch.screenPosition;
                }

                touchPosition /= Touch.activeTouches.Count;
                Vector3 worldPosition = _camera.ScreenToWorldPoint(touchPosition);

                _currentBallRigidbody.position = worldPosition;
            }
            else
            {
                if(_isDragging)
                {
                    LaunchBall();
                }

                _isDragging = false;
            }
#endif
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        private void SpawnNewBall()
        {
            GameObject ballInstance = Instantiate(_ballPrefab, _pivot.position, Quaternion.identity);

            _currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
            _currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

            _currentBallSpringJoint.connectedBody = _pivot;
        }

        private void LaunchBall()
        {
            _currentBallRigidbody.isKinematic = false;
            _currentBallRigidbody = null;

            StartCoroutine(CallDelayed(_delayDuration, Detach));
            // StartCoroutine(DetachBall(_delayDuration));
        }

        private void Detach()
        {
            _currentBallSpringJoint.enabled = false;
            _currentBallSpringJoint = null;

            StartCoroutine(CallDelayed(_respawnDelay, SpawnNewBall));
        }

        // private IEnumerator DetachBall(float delay)
        // {
        //     _currentBallSpringJoint.enabled = false;
        //     _currentBallSpringJoint = null;
        // }

        private IEnumerator CallDelayed(float delay, System.Action callback)
        {
            yield return new WaitForSeconds(delay);

            callback();
        }
        #endregion
    }
}
