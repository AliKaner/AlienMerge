using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public enum UIType
    {
        Lose,
        Win,
        Shop
    }
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject loseUI;
        public GameObject winUI;
        public GameObject shopUI;
        public UIType currentUIType = UIType.Shop;
        
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

        private void Update()
        {
            switch (currentUIType)
            {
                case UIType.Lose:
                    ResetUI();
                    loseUI.SetActive(true);
                    break;
                case UIType.Win:
                    ResetUI();
                    winUI.SetActive(true);
                    break;
                case UIType.Shop:
                    ResetUI();
                    shopUI.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetUIType(UIType uIType)
        {
            currentUIType = uIType;
        }

        public void ResetUI()
        {
            loseUI.SetActive(false);
            winUI.SetActive(false);
            shopUI.SetActive(false);
        }
    }
}