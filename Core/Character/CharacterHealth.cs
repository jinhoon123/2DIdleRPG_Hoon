using System.Collections;
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
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private Transform head = null;
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        maxHealth = 100;
        currentHealth = maxHealth;
    }
    
    public void UpdateHealth(float amount)
    {
        currentHealth -= amount;

        if (amount > 0)
        {
            UIManager.I.InstantiateDamageUIPrefab("DamageNormal", head.position, amount);
            OnHit();
        }
        
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
    
    public void OnHit()
    {
        StartCoroutine(ChangeColorTemporarily(Color.red, 0.1f));
    }

    private IEnumerator ChangeColorTemporarily(Color color, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}