using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float damage;
    private float currentHealth;
    private PlayerShoot player;

    private void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerShoot>();
    }

    private void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            TakeDamage(player.BulletDamage);
    }
}
