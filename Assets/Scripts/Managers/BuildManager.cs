using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private GameObject _turretToBuild;
    public GameObject turretPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _turretToBuild = turretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }
}
