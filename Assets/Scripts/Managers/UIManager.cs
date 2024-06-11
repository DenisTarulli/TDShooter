using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthBar;
    private PlayerHealth player;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        SetMaxHealth();
    }

    private void SetMaxHealth()
    {
        playerHealthBar.maxValue = player.MaxHealth;
        playerHealthBar.value = player.CurrentHealth;
    }

    private void SetHealth(float health)
    {
        playerHealthBar.value = health;
    }

    private void OnEnable()
    {
        PlayerHealth.OnHurt += SetHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHurt -= SetHealth;
    }
}
