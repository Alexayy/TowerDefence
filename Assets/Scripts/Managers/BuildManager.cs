using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private TowerBase _turretToBuild;

    public bool CanBuild => _turretToBuild != null;

    [Header("Turret types")]
    public TowerBase turretBasillica;
    public TowerBase turretBay;
    public TowerBase turretFurnace;
    public TowerBase turretPitter;
    public TowerBase turretHunter;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetTurretToBuild(TowerBase turret)
    {
        _turretToBuild = turret;
    }

    public void BuildTurretOn(FloorTile floorTile)
    {
        if (GameManager.Currency < _turretToBuild.Cost)
        {
            Debug.Log("Not enough mana for that!");
            return;
        }

        GameManager.Currency -= _turretToBuild.Cost;
        GameObject turret = Instantiate(_turretToBuild.gameObject, TurretPlacementPosition(floorTile), Quaternion.identity);
        floorTile.currentTurret = turret;
    }

    private static Vector3 TurretPlacementPosition(FloorTile floorTile)
    {
        return floorTile.transform.position + new Vector3(0f, 2f, 0f);
    }
}
