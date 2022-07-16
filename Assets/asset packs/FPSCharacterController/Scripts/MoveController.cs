using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// using ExtensionMethods;
using UnityEngine.Rendering.VirtualTexturing;

namespace FPSCharacterController
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField]
        private int forwardSpeed = 10;

        [SerializeField]
        private float sprintMultiplier = 2f;

        [SerializeField]
        private int jumpHeight = 1;

        private bool hasJumped;

        private bool sprintToggleEnabled = false;

        private float velocityX;
        private float velocityY;
        private float velocityZ;

        private bool isGrounded;

        private CharacterController _controller;
        private PlayerInput _playerInput;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            HandleMovement();
            ApplyGravity();
            isGrounded = _controller.isGrounded;
            _controller.Move( new Vector3( velocityX, velocityY, velocityZ ) * Time.deltaTime );
        }

        float GetMovementSpeed()
        {
            if ( _playerInput.actions["HoldSprint"].IsPressed() || sprintToggleEnabled )
            {
                return forwardSpeed * sprintMultiplier;
            }
            else
            {
                return forwardSpeed;
            }
        }

        void HandleMovement()
        {
            Vector2 move = _playerInput.actions["Movement"].ReadValue<Vector2>();
            float currentSpeed = GetMovementSpeed();

            if ( Mathf.Abs( move.x ) > 0.1f || Mathf.Abs( move.y ) > 0.1f )
            {
                Vector3 localMovement = transform.TransformVector( new Vector3( move.x, velocityY, move.y ) );
                velocityX = localMovement.x * currentSpeed;
                velocityZ = localMovement.z * currentSpeed;
            }
            else
            {
                velocityX = 0f;
                velocityZ = 0f;
                sprintToggleEnabled = false;
            }
        }

        void ApplyGravity()
        {
            velocityY += Physics.gravity.y * Time.deltaTime;

            if ( _controller.isGrounded && velocityY < 0f)
            {
                hasJumped = false;
                velocityY = Physics.gravity.y;
            }
        }

        // called automatically by PlayerInput component
        void OnJump()
        {
            if ( _controller.isGrounded && !hasJumped )
            {
                velocityY = jumpHeight;
                hasJumped = true;
            }
        }

        // called automatically by PlayerInput component
        void OnToggleSprint()
        {
            sprintToggleEnabled = !sprintToggleEnabled;
        }
    }
}
