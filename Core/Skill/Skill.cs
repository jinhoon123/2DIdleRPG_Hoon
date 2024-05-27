using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Skill
{
    public Character Owner; // 스킬 시전자

    public SkillAsset Asset;
    public DataTable_Skill_Data Data; // 스킬 데이터

    private SkillFactory skillFactory;
    
    private float cooldownTime;
    
    public void Init(Character character, int skillID)
    {
        Owner = character;
        Data = DataTable_Skill_Data.GetData(skillID);
        
        ResourceHandler.I.InstantiateAssetAsync<SkillAsset>("Skill" + Data.TID.ToString(), (result) => Asset = result);
        skillFactory = new SkillFactory(this);

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
        skillFactory.Create(Data.Kind);
    }
    
    public void ResetCooldown()
    {
        cooldownTime = Data.CooldownTime;
    }
}
