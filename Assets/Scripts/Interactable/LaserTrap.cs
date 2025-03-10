using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public PlayerCondition condition;
    [SerializeField] private bool hasHit = false; // 이미 피격했는지 확인하는 플래그

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 4.5f, Color.red);
        bool hit = Physics.Raycast(transform.position, transform.forward, 4.5f, LayerMask.GetMask("Player"));

        if (hit)
        {
            if (!hasHit)
            {
                condition.TakeDamage(20f);
                Debug.Log("충돌");
                hasHit = true; // 한 번 호출 후 플래그 설정
            }
        }
        else
        {
            // 플레이어가 레이에서 벗어나면 플래그 리셋
            hasHit = false;
        }
    }
}
