using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SkinButton : MonoBehaviour
{
    [SerializeField]
    private int _skinNumber;

    //[SerializeField]
    //private SwitchSprite _skinIcon;

    [SerializeField]
    private GameObject _equipImage;

    [SerializeField]
    private GameObject _costGO;

    [SerializeField]
    private TMP_Text _costText;

    [SerializeField]
    private Button _buyButton;

    public Action<int, int> OnBuyAndEquip;

    [SerializeField]
    private int _cost;

    [SerializeField]
    private ValueType _type;

    private void Start()
    {
        _costText.text = _cost.ToString();
    }

    public void Initialize(int currentPlayerLevel, int number)
    {
        _skinNumber = number; 

       // if (_needLvl <= currentPlayerLevel)
       // {
            _buyButton.enabled = true;
            //_needLvlvGO.SetActive(false);

       // }
        /*else
        {
            _buyButton.enabled = false;
            _needLvlvGO.SetActive(true);
            _needLvlvGO.GetComponentInChildren<TMP_Text>().text = "Lv" + _needLvl;
        }*/
    }


    public void BuyAndEquip()
    {
        OnBuyAndEquip?.Invoke(_skinNumber, _cost);
    }

    public void Unlock()
    {
        //_skinIcon?.SwitchSpriteMethod(true);
        _costGO.SetActive(false);

    }

    public void Equip(bool equipState)
    {
        _equipImage.SetActive(equipState);
    }



}
