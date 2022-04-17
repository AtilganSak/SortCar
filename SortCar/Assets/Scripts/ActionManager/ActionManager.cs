using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour
{
    static ActionManager _instance;
    public static ActionManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ActionManager>();
                if(_instance == null)
                {
                    _instance = new GameObject("ActionManager").AddComponent<ActionManager>();
                }
            }
            return _instance;
        }
    }

    public bool m_DontDestroy;
    public bool m_DestroyOnSceneChange;

    string m_ActiveScene;

    ADictionary<string,Delegate> actions = new ADictionary<string, Delegate>();

    private void OnEnable()
    {
        if(m_DontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
        if(m_DestroyOnSceneChange)
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        m_ActiveScene = SceneManager.GetActiveScene().name;
    }
    private void OnDestroy()
    {
        if(m_DestroyOnSceneChange)
        {
            SceneManager.activeSceneChanged -= ChangedActiveScene;
        }
    }

    public void AddListener(string _actionName, Action _action)
    {
        if(_actionName == "" || _action == null)
            throw new NullReferenceException();
        CheckAndAddListener(_actionName, _action);
    }
    public void AddListener<T1>(string _actionName, Action<T1> _action)
    {
        if(_actionName == "" || _action == null)
            throw new NullReferenceException();
        CheckAndAddListener(_actionName, _action);
    }
    public void AddListener<T1,T2>(string _actionName, Action<T1, T2> _action)
    {
        if(_actionName == "" || _action == null)
            throw new NullReferenceException();
        CheckAndAddListener(_actionName, _action);
    }
    public void AddListener<T1, T2, T3>(string _actionName, Action<T1, T2, T3> _action)
    {        
        if (_actionName == "" || _action == null)
            throw new NullReferenceException();

        CheckAndAddListener(_actionName, _action);
    }
    public void AddListener<T1, T2, T3, T4>(string _actionName, Action<T1, T2, T3, T4> _action)
    {
        if(_actionName == "" || _action == null)
            throw new NullReferenceException();

        CheckAndAddListener(_actionName, _action);
    }
    public void AddListener<T1, T2, T3, T4, T5>(string _actionName, Action<T1, T2, T3, T4, T5> _action)
    {
        if(_actionName == "" || _action == null)
            throw new NullReferenceException();

        CheckAndAddListener(_actionName, _action);
    }

    public void RemoveListener(string _actionName, Action _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }
    public void RemoveListener<T1>(string _actionName, Action<T1> _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }
    public void RemoveListener<T1, T2>(string _actionName, Action<T1, T2> _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }
    public void RemoveListener<T1, T2, T3>(string _actionName, Action<T1, T2, T3> _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }
    public void RemoveListener<T1, T2, T3, T4>(string _actionName, Action<T1, T2, T3, T4> _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }
    public void RemoveListener<T1, T2, T3, T4, T5>(string _actionName, Action<T1, T2, T3, T4, T5> _action)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        CheckAndRemoveListener(_actionName, _action);
    }

    public void RemoveAction(string _actionName)
    {
        if(_actionName == "")
            throw new NullReferenceException();

        if(actions.ContainsKey(_actionName))
        {
            actions.RemoveKey(_actionName);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Not found " + _actionName + " Action!!.");
#endif
        }
    }

    public void Fire(string _actionName, params object[] _params)
    {
        if (_actionName == "" || _params == null)
            throw new NullReferenceException();

        bool isFound = false;
        foreach (AKeyValuePair<string,Delegate> item in actions)
        {
            if (item.Key == _actionName)
            {
                for (int i = 0; i < item.Values.Count; i++)
                {
                    if(item.Values[i] != null)
                    {
                        if(item.Values[i].Method.GetParameters().Length == _params.Length)
                            item.Values[i].DynamicInvoke(_params);
                        else
                        {
#if UNITY_EDITOR
                            Debug.LogError("The number of parameters sent and the number of parameters of the procedure are not the same. " + "\n" +
                                "Click to details!!" + "\n" +
                                "\n Method Name: " + item.Values[i].Method.Name + "\n " +
                                "Target method parameters count: " + item.Values[i].Method.GetParameters().Length.ToString() + "\n" +
                                "Want to sent parameters count: " + _params.Length.ToString());
#endif
                        }
                    }
                }
                isFound = true;
            }
        }
        try
        {       
            if (!isFound)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Not found " + _actionName + " Action!!.");
#endif
            }
        }
        catch (Exception ee)
        {
#if UNITY_EDITOR
            Debug.LogError(ee.Message + "\n Fire Method \n " + _actionName);
#endif            
        }
    }

    void CheckAndAddListener(string _actionName, Delegate _del)
    {
        if(!actions.ContainsKey(_actionName))
        {
            actions.AddKey(_actionName, _del);
        }
        else
        {
            if(!actions.ContainsValue(_actionName, _del))
                actions.AddValue(_actionName, _del);
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning(_del.Method.Name + " Listener is already exists!!. \n => Action: " + _actionName);
#endif
            }
        }
    }
    void CheckAndRemoveListener(string _actionName, Delegate _del)
    {
        if(actions.ContainsKey(_actionName))
        {
            actions.RemoveValue(_actionName, _del);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Not found " + _actionName + " Action!!.");
#endif
        }
    }
    void ChangedActiveScene(Scene _scene1, Scene _scene2)
    {
        if(_scene2.name != m_ActiveScene)
        {
            if(m_DestroyOnSceneChange)
            {
                Destroy(gameObject);
            }
        }
    }
}

