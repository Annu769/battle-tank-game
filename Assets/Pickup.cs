using System.Collections;
using System.Collections.Generic;
using BattleTank;
using UnityEngine;

public class Pickup : MonoBehaviour
{
   [SerializeField] private int healAmount = 10;
    private void OnTriggerEnter(Collider other)
    {
        // Ensure the trigger is with a TankView object
        TankView tankView = other.gameObject.GetComponent<TankView>();
        if (tankView != null)
        {
            // Add healing to the tank
            tankView.AddHeal(healAmount);

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }
}

