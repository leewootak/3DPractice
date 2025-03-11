using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour
{
    // UI에서 체력 정보를 관리하는 컴포넌트
    public UICondition uiCondition;
    // 플레이어 컨트롤러 (이동, 행동 등)
    public PlayerController controller;

    // 플레이어 사망 여부
    private bool isDead;

    // UI의 체력 값을 가져오는 프로퍼티
    Condition health { get { return uiCondition.health; } }

    // 데미지를 입었을 때 호출되는 이벤트
    public event Action onTakeDamage;

    private void Update()
    {
        // 체력이 0 이하이고 아직 사망하지 않은 경우 Die() 호출
        if (health.curValue <= 0f && !isDead)
        {
            Die();
        }
    }

    // 체력을 회복하는 함수
    public void Heal(float amount)
    {
        // 일정량 만큼 회복하는 DOT 코루틴 실행
        StartCoroutine(health.DotAdd(health.dotValue * amount));
    }

    // 일정 시간 동안 이동 속도를 증가시키는 부스터 기능
    public void Booster(float speed)
    {
        controller.moveSpeed = speed;
        // 5초 후 원래 속도로 복귀하는 코루틴 실행
        StartCoroutine(ResetSpeed(5f));
    }

    // 일정 시간 후 이동 속도를 원래대로 복구하는 코루틴
    public IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        controller.moveSpeed = 5f;
    }

    // 데미지를 입었을 때 체력을 감소시키고, 이벤트를 발생시킴
    public void TakeDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    // 플레이어 사망 처리 함수
    public void Die()
    {
        isDead = true;
        Debug.Log("Die");
    }
}
