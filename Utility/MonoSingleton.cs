using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static readonly object Lock = new object();
        
    public static T I
    {
        get
        {
            lock (Lock)
            {
                if (instance != null)
                {
                    return instance;
                }
                
                var objs = FindObjectsOfType<T>();

                if (objs.Length > 0)
                {
                    instance = objs[0];
                }

                if (instance != null)
                {
                    return instance;
                }
                    
                var objectName = typeof(T).ToString();
                var obj = GameObject.Find(objectName);
                    
                if (obj == null)
                {
                    obj = new GameObject(objectName);
                }
                    
                instance = obj.AddComponent<T>();

                return instance;
            }
        }
    }
}