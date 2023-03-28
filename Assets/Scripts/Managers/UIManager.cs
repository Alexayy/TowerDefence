using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currency;
    [SerializeField] private TMP_Text _lives;

    [SerializeField] private Button _basillicaTurret;
    [SerializeField] private Button _bayTurret;
    [SerializeField] private Button _drosTurret;
    [SerializeField] private Button _hunterTurret;
    [SerializeField] private Button _furnaceTurret;

    private void Start()
    {
        _basillicaTurret.onClick.RemoveAllListeners();
        _bayTurret.onClick.RemoveAllListeners();
        _drosTurret.onClick.RemoveAllListeners();
        _hunterTurret.onClick.RemoveAllListeners();
        _furnaceTurret.onClick.RemoveAllListeners();
        
        _basillicaTurret.onClick.AddListener(SpawnBasillicaTurret);
        _bayTurret.onClick.AddListener(SpawnBayTurret);
        _drosTurret.onClick.AddListener(SpawnDrosTurret);
        _hunterTurret.onClick.AddListener(SpawnHunterTurret);
        _furnaceTurret.onClick.AddListener(SpawnFurnaceTurret);

        _currency.text = $"${GameManager.Instance.currency.ToString()}";
        _lives.text = $"Lives: {GameManager.Instance.health.ToString()}";
    }

    public void SpawnBasillicaTurret() { Spawn((int)TurretCode.FairBasillica); }
    public void SpawnBayTurret() { Spawn((int)TurretCode.SurgicalBay); }
    public void SpawnDrosTurret() { Spawn((int)TurretCode.DrosPitter); }
    public void SpawnHunterTurret() { Spawn((int)TurretCode.HuntersMaze); }
    public void SpawnFurnaceTurret() { Spawn((int)TurretCode.AutonomousFurnace); }

    private void Spawn(int turretCode)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left mouse button clicked!");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"{hit.collider.tag}");
                
                if (hit.collider.tag == "BaseFloor")
                {
                    ObjectPooler.Instance.SpawnFromPool($"Turret{turretCode}", hit.transform.position,
                        Quaternion.identity);
                    Debug.Log("Object Spawned?");
                }
            }
        }
    }

    public enum TurretCode
    {
        FairBasillica = 1,
        SurgicalBay = 2,
        AutonomousFurnace = 3,
        DrosPitter = 4,
        HuntersMaze = 5,
    }
}
