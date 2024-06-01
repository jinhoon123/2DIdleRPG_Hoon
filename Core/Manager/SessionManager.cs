using CHV;

public class SessionManager : Singleton<SessionManager>
{
    private ISession session;
    
    public SessionSpawner SessionSpawner;
    public SessionEventManager SessionEventManager;

    public void Initialize()
    {
        SessionEventManager = new SessionEventManager(this);
        
        SessionSpawner = new SessionSpawner(this);
        SessionSpawner.SpawnMainCharacter(DataTable_Character_Data.GetData(GameConstants.MAIN_CHARACTER_DATA_ID));
    }
    
    public void RequestSession(GameEnums.eSession sessionType)
    {
        session = null;
        session = SessionFactory.I.Create(sessionType);
        session.InitializeSession(this);
        
        session.GenerateMonster();
    }
}
