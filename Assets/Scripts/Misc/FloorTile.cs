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
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (currentTurret != null)
        {
            _buildManager.SelectTileWithTurret(this);
            return;
        }
        
        if (!_buildManager.CanBuild)
            return;
        
        // Build a turret
        _buildManager.BuildTurretOn(this);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + new Vector3(0f, 7.5f, 0f);
    }
}
