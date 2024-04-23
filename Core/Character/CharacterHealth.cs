using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Character Owner { get; private set; }
        
    public float maxHealth;
    public float currentHealth;
        
    public void Init(Character character)
    {
        Owner = character;
            
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Owner.Dead();
        }
    }
}