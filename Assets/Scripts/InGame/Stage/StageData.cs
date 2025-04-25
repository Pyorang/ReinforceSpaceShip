using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [Header("Boss Status")]
    [Space]
    [SerializeField] private int BossMaxHP;
    [Header("Pattern Create")]
    [Space]
    [SerializeField] private List<int> patternNum;

    public List<int> GetPatternList()
    {
        return patternNum;
    }

    public int GetBossMaxHP() { return  BossMaxHP; }
}
