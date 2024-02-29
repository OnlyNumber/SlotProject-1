using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static T Load<T>(string saveData) where T : new()
    {
        if (PlayerPrefs.HasKey(saveData))
        {
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveData));
        }
        else
        {
            return new T();
        }
    }

    public static void Save<T>(T saveObject, string saveName)
    {
        string transfer = JsonUtility.ToJson(saveObject);

        PlayerPrefs.SetString(saveName, transfer);
    }



}

