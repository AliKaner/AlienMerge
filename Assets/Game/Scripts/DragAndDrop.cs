using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    private bool _mouseState;
    private GameObject _target;
    public Vector3 screenSpace;
    public Vector3 offset;
    int layerMask = LayerMask.GetMask("Unit");
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            _target = GetClickedObject(out hitInfo);
            
            if (_target != null)
            {
                _mouseState = true;
                var position = _target.transform.position;
                if (Camera.main != null)
                {
                    screenSpace = Camera.main.WorldToScreenPoint(position);
                    offset = position -
                             Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                                 screenSpace.z));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
        }
        if (!_mouseState) return;
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        if (Camera.main != null)
        {
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            _target.transform.position = curPosition;
        }

        _target.GetComponent<Collider>().isTrigger = true;
    }

    private GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit,layerMask))
        {
            target = hit.collider.gameObject;
            target.GetComponent<Collider>().isTrigger = false;
        }

        return target;
    }
}