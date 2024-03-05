using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollReward : MonoBehaviour
{
    [SerializeField]
    private List<RollControl> _rollControls;

    [SerializeField]
    private int _amountOfChoices;

    [SerializeField]
    private RewardsData _rewards;

    [SerializeField]
    public float Bet;

    public float Reward; 

    [SerializeField]
    private TMPro.TMP_Text _betText;

    [SerializeField]
    private TMPro.TMP_Text _rewardText;

    public ActivePanel WinPanel;

    public ActivePanel LosePanel;

    public ActivePanel CircleGamePanel;

    public IncreaseCoeficientCircle coeficientCircle;

    [SerializeField]
    private float _startXPostion;

    private void Start()
    {
        float x = -_startXPostion;

        foreach (var item in _rollControls)
        {
            item.Initialize(x);
            x += _startXPostion;
        }
    }

    public void StartRoll()
    {
        if (Bet == 0 || !DataControl.Instance.TryChangeCoins(-Bet)) 
        {
            return;
        }

        //DataControl.Instance.TryChangeCoins(-Bet);



        StartCoroutine(DelayStart());
        StartCoroutine(DelayBeforeStop());
    }

    private IEnumerator DelayStart()
    {
        foreach (var item in _rollControls)
        {
            item.IsStopping = false;

            item.Roll(true);
            yield return new WaitForSeconds(0.5f);

        }
    }

    private IEnumerator DelayBeforeStop()
    {
        yield return new WaitForSeconds(2);

        foreach (var item in _rollControls)
        {
            yield return new WaitForSeconds(1);

            int stoppedElement = Random.Range(0, _amountOfChoices);
            item.IndexOfStoppedObject = stoppedElement;
            item.IsStopping = true;

        }

        GetReward();

    }

    public void ChangeBet(float changeAmount)
    {
        if(Bet + changeAmount < 0)
        {
            return;
        }

        Bet += changeAmount;
        _betText.text = Bet.ToString();
    }

     

    private void GetReward()
    {
        float rewardCoef = 0;

        
        FindReward();

        if(maxAmount == 1)
        {
            LosePanel.Activate(true);
            return;
        }

        if(maxAmount == 2)
        {
            rewardCoef = _rewards.RewardsForCombo[maxCheckIndex].Reward2X;
        }
        else if (maxAmount == 3)
        {
            rewardCoef = _rewards.RewardsForCombo[maxCheckIndex].Reward3X;

        }

        Reward = Bet * rewardCoef;

        _rewardText.text = Reward.ToString();

        WinPanel.Activate(true);


        //coeficientCircle.Initialize();


        //DataControl.Instance.TryChangeCoins(bet * rewardCoef);

    }

    public void StartNextGame()
    {
        StartCoroutine(Delay());
        CircleGamePanel.Activate(true);
        WinPanel.Activate(false);
    
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0);
        coeficientCircle.Initialize();

    }


    public void CloseGame()
    {
        //CircleGamePanel.Activate(false);
    }


    private int maxAmount = 0;
    private int maxCheckIndex = 0;


    private void FindReward()
    {

        int currentCheckIndex;
        int currentAmountSimmilar = 0;
        maxAmount = 0;
        maxCheckIndex = 0;


        foreach (var item in _rollControls)
        {
            currentCheckIndex = item.IndexOfStoppedObject;
            currentAmountSimmilar = 0;

            Debug.Log("Index: " + currentCheckIndex);

            foreach (var checkItem in _rollControls)
            {

                if (checkItem.IndexOfStoppedObject == currentCheckIndex)
                {
                    currentAmountSimmilar++;
                }


            }

            if(currentAmountSimmilar > maxAmount)
            {
                maxAmount = currentAmountSimmilar;
                maxCheckIndex = currentCheckIndex;
            }


        }


    }

}
