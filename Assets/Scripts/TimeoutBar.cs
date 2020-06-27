using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeoutBar : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _handle;

    private UnityEngine.UI.Slider _bar;

    void Start()
    {
        _bar = GetComponent<UnityEngine.UI.Slider>();
    }

    void Update()
    {
        _bar.value = _alarmSystem.TimeoutNormalized;
        if (_alarmSystem.GameoverIsClose)
        {
            _fill.color = Color.red;
            _handle.color = Color.red;
        }
        else
        {
            _fill.color = Color.white;
            _handle.color = Color.white;
        }
    }
}
