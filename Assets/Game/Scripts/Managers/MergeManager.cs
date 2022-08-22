using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private Vector3 initalPos;  // variable for each objects first pos

    private Vector3 _mOffset;   // for drag n drop 
    private float _mZCoord;     // for drag n drop 

    public GameObject superior;   // prefab to spawn if we merge

    private GameObject _other;    // other object for merging

    public string charName;      // each character will have a name and a level , same characters will have same name and level
    public int charLevel;        // this two variable used to compare if objects are same
    
    public LayerMask ignoreMe;      // ignored layer for ray
    
    private MergeManager toMerge;     
   
    private void Start()
    {
        initalPos = transform.position;  // get position at start
    }

    private void OnMouseUp() 
    {                           
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,~ignoreMe))
        {
            _other = hit.transform.gameObject;
            
            if (_other.GetComponent<MergeManager>() != null)        
            {
                toMerge = _other.GetComponent<MergeManager>();       

                if (charName.Equals(toMerge.charName) && charLevel == toMerge.charLevel)   
                {
                    if (superior != null)       
                    {
                        superior = Instantiate(superior);         
                        superior.transform.position = _other.transform.position;  
                        Destroy(_other);    
                        Destroy(this.gameObject);
                        return;

                    }
                    else
                    {
                        transform.position = initalPos;    
                    }
                }
                else
                {
                    transform.position = initalPos;
                }

            }
            else
            {
                transform.position = initalPos;
            }


        }
            
        else 
        {
            transform.position = initalPos;
        }
        gameObject.layer = 0;

    }

    private void OnMouseDown()
    {
        _mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _mOffset = gameObject.transform.position - GetMouseWorldPos();

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3((GetMouseWorldPos() + _mOffset).x, 1.5f, (GetMouseWorldPos() + _mOffset).z);
        gameObject.layer = 6;
    }



}
