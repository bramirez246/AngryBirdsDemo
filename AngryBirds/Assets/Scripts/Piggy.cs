using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float damageThreshold = 0.2f;
 
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void DamagePiggy(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0f)
        {
            KillPiggy();
        }
    }

    private void KillPiggy()
    {
        GameManager.instance.RemovePiggy(this);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;
        if(impactVelocity > damageThreshold)
        {
            DamagePiggy(impactVelocity);
        }
    }
}