public class ADictionary<T1, T2> : IEnumerable
{    
    public List<AKeyValuePair<T1,T2>> Pairs = new List<AKeyValuePair<T1, T2>>();

    public bool AddKey(T1 key,T2 value)
    {
        if (!ContainsKey(key))
        {
            AKeyValuePair<T1, T2> newKeyValue = new AKeyValuePair<T1, T2>();
            newKeyValue.Key = key;
            newKeyValue.Values = new List<T2>();
            newKeyValue.Values.Add(value);
            Pairs.Add(newKeyValue);
            return true;
        }        
        return false;
    }
    public bool AddValue(T1 toKey, T2 value)
    {
        for (int i = 0; i < Pairs.Count; i++)
        {
            if (Pairs[i].Key.Equals(toKey))
            {
                if (Pairs[i].Values != null)
                {
                    if (!Pairs[i].Values.Contains(value))
                    {
                        Pairs[i].Values.Add(value);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool RemoveKey(T1 key)
    {
        for (int i = 0; i < Pairs.Count; i++)
        {
            if (Pairs[i].Key.Equals(key))
            {
                Pairs.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    public bool RemoveValue(T2 value)
    {
        bool rs = false;
        for (int i = 0; i < Pairs.Count; i++)
        {
            for (int k = 0; k < Pairs[i].Values.Count; k++)
            {
                if (Pairs[i].Values[k].Equals(value))
                {
                    Pairs[i].Values.RemoveAt(k);
                    rs = true;
                }
            }
        }
        return rs;
    }
    public bool RemoveValue(T1 targetKey, T2 value)
    {
        for(int i = 0; i < Pairs.Count; i++)
        {
            if(Pairs[i].Key.Equals(targetKey))
            {
                if(Pairs[i].Values != null)
                {
                    if(Pairs[i].Values.Contains(value))
                    {
                        Pairs[i].Values.Remove(value);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ContainsKey(T1 key)
    {
        for (int i = 0; i < Pairs.Count; i++)
        {
            if (Pairs[i].Key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
    public bool ContainsValue(T1 targetKey,T2 value)
    {
        for (int i = 0; i < Pairs.Count; i++)
        {
            if (Pairs[i].Key.Equals(targetKey))
            {
                for (int k = 0; k < Pairs[i].Values.Count; k++)
                {
                    if (Pairs[i].Values[k].Equals(value))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public int KeyCount() => Pairs.Count;
    public int ValueCount()
    {
        int totalValueCount = 0;
        for(int i = 0; i < Pairs.Count; i++)
        {
            if(Pairs[i].Key != null)
            {
                if(Pairs[i].Values != null)
                {
                    totalValueCount += Pairs[i].Values.Count;
                }
            }
        }
        return totalValueCount;
    }
    public int ValueCount(T1 key)
    {
        int totalValueCount = 0;
        for(int i = 0; i < Pairs.Count; i++)
        {
            if(Pairs[i].Key.Equals(key))
            {
                if(Pairs[i].Values != null)
                {
                    totalValueCount += Pairs[i].Values.Count;
                }
            }
        }
        return totalValueCount;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.Pairs.GetEnumerator();
    }   
}
[System.Serializable]
public struct AKeyValuePair<T1, T2>
{
    public T1 Key;
    public List<T2> Values;
}
