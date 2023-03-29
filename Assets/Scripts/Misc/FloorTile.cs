using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    private GameObject _currentTurret;
    private Renderer _renderer;
    private Color _defaultColor;
    private BuildManager _buildManager;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        _buildManager = BuildManager.Instance;
    }

    private void OnMouseEnter()
    {
        if (_buildManager.GetTurretToBuild() == null)
            return;
        
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }

    private void OnMouseUp()
    {
        if (_buildManager.GetTurretToBuild() == null)
            return;
        
        if (_currentTurret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }
        
        // Build a turret
        GameObject turretToBuild = _buildManager.GetTurretToBuild();
        _currentTurret = Instantiate(turretToBuild, transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
    }
}
