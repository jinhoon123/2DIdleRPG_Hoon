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
    
    public void UpdateHealth(Unified damage)
    {
        currentHealth -= CalculateFinalDamage(damage);

        if (damage.FloatPart > 0)
        {
            UIManager.I.InstantiateDamageUIPrefab("DamageNormal", head.position, damage.FloatPart);
            OnHit();
        }
        
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private float CalculateFinalDamage(Unified damage)
    {
        // 크리티컬 확률
        
        // 확률로 인해 크리티컬이 true이면 데미지를 2배로 증가
        
        return damage.FloatPart;
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