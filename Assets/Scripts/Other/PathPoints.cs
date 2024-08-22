using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public static PathPoints Singleton { get; private set; }
    [Header("GREEN")]
    [SerializeField] private GameObject[] _greenUnitsPath, _greenMinerPath, _greenWoodcutterPath;
    [Header("RED")]
    [SerializeField] private GameObject[] _redUnitsPath, _redMinerPath, _redWoodcutterPath;

    private void Awake()
    {
        Singleton = this;
    }

    public GameObject[] GetGreenTeamUnitPoints() => _greenUnitsPath;

    public GameObject[] GetGreenMinerPath() => _greenMinerPath;

    public GameObject[] GetGreenWoodcutterPath() => _greenWoodcutterPath;

    public GameObject[] GetRedTeamUnitPoints() => _redUnitsPath;

    public GameObject[] GetRedWoodcutterPath() => _redWoodcutterPath;

    public GameObject[] GetRedMinerPath() => _redMinerPath;
}