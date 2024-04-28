using CHV;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    #region Events
    public delegate void CharacterEvent();
    public event CharacterEvent OnCharacterDead; 

    #endregion

    private Character owner;
        
    public float maxHealth;
    public float currentHealth;

    public bool isDead;
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
        
        maxHealth = 100;
        currentHealth = maxHealth;
    }
    
    public void UpdateHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    
    private void Dead()
    {
        isDead = true;
        
        GameManager.I.monsters.Remove(owner);
        OnCharacterDead?.Invoke();
        Destroy(gameObject);
    }
}