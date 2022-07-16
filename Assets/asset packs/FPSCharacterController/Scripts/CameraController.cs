using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSCharacterController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private int mouseSensitivity = 20;

        [SerializeField]
        private int gamepadSensitivity = 60;

        private int lookMultiplier;

        private PlayerInput _playerInput;
        private float xRotation = 0f;
        private Vector2 previousCameraChange;

        private void Start()
        {
            _playerInput = gameObject.GetComponent<PlayerInput>();
            _camera.transform.localRotation = Quaternion.Euler( transform.forward );
        }

        private void Update()
        {
            if (_playerInput.currentControlScheme == "Gamepad")
            {
                lookMultiplier = gamepadSensitivity;
            }
            else if ( _playerInput.currentControlScheme == "Keyboard and Mouse" )
            {
                lookMultiplier = mouseSensitivity;
            }
            HandleCamera();
        }
        
        private void HandleLookX(float x)
        {
            transform.Rotate( Vector3.up, x * Time.deltaTime * lookMultiplier  );
        }

        private void HandleLookY(float y)
        {
            xRotation -= y * Time.deltaTime * lookMultiplier;

            if ( xRotation <= -90 )
            {
                xRotation = -90;
            }
            else if ( xRotation >= 90 )
            {
                xRotation = 90;
            }

            _camera.transform.localRotation = Quaternion.Euler( xRotation, 0f, 0f );
        }

        void HandleCamera()
        {
            Vector2 lookVector = _playerInput.actions["CameraLook"].ReadValue<Vector2>();
            HandleLookX( lookVector.x );
            HandleLookY( lookVector.y );
            
        }

        void OnCameraLook(InputValue value)
        {
            // Vector2 mouseDelta = value.Get<Vector2>() * Time.deltaTime;
            // HandleLookX( mouseDelta.x );
            // HandleLookY( mouseDelta.y );
        }


        void Debug()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay( transform.position, transform.forward * 10 );
            Gizmos.color = Color.red;
            Gizmos.DrawRay( transform.position, transform.right * 10 );
        }

        private void OnDrawGizmos()
        {
            Debug();
        }
    }
}
