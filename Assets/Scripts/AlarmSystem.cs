using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private Laser _outLaser;
    [SerializeField] private Laser _CenterLaser;
    [SerializeField] private Laser _inLaser;
    [SerializeField] private float _timeout;
    [SerializeField] private float _timeRedZone;
    [SerializeField] private AudioSource _alarmAudio;
    [SerializeField] private AudioSource _gameoverAudio;
    [SerializeField] private AudioSource _victoryAudio;
    [SerializeField] private UnityEvent _gameover;
    [SerializeField] private UnityEvent _victory;

    private float _time;
    private float _timeoutNormalized;
    private bool _isActive = false;
    private bool _gameoverFlag = false;
    public bool _gameoverIsClose = false;

    public float TimeoutNormalized => _timeoutNormalized;
    public bool GameoverIsClose => _gameoverIsClose;

    void Update()
    {
        
        CheckLasers();
        if (_isActive)
        {
            _time += UnityEngine.Time.deltaTime;
            
        }
        else if (_time > 0)
        {
            _time -= UnityEngine.Time.deltaTime * 4f;
        }
        else
        {
            _time = 0;
            
        }
        _timeoutNormalized = _time / _timeout;
        _alarmAudio.volume = TimeoutNormalized;
        _gameoverAudio.volume = (_time - _timeRedZone) / (_timeout - _timeRedZone);
        if (!_gameoverFlag && TimeoutNormalized >= 1)
        {
            _time = _timeout;
            _alarmAudio.Stop();
            _gameover.Invoke();
            _gameoverFlag = true;
        }
        if (!GameoverIsClose && _time >= _timeRedZone)
        {
            _gameoverAudio.Play();
            _gameoverIsClose = true;
        }
        else if (GameoverIsClose && _time < _timeRedZone)
        {
            _gameoverAudio.Stop();
            _gameoverIsClose = false;
        }
    }

    public void CheckLasers()
    {
        if (_inLaser.PlayerInSight && _CenterLaser.PlayerInSight && !_isActive)
        {
            Activate();
        }
        if (_CenterLaser.PlayerInSight && _outLaser.PlayerInSight && _isActive)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            Debug.Log("Activate");
            _alarmAudio.loop = true;
            _alarmAudio.Play();
        }
    }

    private void Deactivate()
    {
        if (_isActive)
        {
            _isActive = false;
            Debug.Log("Deactivate");
            if (CheckForVictory())
            {
                _victory.Invoke();
                _alarmAudio.Stop();
                _gameoverAudio.Stop();
                _victoryAudio.Play();
            }
        }
    }

    private bool CheckForVictory()
    {
        Money[] allMoney = GameObject.FindObjectsOfType<Money>();
        foreach (var money in allMoney)
        {
            if (!money.IsTaken)
            {
                return false;
            }
        }
        return true;
    }
}
