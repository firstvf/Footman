using System.Collections.Generic;
using UnityEngine;

public class RedTeamUnitsList : MonoBehaviour
{
    public static RedTeamUnitsList Singleton { get; private set; }
    public List<RedTeam> RedTeamList;

    private void Awake()
    {
        Singleton = this;
    }
}