using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public TextMeshProUGUI text;
 
    private int _gold;
    private bool _wasInitialized;
 
    private const string Key = "Gold";
 
    private void OnEnable()
    {
        _gold = PlayerPrefs.GetInt(Key, 0);
        _wasInitialized = true;
    }
 
    private void OnDisable()
    {
        PlayerPrefs.SetInt(Key, _gold);
        _wasInitialized = false;
    }

    public void ChangeGold(int amount)
    {
        _gold += amount;
        PlayerPrefs.SetInt(Key, _gold);
    }
 
    private void OnGoldChanged()
    {
        text.text = "Gold: " + _gold;
    }
 
    public int Gold
    {
        get => _gold;
        set
        {
            if (_wasInitialized == false)
            {
                Debug.LogError("Cannot set gold before initialization.");
                return;
            }

            if (_gold == value) return;
            _gold = value;
            OnGoldChanged();
        }
    }
}
