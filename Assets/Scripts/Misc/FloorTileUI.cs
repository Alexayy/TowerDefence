using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileUI : MonoBehaviour
{
    private FloorTile _target;
    public GameObject ui;

    public void SetTarget(FloorTile floorTile)
    {
        this._target = floorTile; 
        transform.position = _target.GetBuildPosition();
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
