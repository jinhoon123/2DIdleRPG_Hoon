using CHV;
using UnityEngine;

public class CharacterTarget : MonoBehaviour
{
    private Character owner;
    private Character target;

    [SerializeField] 
    private new BoxCollider2D collider;
    
    private Bounds bounds;
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
        bounds = collider.bounds;
        
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
            target = GameManager.I.mainCharacter;
        }
        else
        {
            target = null;
            
            var minDistance = float.MaxValue;
            foreach (var monster in GameManager.I.monsters)
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
        return bounds.min.x <= inBounds.max.x && bounds.max.x >= inBounds.min.x && bounds.min.y <= inBounds.max.y && bounds.max.y >= inBounds.min.y;
    }
}
