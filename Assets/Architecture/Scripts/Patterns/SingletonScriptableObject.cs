using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPGSkills.Architecture
{
    public abstract class SingletonScriptableObject : ScriptableObject { }
    
    public abstract class SingletonScriptableObject<T> : SingletonScriptableObject
        where T : SingletonScriptableObject<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                    
#if UNITY_EDITOR
				var type = typeof(T);
				var paths = AssetDatabase.GetAllAssetPaths();
				var path  = paths.FirstOrDefault(p => p.EndsWith(type.Name + ".asset"));
				_instance = (T)AssetDatabase.LoadAssetAtPath(path, type);
#else
				_instance = Resources.LoadAll<T>("").First();
#endif
                return _instance;
            }
        }
       
    }
}

