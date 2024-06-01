using CHV;
using UnityEngine;

public class CharacterTarget : MonoBehaviour
{
    private Character owner;
    private Character target;

    [SerializeField] 
    private new BoxCollider2D collider;
    
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
        SetupTarget();
    }
    
    public void UpdateTarget()
    {
        SetupTarget();
    }

    public Character GetTarget()
    {
        return target;
    }
    
    private void SetupTarget()
    {
        if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.Monster)
        {
            target = SessionManager.I.SessionSpawner.MainCharacter;
        }
        else
        {
            target = null;
            
            var minDistance = float.MaxValue;
            foreach (var monster in SessionManager.I.SessionSpawner.Monsters)
            {
                var distance = Vector2.Distance(owner.transform.position, monster.transform.position);
                if (distance < minDistance)
                {
                    target = monster;
                    minDistance = distance;
                }
            }
        }
    }

    public bool IsHitTarget(Bounds inBounds)
    {     
        return collider.bounds.min.x <= inBounds.max.x && collider.bounds.max.x >= inBounds.min.x && collider.bounds.min.y <= inBounds.max.y && collider.bounds.max.y >= inBounds.min.y;
    }
}
