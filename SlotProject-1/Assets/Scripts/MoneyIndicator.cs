using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIndicator : MonoBehaviour
{
    [SerializeField]
    private List<TMPro.TMP_Text> _coinList;



    private void Start()
    {
        DataControl.Instance.OnDataLoaded += Initialize;

    }

    public void Initialize()
    {

        //DataControl.Instance.OnDataLoaded += ChangeCoinText;

        DataControl.Instance.CurrentPlayerData.OnCoinChange += ChangeCoinText;
        ChangeCoinText(DataControl.Instance.CurrentPlayerData.Coins);
    }


    public void ChangeCoinText(float coin)
    {
        foreach (var item in _coinList)
        {
            item.text = (coin).ToString();
        }
    }

}
