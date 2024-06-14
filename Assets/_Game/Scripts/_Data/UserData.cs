using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    private Dictionary<string, object> _dict;

    public Dictionary<string, object> Dict { get => _dict; set => _dict = value; }

    public UserData()
    {
        Dict = new Dictionary<string, object>();
    }

    public T GetData<T>(string dataKey, T defaultValue)
    {
        var got = Dict.TryGetValue(dataKey, out var obj);
        if (got)
        {
            return (T)obj;
        }
        else
        {
            SetData(dataKey, defaultValue);
            return defaultValue;
        }
    }

    public void SetData(string key, object data)
    {
        Dict[key] = data;
    }

    public void DeleteData(string key)
    {
        Dict.Remove(key);
    }
}