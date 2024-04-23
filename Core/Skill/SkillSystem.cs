using System.Collections.Generic;
using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    private Character owner;
    private readonly List<Skill> skills = new();
    
    public float globalCooldown;

    private Queue<Skill> skillQueue = new();
    
    public async UniTask Init(Character character)
    {
        owner = character;
        globalCooldown = 1.0f;

        await CreateSkill();
    }

    private async UniTask CreateSkill()
    {
        if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            var skill1 = new Skill();
            await skill1.Init(owner, 10001);
            
            var skill2 = new Skill();
            await skill2.Init(owner, 10002);
            
            skills.Add(skill1);
            skills.Add(skill2);
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
                skill.ResetCooldown();
                skillQueue.Enqueue(skill);
            }
        }

        if (skillQueue.Count > 0 && globalCooldown <= 0.0f && GameManager.I.monsters.Count != 0)
        {
            var skill = skillQueue.Dequeue();
            skill.Execute();

            globalCooldown = 1.0f;
        }
    }
}