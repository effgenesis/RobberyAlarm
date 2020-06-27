using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerCollision;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private AudioSource _collectAudio;

    private MoneyCounter _moneyCounter;

    private bool _isTaken = false;

    public bool IsTaken => _isTaken;

    private void Start()
    {
        _moneyCounter = GetComponentInParent<MoneyCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && !IsTaken)
        {
            _isTaken = true;
            _collectAudio.Play();
            _moneyCounter?.CheckMoneyRemain();
            _playerCollision.Invoke();
            _sprite.color = Color.clear;
        }
    }
}
