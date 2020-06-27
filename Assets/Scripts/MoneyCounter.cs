using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    private bool _noMoneyRemain = false;

    public bool NoMoneyRemain => _noMoneyRemain;

    void Start()
    {
        CheckMoneyRemain();
    }

    public void CheckMoneyRemain()
    {
        Money[] allMoney = GetComponentsInChildren<Money>();
        _noMoneyRemain = true;
        foreach (var money in allMoney)
        {
            if (!money.IsTaken)
            {
                _noMoneyRemain = false;
                break;
            }
        }
        
    }
}
