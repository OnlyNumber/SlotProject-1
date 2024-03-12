using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SpinManager : MonoBehaviour
{
    [SerializeField]
    private float RotatePower;

    [SerializeField]
    private float StopPower;

    [SerializeField]
    private Rigidbody2D _rb;

    public List<Action> Rewards = new List<Action>();

    private float _angle;

    [SerializeField]
    private float _angleOffset;

    [SerializeField]
    private RectTransform _rectTransform;

    private bool _isRolling;

    [SerializeField]
    private Button _freeButton;

    [SerializeField]
    private Button _goToDailyBonusButton;

    private int _reward;

    [SerializeField]
    private ActivePanel _dailyPanel;
    [SerializeField]
    private ActivePanel _mainPanel;

    private DateTime _checkTime;

    private void Start()
    {
        DataControl.Instance.OnDataLoaded += Initialize;

    }

    public void Initialize()
    {
        //Debug.Log(DataControl.Instance.GetDate());

        _checkTime = DataControl.Instance.GetDate();

        _checkTime = _checkTime.AddMinutes(5);

        Debug.Log(_checkTime);
        
        if (DateTime.Compare(_checkTime, DateTime.Now) >= 0/* && DataControl.Instance.CurrentData.Spin <= 0*/)
        {
            _freeButton.interactable = false;
        }

        Rewards.Add(() => DataControl.Instance.TryChangeCoins(20));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(25));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(30));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(35));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(40));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(45));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(50));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(100));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(150));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(5));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(10));
        Rewards.Add(() => DataControl.Instance.TryChangeCoins(15));
        

        _angle = 360 / Rewards.Count;

    }

    private void Update()
    {
        if (Mathf.Abs(_rb.angularVelocity) > 1)
        {

            if (Mathf.Abs(_rb.angularVelocity) > 400)
            {
                _rb.angularVelocity -= Time.deltaTime * StopPower;

                _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, 0, 1600);
                _isRolling = true;
            }
            else
            {
                float _rotationAngle = _rectTransform.rotation.eulerAngles.z;

                if (_rotationAngle < 0)
                {
                    _rotationAngle += 360;
                }

                if ((_angle) * _reward + _angleOffset < _rotationAngle && _angleOffset + (_angle) * (_reward + 1) > _rotationAngle)
                {
                    _rb.angularVelocity -= Time.deltaTime * StopPower * 20000;

                    _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, 0, 1600);
                    _isRolling = true;

                }
            }
        }

        if (_rb.angularVelocity == 0 && _isRolling)
        {

            _isRolling = false;
            _freeButton.interactable = false;
            //_goToDailyBonusButton.interactable = false;


            StartCoroutine(DelayBeforeExit());



        }

        if (DateTime.Compare(_checkTime, DateTime.Now) <= 0)
        {
            _freeButton.interactable = true;
        }

    }

    public IEnumerator DelayBeforeExit()
    {

        GetReward();
        DataControl.Instance.SetDate(DateTime.Now);
        _freeButton.interactable = false;

        yield return new WaitForSeconds(1);


        _dailyPanel.Activate(false);
        _mainPanel.Activate(true);

        //_dailyPanel.ActivateNextScene(_mainPanel.gameObject);


    }


    private void GetReward()
    {
        if (Rewards[_reward] != null)
        {
            Rewards[_reward]?.Invoke();
            Debug.Log("Reward GetReward " + _reward);
        }
        else
        {
            Debug.Log("Reward count " + Rewards.Count);
        }



        if (DateTime.Compare(DataControl.Instance.GetDate(), DateTime.Today) >= 0 /*&& DataControl.Instance.CurrentData.Spin <= 0*/)
        {
            _freeButton.interactable = false;
        }

    }

    public void StartFreeRoll()
    {
        if (_isRolling)
        {
            return;
        }


        _freeButton.interactable = false;

        StartRotate();

        


    }

    /*public void StartPayRoll(int cost)
    {
        if (_isRolling)
        {
            return;
        }

        if (DataControl.Instance.TryChangeCoins(ValueType.coin, -cost))
        {
            StartRotate();
        }
    }*/

    public void StartRotate()
    {
        _reward = UnityEngine.Random.Range(0, Rewards.Count);

        Debug.Log("Reward random " + _reward);

        transform.rotation = Quaternion.identity;

        _rb.AddTorque(RotatePower);


    }
    




}
