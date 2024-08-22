using System.Collections.Generic;
using UnityEngine;

public class GreenTeamUnitsList : MonoBehaviour
{
    public static GreenTeamUnitsList Singleton { get; private set; }
    public List<GreenTeam> GreenTeamList;

    private void Awake()
    {
        Singleton = this;
    }
}