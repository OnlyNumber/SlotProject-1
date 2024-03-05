using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftControl : MonoBehaviour
{

    public ActivePanel activePanel;

    private void Start()
    {
        DataControl.Instance.OnDataLoaded += Initialize; 
    }


    public void Initialize()
    {
        DataControl.Instance.OnNotEnoughCoins += () => activePanel.Activate(true);
    }

    public void GetGift()
    {
        DataControl.Instance.TryChangeCoins(100);
    }


}
