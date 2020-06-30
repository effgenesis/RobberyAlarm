using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionState : MonoBehaviour
{
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _gameover;
    [SerializeField] private AudioSource _victoryAudio;

    private bool CheckMissionComplete()
    {
        MoneyCounter moneyCounter = FindObjectOfType<MoneyCounter>();
        if (moneyCounter.NoMoneyRemain) { return true; }
        return false;
    }

    public void CheckForVictory()
    {
        if (CheckMissionComplete())
        {
            _victory.Invoke();
            _victoryAudio.Play();
        }
    }

    public void DeclareGameover()
    {
        _gameover.Invoke();
    }
}
