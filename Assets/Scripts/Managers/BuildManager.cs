using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private TowerBase _turretToBuild;

    private FloorTile _selectedTileWithTurret;

    public FloorTileUI floorTileUI;
    
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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectTileWithTurret(FloorTile tile)
    {
        if (_selectedTileWithTurret == tile)
        {
            DeselectTile();
            return;
        }
        
        _selectedTileWithTurret = tile;
        _turretToBuild = null;
        floorTileUI.SetTarget(tile);
    }

    public void DeselectTile()
    {
        _selectedTileWithTurret = null;
        floorTileUI.Hide();
    }
    
    public void SetTurretToBuild(TowerBase turret)
    {
        _turretToBuild = turret;
        DeselectTile();
    }

    public TowerBase GetTurretTobuild()
    {
        return _turretToBuild;
    }

    public static Vector3 TurretPlacementPosition(FloorTile floorTile)
    {
        return floorTile.transform.position + new Vector3(0f, 2f, 0f);
    }
}
