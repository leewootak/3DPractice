using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 4.5f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, 4.5f, LayerMask.GetMask("Player")))
        {
            Debug.Log("충돌");
        }
    }
}
