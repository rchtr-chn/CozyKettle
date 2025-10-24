using System;
using UnityEngine;
using UnityEngine.UI;

public class BankScreen : MonoBehaviour
{
    [SerializeField] private Text _moneyText;

    private void OnEnable()
    {
        UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        float currentMoney = PlayerStaticData.GetMoney();
        _moneyText.text = "$ " + currentMoney.ToString("F2");
    }
}
