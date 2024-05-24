using Cysharp.Threading.Tasks;

public enum eSession
{
    // 스테이지
    Game = 0,
    
    // 던전
    Dungeon = 1
}
public class SessionManager : Singleton<SessionManager>
{
    private ISession session;
    
    public SessionSpawner SessionSpawner;

    public void Initialize()
    {
        SessionSpawner = new SessionSpawner();
    }
    
    public void RequestSession(eSession sessionType)
    {
        session = SessionFactory.I.Create(sessionType);
        session.SpawnMainCharacter();
    }
}
