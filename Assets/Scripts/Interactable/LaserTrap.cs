using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    // 플레이어 체력 관리를 위한 PlayerCondition 컴포넌트 참조
    public PlayerCondition condition;
    // 플레이어가 이미 피격되었는지 확인하는 플래그
    [SerializeField] private bool hasHit = false;

    void Update()
    {
        // 레이를 빨간색 선으로 표시
        Debug.DrawRay(transform.position, transform.forward * 4.5f, Color.red);
        // 레이캐스트를 사용해 플레이어 레이어와 충돌하는지 확인
        bool hit = Physics.Raycast(transform.position, transform.forward, 4.5f, LayerMask.GetMask("Player"));

        if (hit)
        {
            // 플레이어와 처음 충돌했을 때만 데미지 적용
            if (!hasHit)
            {
                condition.TakeDamage(20f); // 플레이어에게 데미지 적용
                Debug.Log("충돌");
                hasHit = true; // 중복 데미지 방지를 위해 플래그 설정
            }
        }
        else
        {
            // 플레이어가 레이에서 벗어나면 피격 플래그 초기화
            hasHit = false;
        }
    }
}
