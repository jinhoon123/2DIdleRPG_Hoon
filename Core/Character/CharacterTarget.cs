using CHV;
using Core.Character;
using UnityEngine;

public class CharacterTarget : MonoBehaviour
{
    private Character owner;
    private Character target;

    public void Init(Character character)
    {
        owner = character;
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
            var minDistance = float.MaxValue;
            foreach (var monster in GameManager.I.monsters)
            {
                var distance = Vector2.Distance(owner.transform.position, monster.transform.position);
                if (distance < minDistance)
                {
                    target = monster;
                    distance = minDistance;
                }
            }
        }
    }
}
