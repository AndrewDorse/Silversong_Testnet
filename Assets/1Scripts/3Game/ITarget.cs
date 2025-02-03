using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{

    public Vector3 GetPosition();

    public StatsController GetStatsController();

    public void SetTarget(string targetId);

    public string GetId();

    public bool CanBeAttacked();

    public bool IsAi();

}
