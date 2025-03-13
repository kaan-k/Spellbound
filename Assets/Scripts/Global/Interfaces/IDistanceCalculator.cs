using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IDistanceCalculator
{
    public float CalculateDistance(UnityEngine.Transform from, UnityEngine.Transform to);
}
