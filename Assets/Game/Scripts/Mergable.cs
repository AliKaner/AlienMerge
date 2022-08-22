using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    public int level=0;
    public bool isInBattle = false;
    public bool isDragging;
    public GameObject level1Model;
    public GameObject level2Model;
    public GameObject level3Model;

    private void Update()
    {
        switch (level)
        {
            case 0:
                CloseModels();
                level1Model.SetActive(true);
                break;
            case 1:
                CloseModels();
                level2Model.SetActive(true);
                break;
            case 2:
                CloseModels();
                level3Model.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag) && other.gameObject.GetComponent<Mergable>().level == level && !isInBattle)
        {
            other.gameObject.GetComponent<Mergable>().LevelUp();
            Destroy(this);
        }
    }

    private void CloseModels()
    {
        level1Model.SetActive(false);
        level2Model.SetActive(false);
        level3Model.SetActive(false);
    }
    public void LevelUp()
    {
        level++;
    }

}
