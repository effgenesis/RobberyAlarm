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
    [SerializeField] private AudioSource _timeoutAudio;
    [SerializeField] private UnityEvent _playerLeaveHouse;
    [SerializeField] private UnityEvent _timeIsUp;

    private float _time;
    private float _timeoutNormalized;
    private bool _isActive = false;
    private bool _timeoutFlag = false;
    public bool _timeoutIsClose = false;

    public float TimeoutNormalized => _timeoutNormalized;
    public bool GameoverIsClose => _timeoutIsClose;

    void Update()
    {
        
        CheckLasers();
        if (_isActive)
        {
            _time += Time.deltaTime;
            
        }
        else if (_time > 0)
        {
            _time -= Time.deltaTime * 4f;
        }
        else
        {
            _time = 0;
            
        }
        _timeoutNormalized = _time / _timeout;
        _alarmAudio.volume = TimeoutNormalized;
        _timeoutAudio.volume = (_time - _timeRedZone) / (_timeout - _timeRedZone);
        if (!_timeoutFlag && TimeoutNormalized >= 1)
        {
            _time = _timeout;
            _alarmAudio.Stop();
            _timeIsUp.Invoke();
            _timeoutFlag = true;
        }
        if (!GameoverIsClose && _time >= _timeRedZone)
        {
            _timeoutAudio.Play();
            _timeoutIsClose = true;
        }
        else if (GameoverIsClose && _time < _timeRedZone)
        {
            _timeoutAudio.Stop();
            _timeoutIsClose = false;
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
            _playerLeaveHouse.Invoke();
        }
    }

    public void StopSounds()
    {
        _alarmAudio.Stop();
        _timeoutAudio.Stop();
    }
}
