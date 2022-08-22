using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    
    public TextMeshProUGUI totalGoldText;
    public TextMeshProUGUI meleeUnitPriceText;
    public TextMeshProUGUI rangedUnitPriceText;
    
    private bool _wasInitialized;

    [SerializeField] private int meleeUnitPrice;
    [SerializeField] private int rangedUnitPrice;
    [SerializeField] private int gold;
    
    private const string Golds = "Gold";
    
    public void Awake()
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

    private void Start()
    {
        PriceTagRefresh();
    }
    
    public void GainGold(int amout)
    {
        gold += amout;
        PriceTagRefresh();
    }

    public void BuyMeleeUnit()
    {
        gold -= meleeUnitPrice;
        meleeUnitPrice += 5;
        PriceTagRefresh();
    }

    public void BuyRangedUnit()
    {
        gold -= rangedUnitPrice;
        rangedUnitPrice += 5;
        PriceTagRefresh();
    }

    private void PriceTagRefresh()
    {
        Debug.Log("GOLD");
        meleeUnitPriceText.text = meleeUnitPrice.ToString();
        rangedUnitPriceText.text = rangedUnitPrice.ToString();
        totalGoldText.text = gold.ToString();
    }
}