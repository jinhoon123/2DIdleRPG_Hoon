public class SessionEventManager
{
    private readonly SessionManager owner;
    
    public SessionEventManager(SessionManager inOwner)
    {
        this.owner = inOwner;
    }
    
    public void OnMonsterDead()
    {
        if (owner.SessionSpawner.Monsters.Count == 0)
        {
            GameManager.I.backHandler.OnScrolling();
            GameManager.I.groundHandler.OnScrolling();
        }
    }
}