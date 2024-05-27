using System;
using System.Collections.Generic;
using CHV;

public class SkillFactory
{
    private readonly Dictionary<DataTable_Skill_Data.eKind, Action> skills;

    public SkillFactory(Skill skill)
    {
        skills = new Dictionary<DataTable_Skill_Data.eKind, Action>
        {
            { DataTable_Skill_Data.eKind.Projectile, () => ResourceHandler.I.InstantiateSkillPrefabAsync<SkillProjectile>(skill) },
            { DataTable_Skill_Data.eKind.Target, () => ResourceHandler.I.InstantiateSkillPrefabAsync<SkillTarget>(skill) }
        };
    }

    public void Create(DataTable_Skill_Data.eKind kind)
    {
        if (skills.TryGetValue(kind, out var skill))
        {
            skill();
        }
        else
        {
            throw new NotSupportedException($"{kind} is not supported.");
        }
    }
}