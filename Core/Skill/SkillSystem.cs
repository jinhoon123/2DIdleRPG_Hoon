using System.Collections.Generic;
using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    private Character owner;

    #region Skill

    // 캐릭터 기본공격
    public Skill BasicAttack;
    
    // 캐릭터 스킬
    private readonly List<Skill> skills = new();
    private Queue<Skill> skillQueue = new();

    #endregion

    
    #region Options

    public float globalCooldown;

    #endregion
    
    
    public async UniTask Init(Character inOwner)
    {
        owner = inOwner;
        globalCooldown = 1.0f;

        await CreateSkill();
    }

    private async UniTask CreateSkill()
    {
        if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            var skill1 = new Skill();
            await skill1.Init(owner, 10001); // 기본공격
            BasicAttack = skill1;

             var skill2 = new Skill();
             await skill2.Init(owner, 20001);


            skills.Add(skill2);
        }
    }

    public void UpdateBasicAttack()
    {
        if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            // 기본공격 업데이트
            BasicAttack.CalculateCooldownTime();
            if (BasicAttack.IsUseSkill())
            {
                if (owner.characterAnimation.IsAnimationPlaying("Attack") == false)
                {
                    owner.characterAnimation.PlayAnimation("Attack");
                }
            }
        }
    }
    
    public void UpdateSkill()
    {
        // 글로벌 쿨타임 계산
        // 스킬은 1초에 한번씩 시전 가능
        if (globalCooldown >= 0)
        {
            globalCooldown -= Time.deltaTime;
        }
        
        // 스킬 사용
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