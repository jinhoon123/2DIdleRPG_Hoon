using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Skill
{
    public Character Owner; // 스킬 시전자

    public SkillAsset Asset;
    public DataTable_Skill_Data Data; // 스킬 데이터
    
    private float cooldownTime;
    
    public async UniTask Init(Character character, int skillID)
    {
        Owner = character;
        Data = DataTable_Skill_Data.GetData(skillID);
        
        await ResourceHandler.I.InstantiateSkillAsset<SkillAsset>("Skill" + Data.TID.ToString(), (result) => Asset = result);

        ResetCooldown();
    }
    
    public void CalculateCooldownTime()
    {
        // 배경이 스크롤 되는동안은 스킬 쿨타임이 감소되지 않게 하기
        if (GameManager.I.backHandler.isScrolling)
        {
            return;
        }
        
        cooldownTime -= Time.deltaTime;
    }

    public bool IsUseSkill()
    {
        if (cooldownTime > 0)
        {
            return false;
        }

        if (Owner.characterTarget.GetTarget() is null)
        {
            return false;
        }
        
        return true;
    }

    public void Execute()
    {
        ResourceHandler.I.InstantiateSkillPrefab(this);
    }
    
    public void ResetCooldown()
    {
        cooldownTime = Data.CooldownTime;
    }
}
