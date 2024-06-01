using CHV;
using UnityEngine;

public class GameSession : ISession
{
    private SessionManager owner;
    
    private static ISession Create() => new GameSession();
    

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterType()
    {
        SessionFactory.I.Register(GameEnums.eSession.Game, Create);
    }


    public void InitializeSession(SessionManager inOwner)
    {
        this.owner = inOwner;
    }

    public void GenerateMonster()
    {
        owner.SessionSpawner.SpawnMonster(DataTable_Character_Data.GetData(20001));
    }
}