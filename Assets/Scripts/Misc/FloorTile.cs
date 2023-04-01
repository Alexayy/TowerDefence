using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [FormerlySerializedAs("_currentTurret")] public GameObject currentTurret;
    public TowerBase towerBase;
    
    private Renderer _renderer;
    private Color _defaultColor;
    private BuildManager _buildManager;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        _buildManager = BuildManager.Instance;
    }

    #region Turret Functionalities on the Tile

    void BuildTurret(TowerBase towerBase)
    {
        if (GameManager.Currency < towerBase.Cost)
        {
            Debug.Log("Not enough mana for that!");
            return;
        }

        GameManager.Currency -= towerBase.Cost;
        GameObject turretGo = Instantiate(towerBase.gameObject, BuildManager.TurretPlacementPosition(this), Quaternion.identity);
        SFXManager.Instance.PlaySound(towerBase.placedTurretSound);
        currentTurret = turretGo;
        this.towerBase = towerBase;
    }

    public void UpgradeTurret()
    {
        if (GameManager.Currency < towerBase.UpgradePrice)
        {
            Debug.Log("Not enough mana for that!");
            return;
        }

        var baseTowerUpgrade = currentTurret.GetComponent<TowerBase>();
        GameManager.Currency -= towerBase.UpgradePrice;
        baseTowerUpgrade._damage += baseTowerUpgrade._damageUpgrade;
        baseTowerUpgrade._range += baseTowerUpgrade._upgradeRange;
        baseTowerUpgrade._upgradeCost += baseTowerUpgrade._upgradeCost;
    }

    public void SellTurret()
    {
        if (towerBase != null)
        {
            towerBase.Sell();
            Destroy(currentTurret);
            towerBase = null;
        }
    }
    
    public Vector3 GetBuildPosition()
    {
        return transform.position + new Vector3(0f, 7.5f, 0f);
    }

    #endregion

    #region Mouse / Touch Events

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (_buildManager.CanBuild)
            return;
        
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }

    private void OnMouseUp()
    {
        if (currentTurret != null)
        {
            _buildManager.SelectTileWithTurret(this);
            return;
        }
        
        if (!_buildManager.CanBuild)
            return;
        
        // Build a turret
        BuildTurret(_buildManager.GetTurretTobuild());
    }

    #endregion
}
