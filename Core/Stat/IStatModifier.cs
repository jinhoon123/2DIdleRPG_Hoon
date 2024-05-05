using CHV;

public interface IStatModifier
{
    public void Init(DataTable_Stat_Data.eStatType inStat);
    public Unified GetAddValue();
    public Unified GetMultiplyValue();
}