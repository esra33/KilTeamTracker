using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalManager", menuName = "Manager/GlobalManager")]
public class Managers : ScriptableObject
{
    private static Managers m_instance = null;
    public static Managers instance
    {
        get { return m_instance; }
        private set
        {
            if (m_instance == null)
            {
                m_instance = value;
            }
        }
    }

    [SerializeField] private List<ScriptableManager> m_ScriptableManagers = null;
    [SerializeField] private List<MonoBehaviorManager> m_MonoBehaviorManagers = null;

    private Dictionary<Type, object> m_ManagersDictionary = null;

    public T GetManager<T>()
    {
        Type type = typeof(T);
        T target = default(T);
        object result = GetManager(type);
        if(result != null)
        {
            target = (T)result;
        }

        return target;
    }

    public object GetManager(Type type)
    {
        object target = null;
        if (m_ManagersDictionary.ContainsKey(type))
        {
            target = m_ManagersDictionary[type];
        }
        else
        {
            foreach (KeyValuePair<Type, object> managerPair in m_ManagersDictionary)
            {
                if (type.IsAssignableFrom(managerPair.Key))
                {
                    target = managerPair.Value;
                }
            }
        }

        return target;
    }

    private void OnEnable()
    {
        instance = this;
        Initialize();
        ListenForChangePlayMode();
    }

    [ContextMenu("Force Initialize")]
    private void Initialize()
    {
        m_ManagersDictionary = new Dictionary<Type, object>();
        if (m_ScriptableManagers == null)
        {
            return;
        }

        AddManagers<ScriptableManager>(m_ScriptableManagers);

        foreach (MonoBehaviorManager manager in m_MonoBehaviorManagers)
        {
            if (!m_ManagersDictionary.ContainsKey(manager.GetType()))
            {
                UnityEngine.Object target;
                if (Application.isPlaying)
                {
                    target = Instantiate(manager);
                    DontDestroyOnLoad(target);
                }
                else
                {
                    target = manager;
                }

                m_ManagersDictionary[manager.GetType()] = target;
            }
        }

        foreach (KeyValuePair<Type, object> managerPair in m_ManagersDictionary)
        {
            IManager manager = managerPair.Value as IManager;
            if (manager != null)
            {
                manager.Initialize();
            }
        }
    }

    private void AddManagers<T>(List<T> managers) where T : IManager
    {
        foreach (T manager in managers)
        {
            m_ManagersDictionary[manager.GetType()] = manager;
        }
    }

	public void Inject(object target)
	{
        FieldInfo[] fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance |BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Static);
        foreach(FieldInfo field in fields)
        {
            foreach(object attribute in field.GetCustomAttributes(true))
            {
                InjectableAttribute injectableAttribute = attribute as InjectableAttribute;
                if(injectableAttribute == null)
                {
                    continue;
                }

                Type targetType = field.FieldType;
                object targetValue = GetManager(targetType);
                if(targetValue == null)
                {
                    continue;
                }

                field.SetValue(target, targetValue);
            }
        }
	}

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void ListenForChangePlayMode()
    {
        UnityEditor.EditorApplication.playModeStateChanged += RefreshManagers;
    }

#if UNITY_EDITOR
    private void RefreshManagers(UnityEditor.PlayModeStateChange state)
    {
        Initialize();
    }
#endif
}
