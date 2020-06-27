using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private bool _controlEnabled = true;
    private bool _isMoving = false;

    void Update()
    {
        _isMoving = false;
        if (_controlEnabled)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _isMoving = true;
                _animator.SetTrigger("MoveLeft");
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _isMoving = true;
                _animator.SetTrigger("MoveRight");
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _isMoving = true;
                _animator.SetTrigger("MoveDown");
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                _isMoving = true;
                _animator.SetTrigger("MoveUp");
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
        }
        if (!_isMoving)
        {
            _animator.SetTrigger("Idle");
        }
    }
    
    public void DisableControl()
    {
        _controlEnabled = false;
    }
}
