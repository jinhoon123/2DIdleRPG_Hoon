using System.Collections.Generic;
using CHV;
using Core.Character;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillSystem : MonoBehaviour
{
    private Character owner;
    private readonly List<Skill> skills = new();
    
    public float globalCooldown;
    
    public GameObject skillObj;
    
    public void Init(Character character)
    {
        owner = character;
        globalCooldown = 1.0f;
        
        CreateSkill();
    }

    private void CreateSkill()
    {
        if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            var skill = Instantiate(skillObj, GameManager.I.skillRoot).GetComponent<Skill>();
            skill.Init(owner, 10001);
            skills.Add(skill);
        }
    }
    
    public void UpdateSkill()
    {
        globalCooldown -= Time.deltaTime;
        
        foreach (var skill in skills)
        {
            skill.CalculateCooldownTime();

            if (skill.IsUseSkill())
            {
                skill.ExecuteSkill();
            }
        }
    }
}