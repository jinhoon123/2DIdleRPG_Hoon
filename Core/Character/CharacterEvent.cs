using UnityEngine;
using UnityEngine.Serialization;

public class CharacterEvent : MonoBehaviour
{
    private Character owner;
    
    #region Events
    
    public delegate void CharacterStateEvent();
    public event CharacterStateEvent OnCharacterDead; 

    #endregion
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
    }
    
    public void OnAttackAnimationEvent()
    {
        owner.skillSystem.BasicAttack.Execute();
    }

    public void OnAttackAnimationEndEvent()
    {
        owner.skillSystem.BasicAttack.ResetCooldown();
    }

    public void OnDeadAnimationEndEvent()
    {
        OnCharacterDead?.Invoke();
    }
}
