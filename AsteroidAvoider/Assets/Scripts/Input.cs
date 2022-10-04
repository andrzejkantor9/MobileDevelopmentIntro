using UnityEngine;

using UnityEngine.InputSystem;

namespace AsteroidAvoider
{
    public class Input : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private Vector3 testVector;
        [SerializeField]
        private float _forceMagnitude;
        [SerializeField]
        private float _maxVelocity;
        [SerializeField]
        private float _rotationSpeed;
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]

        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        #endregion

        #region States
        private Vector3 _movementDirection;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            _mainCamera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ProcessInput();
            KeepPlayerOnScreen();
            RotateToFaceVelocity();
        }

        private void FixedUpdate() 
        {
            if(_movementDirection != Vector3.zero)
            {
                Vector3 force = _movementDirection * _forceMagnitude * Time.fixedDeltaTime;
                
                _rigidbody.AddForce(force, ForceMode.Force);
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
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
        private void ProcessInput()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

                _movementDirection = transform.position - worldPosition;
                _movementDirection.z = 0f;
                _movementDirection.Normalize();

                // Debug.Log($"touch positzion: {touchPosition}, world position: {worldPosition}", this);
            }
            else
            {
                _movementDirection = Vector3.zero;
            }
        }

        private void KeepPlayerOnScreen()
        {
            Vector3 newPosition = transform.position;
            Vector3 viewportPosition  = _mainCamera.WorldToViewportPoint(transform.position);

            if(viewportPosition.x > 1)
            {
                newPosition.x = -newPosition.x + .1f;
            }
            else if(viewportPosition.x < 0)
            {
                newPosition.x = -newPosition.x - .1f;
            }

            if(viewportPosition.y > 1)
            {
                newPosition.y = -newPosition.y + .1f;
            }
            else if(viewportPosition.y < 0)
            {
                newPosition.y = -newPosition.y - .1f;
            }

            transform.position = newPosition;
        }

        private void RotateToFaceVelocity()
        {
            if(_rigidbody.velocity == Vector3.zero)
                return;

            // Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.right);
            Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, testVector);
            transform.rotation = 
                Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        #endregion
    }
}
