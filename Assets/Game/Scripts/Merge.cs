using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Merge : MonoBehaviour
{
    private Vector3 _startPosition;
    private Transform _draggingObject;
    public Vector3 screenSpace;
    private Grid _grid;

    private void Awake()
    {
        _grid = gameObject.GetComponent<Grid>();
    }

    private void OnMouseDown()
    {
        _startPosition = transform.position;
        _draggingObject = transform;
        _grid.isMerging = true;
    }

    private void OnMouseUp()
    {
        transform.position = _startPosition;
        _draggingObject = null;
    }

    private void Update()
    {
        if (_draggingObject == null && GameManager.Instance.currentGameState == GameState.Battle) return;
        screenSpace = Camera.main.WorldToScreenPoint(_draggingObject.position);
        _draggingObject.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }
}
