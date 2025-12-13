using System;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed = 5f;
    [SerializeField] private float _RunSpeed = 8f;
    [SerializeField] private float _RotationSpeed = 200f;
    private CharacterController _characterController=> GetComponent<CharacterController>();
    private Animator _animator => GetComponent<Animator>();


    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

         bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? _RunSpeed : _MoveSpeed;
        transform.Rotate(Vector3.up * horizontal * _RotationSpeed * Time.deltaTime);

        //Vector3 move = transform.right * horizontal + transform.forward * vertical;
        Vector3 move = transform.forward * vertical * speed * Time.deltaTime;
        _characterController.Move(move * _MoveSpeed * Time.deltaTime);

         if (_animator != null)
        {
            float blend = Mathf.Abs(vertical) > 0 ? (isRunning ? 2f : 1f) : 0f;
            _animator.SetFloat("Blend", blend, 0.2f, Time.deltaTime);
        }
        
    }


    
}

