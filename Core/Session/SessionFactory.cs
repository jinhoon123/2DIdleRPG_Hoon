using System;
using System.Collections.Generic;

public class SessionFactory : Singleton<SessionFactory>
{
    private readonly Dictionary<eSession, Func<ISession>> factories = new Dictionary<eSession, Func<ISession>>();
        
    public ISession Create(eSession contentType)
    {
        if (!factories.TryGetValue(contentType, out var factory))
        {
            throw new Exception($"No factory method set for {contentType}");
        }
        
        return factory();
    }

    public void Register(eSession contentType, Func<ISession> factoryMethod)
    {
        factories[contentType] = factoryMethod;
    }
}