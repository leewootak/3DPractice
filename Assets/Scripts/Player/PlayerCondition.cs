using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public PlayerController controller;

    private bool isDead;
    private bool isBooster;

    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    private void Update()
    {
        health.Subtract(health.dotValue * Time.deltaTime);

        if(health.curValue <= 0f && !isDead)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        StartCoroutine(health.DotAdd(health.dotValue * amount));
    }

    public void TakeDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public void Booster(float speed)
    {
        controller.moveSpeed = speed;
        StartCoroutine(ResetSpeed(5f));
    }

    public IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        controller.moveSpeed = 5f;
    }

    public void Die()
    {
        isDead = true;
        Debug.Log("Die");
    }
}
