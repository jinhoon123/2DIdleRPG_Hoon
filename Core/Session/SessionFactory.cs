using System;
using System.Collections.Generic;

public class SessionFactory : Singleton<SessionFactory>
{
    private readonly Dictionary<GameEnums.eSession, Func<ISession>> factories = new Dictionary<GameEnums.eSession, Func<ISession>>();
        
    public ISession Create(GameEnums.eSession contentType)
    {
        if (!factories.TryGetValue(contentType, out var factory))
        {
            throw new Exception($"No factory method set for {contentType}");
        }
        
        return factory();
    }

    public void Register(GameEnums.eSession contentType, Func<ISession> factoryMethod)
    {
        factories.TryAdd(contentType, factoryMethod);
    }
}