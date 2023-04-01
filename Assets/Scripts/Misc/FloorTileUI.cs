using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloorTileUI : MonoBehaviour
{
    [Header("Turret on tile UI")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TMP_Text _sellButtonText;
    [SerializeField] private TMP_Text _upgradeButtonText;
    
    private FloorTile _target;
    public GameObject ui;

    private void Awake()
    {
        _upgradeButton.onClick.RemoveAllListeners();
        _sellButton.onClick.RemoveAllListeners();
        
        _upgradeButton.onClick.AddListener(delegate
        {
            _target.UpgradeTurret();
        });
        _sellButton.onClick.AddListener(delegate
        {
            _target.SellTurret();
            ui.SetActive(false);
            BuildManager.Instance.DeselectTile();
        });
    }

    public void SetTarget(FloorTile floorTile)
    {
        this._target = floorTile; 
        transform.position = _target.GetBuildPosition();
        ui.SetActive(true);
        
        _upgradeButtonText.text = $"Upgrade ${_target.towerBase.UpgradePrice.ToString()}";
        _sellButtonText.text = $"Sell ${_target.towerBase.SellPrice.ToString()}";
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
