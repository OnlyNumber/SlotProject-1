using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCoeficientCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject _circle;

    [SerializeField]
    private TMPro.TMP_Text _circleText;

    [SerializeField]
    private float _speed;

    private float _coeficient = 1;

    [SerializeField]
    private float _bounds;

    public System.Action OnUpdate;

    private bool _isAdding;

    public ActivePanel WinPanel;

    public ActivePanel LosePanel;

    public TMPro.TMP_Text _rewardText;

    public RollReward rollReward;

    private void Start()
    {
        //Initialize();
    }

    public void Initialize()
    {
        _bounds = Random.Range(1.1f, 4);
        if (Application.isMobilePlatform)
        {
            OnUpdate += GamePhone;
        }
        else
        {
            OnUpdate += GamePC;
        }
        _coeficient = 1;
        _circleText.text = "1";
        OnUpdate += Adding;
        stop = true;
    }


    private void Update()
    {
        OnUpdate?.Invoke();
    }

    public void GameAction(System.Func<bool> OnDown, System.Func<bool> OnUp)
    {
        if (OnDown())
        {
            _isAdding = true;
        }
        else if (OnUp() && stop)
        {
            _isAdding = false;
            RemoveOnUpdate();
            Win();
        }
    }

    bool stop ;

    public void Adding()
    {
        if(_isAdding)
        {
            _coeficient += _speed * Time.deltaTime;

            if (_coeficient > _bounds)
            {
                stop = false;
                RemoveOnUpdate();
                _isAdding = false;
                Lose();
            }
            _circle.transform.localScale = Vector3.one * _coeficient;
            _circleText.text = System.Math.Round(_coeficient, 2).ToString();
        }
    }




    public void RemoveOnUpdate()
    {
        if (Application.isMobilePlatform)
        {
            OnUpdate -= GamePhone;
        }
        else
        {
            OnUpdate -= GamePC;
        }

        OnUpdate -= Adding;

        //OnUpdate = new System.Action();

    }

    public void GamePhone()
    {
        GameAction(() => Input.GetTouch(0).phase == TouchPhase.Began, GetPhoneUp);
    }

    public void GamePC()
    {
        GameAction(() => Input.GetMouseButtonDown(0), () => Input.GetMouseButtonUp(0));
    }


    public bool GetPhoneUp()
    {
        return Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    public void Win()
    {
        float reward;

        Debug.Log("Win");

        WinPanel.Activate(true);
        reward = rollReward.Reward * _coeficient;

        reward = (float)System.Math.Round(reward, 2);

        _rewardText.text = reward.ToString();
        DataControl.Instance.TryChangeCoins(reward);
        //RemoveOnUpdate();

    }

    public void Lose()
    {
        LosePanel.Activate(true);
    }

}
