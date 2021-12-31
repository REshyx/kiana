using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*xbb
 * 单例模板
 * */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool applicationQuit = false;

    public static T Instance
    {
        get
        {
            if (applicationQuit)
            {
                return null;
            }

            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T) + " Singleton";
                        _instance = obj.AddComponent<T>();
                    }
                }
            }

            return _instance;
        }
    }

    public void OnApplicationQuit()
    {
        applicationQuit = true;
    }
}
