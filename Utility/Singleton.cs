public class Singleton<T> where T : class, new()
{
    private static T Instance;

    public static T I
    {
        get
        {
            if (Instance == null)
            {
                Instance = new T();
            }
            return Instance;
        }
    }
}
