using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public PlayerStatBar playerStatBar;
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }
    private void OnHealthEvent(Character character)
    {
        float persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}
