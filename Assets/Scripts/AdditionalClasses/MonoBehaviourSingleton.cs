using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    private static T s_instance;
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                    s_instance = objs[0];

                if (objs.Length > 1)
                {
                    s_instance = objs[0];
                    foreach (var obj in objs)
                    {
                        if (s_instance == obj)
                            continue;
                        Destroy(obj);
                    }
                }
                if (s_instance == null)
                {
                    UnityEngine.Debug.Log("New gameobject");
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    s_instance = obj.AddComponent<T>();
                }
            }
            return s_instance;
        }
    }
}
