using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public bool isHealing;

    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    private void Update()
    {
        if (!isHealing)
        {
            health.Subtract(health.dotValue * Time.deltaTime);
        }

        if(health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        isHealing = true;
        health.Add(health.dotValue * amount);
        Invoke("Decreasing", 3f);
    }

    public void Decreasing()
    {
        isHealing = false;
    }

    public void Booster(float amount)
    {

    }

    public void TakeDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Die");
    }
}
