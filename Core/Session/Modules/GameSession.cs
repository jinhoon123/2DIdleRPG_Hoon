using CHV;
using UnityEngine;

public class GameSession : ISession
{
    private static ISession Create() => new GameSession();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterType()
    {
        SessionFactory.I.Register(eSession.Game, Create);
    }


    public void SpawnMainCharacter()
    {
        SessionManager.I.SessionSpawner.SpawnMainCharacter(DataTable_Character_Data.GetData(10001));
    }
}