using System;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseBasillicaTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.turretBasillica);
    }
    
    public void PurchaseBayTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.turretBay);
    }
    
    public void PurchaseFurnaceTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.turretFurnace);
    }
    
    public void PurchasePitterTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.turretPitter);
    }
    
    public void PurchaseHunterTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.turretHunter);
    }
}
