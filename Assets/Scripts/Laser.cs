using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    private bool _playerInSight = false;

    public bool PlayerInSight => _playerInSight;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        
        if (hit.collider.TryGetComponent<Player>(out Player player))
        {
            _playerInSight = true;
        }
        else
        {
            _playerInSight = false;
        }
    }
}
