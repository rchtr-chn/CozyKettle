using System;
using UnityEngine;
using UnityEngine.UI;

public class SummaryManager : MonoBehaviour
{
    [Header("Text References")]
    public Text TotalCustomersServedText;
    public Text TotalSatisfiedCustomersText;
    public Text TotalUnsatisfiedCustomersText;
    public Text ExpensesText;
    public Text GrossEarningsText;
    public Text PotentialEarningsText;
    public Text NetProfitText;

    [Header("UI References")]
    public Button ProceedButton;

    [Header("Data Tracking")]
    public int TotalCustomersServed { get; private set; } = 0;
    public int TotalSatisfiedCustomers { get; private set; } = 0;
    public int TotalUnsatisfiedCustomers { get; private set; } = 0;
    public float Expenses { get; private set; } = 0f;
    public float GrossEarnings { get; private set; } = 0f;
    public float PotentialEarnings { get; private set; } = 0f;
    public float NetProfit { get; private set; } = 0f;

    public int GetTotalCustomers() { return TotalCustomersServed; }
    public int GetTotalSatisfiedCustomers() { return TotalSatisfiedCustomers; }
    public int GetTotalUnsatisfiedCustomers() { return TotalUnsatisfiedCustomers; }
    public float GetExpenses() { return Expenses; }
    public float GetGrossEarnings() { return GrossEarnings; }
    public float GetPotentialEarnings() { return PotentialEarnings; }
    public float GetNetProfit() { return NetProfit; }

    private void Awake()
    {
        ProceedButton.onClick.AddListener(SceneController.Instance.LoadTeaShopScene);
    }

    public void RecordCustomerServed(bool isSatisfied, float earning, float potentialEarning)
    {
        TotalCustomersServed++;
        if (isSatisfied)
            TotalSatisfiedCustomers++;
        else
            TotalUnsatisfiedCustomers++;
        GrossEarnings += earning;
        PotentialEarnings += potentialEarning;
    }

    private void UpdateNetProfit()
    {
        NetProfit = GrossEarnings - Expenses;
    }

    public void AddExpense(float amount)
    {
        Expenses += amount;
    }

    public void UpdateSummaryTexts()
    {
        UpdateNetProfit();
        TotalCustomersServedText.text = TotalCustomersServed.ToString();
        TotalSatisfiedCustomersText.text = TotalSatisfiedCustomers.ToString();
        TotalUnsatisfiedCustomersText.text = TotalUnsatisfiedCustomers.ToString();
        ExpensesText.text = "$" + Expenses.ToString("F2");
        GrossEarningsText.text = "$" + GrossEarnings.ToString("F2");
        PotentialEarningsText.text = "$" + PotentialEarnings.ToString("F2");
        NetProfitText.text = "$" + NetProfit.ToString("F2");
    }
}
