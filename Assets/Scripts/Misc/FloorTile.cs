using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [FormerlySerializedAs("_currentTurret")] public GameObject currentTurret;
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
        if (!_buildManager.CanBuild)
            return;
        
        if (currentTurret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }
        
        // Build a turret
        _buildManager.BuildTurretOn(this);
    }
}
