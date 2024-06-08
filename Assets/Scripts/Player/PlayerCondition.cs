using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
    }

    public void GetStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Die()
    {
        Debug.Log("Die!!");
    }

    public void TakeDamage(int damage)
    {
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }
}
