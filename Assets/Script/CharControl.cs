using System;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed = 5f;
    private CharacterController _characterController;
    private Animator _animator;
    private bool _isWatchingTime;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LookWatch();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        _characterController.Move(move * _MoveSpeed * Time.deltaTime);
    }

    private void LookWatch()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (!_isWatchingTime)
            {
                // Начинаем смотреть на часы
                _isWatchingTime = true;
                _animator.SetBool("IsWatchingTime", true);
                Debug.Log("Started looking at watch");
            }
            else
            {
                // Заканчиваем смотреть на часы
                _isWatchingTime = false;
                _animator.SetBool("IsWatchingTime", false);
                Debug.Log("Stopped looking at watch");
            }
        }
    }
}

