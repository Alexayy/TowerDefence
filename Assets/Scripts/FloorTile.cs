using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;

    private GameObject _currentTurret;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }

    private void OnMouseUp()
    {
        if (_currentTurret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }
        
        // Build a turret
        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        _currentTurret = Instantiate(turretToBuild, transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
    }
}
