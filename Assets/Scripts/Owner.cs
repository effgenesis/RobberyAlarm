using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void WakeUp()
    {
        _animator.SetTrigger("WakeUp");
    }
}
