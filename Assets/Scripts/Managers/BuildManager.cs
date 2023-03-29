using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private GameObject _turretToBuild;
    
    [Header("Turret types")]
    public GameObject turretBasillica;
    public GameObject turretBay;
    public GameObject turretFurnace;
    public GameObject turretPitter;
    public GameObject turretHunter;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        _turretToBuild = turret;
    }
}
