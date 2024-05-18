using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventServices
{
    private static EventServices instance = null;
    public static EventServices Instance
    {
        get
        {
            if (instance == null)
                instance = new EventServices();

            return instance;
        }
    }

    public event Action<int> OnEnemyDestroy;
    public event Action<int> OnPlayerFiredBullet;
    public event Action<float> OnDistanceTravelled;
    public event Action OnPlayerShoot;
    public event Action OnGameOver;
    public event Action<int> OnSetMaxHealthBar;
    public event Action<int> OnSetPlayerHealthBar;

    public void InvokeEnemyDestroy(int enemiesDestroyedCount)
    {
        OnEnemyDestroy?.Invoke(enemiesDestroyedCount);
    }

    public void InvokePlayerFiredBullet(int bulletCount)
    {
        OnPlayerFiredBullet?.Invoke(bulletCount);
    }

    public void InvokeDistanceTravelled(float distance)
    {
        OnDistanceTravelled?.Invoke(distance);
    }

    public void InvokePlayerShoot()
    {
        OnPlayerShoot?.Invoke();
    }

    public void InvokeGameOver()
    {
        OnGameOver?.Invoke();
    }

    public void InvokeSetMaxHealthBar(int maxHealth)
    {
        OnSetMaxHealthBar?.Invoke(maxHealth);
    }

    public void InvokeSetPlayerHealthBar(int health)
    {
        OnSetPlayerHealthBar?.Invoke(health);
    }
}

