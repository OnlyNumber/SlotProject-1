using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private List<SkinButton> _skinList;

    //[SerializeField]
    //private SkinType _skinType;

    [SerializeField]
    private ActivePanel _failPanel;

    [SerializeField]
    private ActivePanel _successPanel;


    [SerializeField]
    private List<Sprite> _backgroundList;

    [SerializeField]
    private Image _background;

    private void Start()
    {
        DataControl.Instance.OnDataLoaded += Initialize;
    }

    public void Initialize()
    {


        
        if (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count < _skinList.Count )
        {
            while (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count != _skinList.Count)
            {
                DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Add(false);
            }
        }
        else if (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count > _skinList.Count)
        {
            while (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count != _skinList.Count)
            {
                DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.RemoveAt(DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count - 1);
            }
        }

        DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList[0] = true;

        _skinList[0].Unlock();

        

        for (int i = 0; i < DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList.Count; i++)
        {
            _skinList[i].Initialize(9999, i);
            _skinList[i].OnBuyAndEquip += BuyAndEquip;
            
            if (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList[i] == true)
            {
            
                _skinList[i].Unlock();

            }


        }

        _skinList[DataControl.Instance.CurrentPlayerData.CurrentBackgroundSkin].Equip(true);

        _background.sprite = _backgroundList[DataControl.Instance.CurrentPlayerData.CurrentBackgroundSkin];


    }

    public void BuyAndEquip(int number, int cost)
    {
            Debug.Log("_player.GetSkinList");

        if (DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList[number] == false)
        {

            if (DataControl.Instance.TryChangeCoins(-cost))
            {
                DataControl.Instance.CurrentPlayerData.ActivatedBackgroundSkinList[number] = true;
                _skinList[number].Unlock();
                //_successPanel.Activate(true);
            }
            else
            {
                //_failPanel.Activate(true);

                return;
            }
        }

        _skinList[DataControl.Instance.CurrentPlayerData.CurrentBackgroundSkin].Equip(false);

        _skinList[number].Equip(true);


        DataControl.Instance.CurrentPlayerData.CurrentBackgroundSkin = number;

        _background.sprite = _backgroundList[DataControl.Instance.CurrentPlayerData.CurrentBackgroundSkin];

    }



}
