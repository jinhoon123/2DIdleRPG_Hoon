using System.Collections.Generic;
using System.Linq;
using CHV;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    private Character owner;
    private readonly List<Stat> stats = new();
    
    public void Init(Character inOwner)
    {
        owner = inOwner;
        
        InitializeStat();
    }

    private void InitializeStat()
    {
        var dataCount = DataTable_Stat_Data.GetQuantVarsValues();

        for (var i = 0; i < dataCount; i++)
        {
            var data = DataTable_Stat_Data.GetVar(i);
            var defaultValue = 0.0f;
         
            if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
            {
                defaultValue = data.PlyaerDefault;
            }
            
            if (owner.Data.CharacterType == DataTable_Character_Data.eCharacterType.Monster)
            {
                defaultValue = data.MonsterDefault;
            }

            var stat = new Stat(data.StatType, new Unified(defaultValue));
            stats.Add(stat);
        }
    }

    public Unified GetStat(DataTable_Stat_Data.eStatType type)
    {
        foreach (var stat in stats.Where(stat => stat.StatType == type))
        {
            return stat.GetValue();
        }
        
        return new Unified(0);
    }
}
