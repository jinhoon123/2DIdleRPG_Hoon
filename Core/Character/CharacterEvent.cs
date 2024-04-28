using UnityEngine;
using UnityEngine.Serialization;

public class CharacterEvent : MonoBehaviour
{
    private Character owner;
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
    }
    
    public void OnAttackEvent()
    {
        owner.skillSystem.BasicAttack.Execute();
    }

    public void OnAttackEndEvent()
    {
        owner.skillSystem.BasicAttack.ResetCooldown();
    }
}
