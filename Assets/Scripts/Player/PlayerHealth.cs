using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float invulnerabilityTime;
    private bool canTakeDamage;
    private float currentHealth;

    public static event Action<float> OnHurt;
    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }

    private void Awake()
    {
        currentHealth = maxHealth;        
    }

    private void Start()
    {
        canTakeDamage = true;
    }

    private void TakeDamage(float damageToTake)
    {
        if (!canTakeDamage) return;

        StartCoroutine(nameof(Invulnerability));
        currentHealth -= damageToTake;
        OnHurt?.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            GameManager.Instance.GameOver();
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
