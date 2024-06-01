using System.Collections.Generic;
using CHV;

public class Stat
{
    public readonly DataTable_Stat_Data.eStatType StatType;
    
    private readonly Unified baseValue;
    private readonly List<IStatModifier> modifiers = new();
    private readonly Operation operation = new();
    
    public Stat(DataTable_Stat_Data.eStatType type, Unified baseValue)
    {
        StatType = type;
        this.baseValue = baseValue;
    }
    
    public Unified GetValue()
    {
        // 스탯의 기본값을 적용하고 multiply 값을 0으로 초기화 합니다.
        var addValue = baseValue;
        var multiplyValue = new Unified(0);
            
        foreach (var modifier in modifiers)
        {
            addValue = operation.Add(addValue, modifier.GetAddValue());
            multiplyValue = operation.Add(multiplyValue, modifier.GetMultiplyValue());
        }
        
        // Percentage가 0일 경우 최소 1을 보장해야 finalValue의 값 손실이 없습니다.
        multiplyValue = operation.Max(multiplyValue, operation.One);
            
        // 수치 누적과 퍼센테이지 누적을 곱해서 스탯의 최종값을 반환 합니다.
        return operation.Multiply(addValue, multiplyValue);
    }
    
}   