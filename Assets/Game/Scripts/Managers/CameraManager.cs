using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    [SerializeField] private Transform battleCamera;
    [SerializeField] private Transform shopCamera;

    private void Awake()
    {
        if (Instance == null )
        {
            Instance =this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"Another instance of {GetType()} already exist! Destroying self...");
            Destroy(this);
        }
    }

    public void SetBattleCamera()
    {
        var transform1 = transform;
        transform1.position = battleCamera.position;
        transform1.rotation = battleCamera.rotation;
    }

    public void SetShopCamera()
    {
        var transform1 = transform;
        transform1.position = shopCamera.position;
        transform1.rotation = shopCamera.rotation;
    }
}
