using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsData")]
public class RewardsData : ScriptableObject
{
    public List<RewardForCombo> RewardsForCombo;
}

[System.Serializable]
public class RewardForCombo
{
    public float Reward2X;
    public float Reward3X;

}