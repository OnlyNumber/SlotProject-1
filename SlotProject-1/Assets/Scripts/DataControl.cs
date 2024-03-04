using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataControl : MonoBehaviour
{
    public PlayerData CurrentPlayerData;
    public System.Action OnDataLoaded;

    public static DataControl Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        CurrentPlayerData = SaveManager.Load<PlayerData>(StaticFields.PLAYER_DATA);
        OnDataLoaded?.Invoke();
    }


    private void OnApplicationQuit()
    {
        SaveManager.Save(CurrentPlayerData, StaticFields.PLAYER_DATA);
    }

    public bool TryChangeCoins(float changeAmount)
    {
        if(changeAmount + CurrentPlayerData.Coins >= 0)
        {

            CurrentPlayerData.Coins += changeAmount;
            return true;
        }

        return false;
    }

    public float GetCoint()
    {
        return CurrentPlayerData.Coins;
    }

}

[Serializable]
public class PlayerData
{
    public System.Action<float> OnCoinChange;

    [SerializeField]
    private float _coins;

    public float Coins
    {
        set
        {
            _coins = value;

            OnCoinChange?.Invoke(_coins);
        }

        get
        {
            return _coins;
        }

    }

    public int VolumeMusic;

    public int VolumeClip;

    public PlayerData()
    {
        VolumeMusic = 1;
        VolumeClip = 1;
        Coins = 200;
    }

}