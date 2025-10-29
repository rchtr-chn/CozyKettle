using System;
using UnityEngine;
using UnityEngine.UI;

public class BankScreen : MonoBehaviour
{
    [SerializeField] private Text _moneyText;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PhoneClickSFX);
        UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        float currentMoney = PlayerStaticData.GetMoney();
        _moneyText.text = "$ " + currentMoney.ToString("F2");
    }

    private void OnDisable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PhoneClickSFX);
    }
}
