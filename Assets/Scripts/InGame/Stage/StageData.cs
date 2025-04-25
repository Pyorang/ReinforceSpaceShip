using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField] private List<int> patternNum;

    public List<int> GetPatternList()
    {
        return patternNum;
    }
}
