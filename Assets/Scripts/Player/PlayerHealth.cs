using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float invulnerabilityTime;
    private bool canTakeDamage;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void TakeDamage(float damageToTake)
    {
        if (!canTakeDamage) return;

        Debug.Log(currentHealth);

        StartCoroutine(nameof(Invulnerability));
        currentHealth -= damageToTake;

        if (currentHealth <= 0f)
        {
            // Placeholder
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().damage);
    }

    private IEnumerator Invulnerability()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invulnerabilityTime);

        canTakeDamage = true;
    }
}
