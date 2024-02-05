using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UpgradeValues
{
    public float buttonCost;
    public float increaseButtonCost;
}

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeValues> upgradeValuesList = new List<UpgradeValues>();
    public List<TextMeshProUGUI> buttonText = new List<TextMeshProUGUI>();
    bool xOption = true;

    [Header("IncreaseStats")]
    public float decreasePassiveGainTime = 1.2f;
    public float increaseCriticalChance = 0.02f;
    public float criticalStrikeValueIncrease = 2f;
    public int playerClickIncrease = 1;

    private void Start()
    {
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        for (int i = 0; i < buttonText.Count && i < upgradeValuesList.Count; i++)
        {
            buttonText[i].text = "Cost: " + FormatNumber(upgradeValuesList[i].buttonCost) + "$";
        }
    }

    public void Upgrade(int buttonIndex)
    {
        UpgradeValues currentUpgradeValues = GetCurrentUpgradeValues(buttonIndex);

        if (currentUpgradeValues != null)
        {
            if (xOption)
            {
                // If xOption is true, upgrade only once
                IncreaseCostOnButton(buttonIndex);
            }
            else
            {
                // If xOption is false, keep upgrading until the player can't afford it
                while (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney >= currentUpgradeValues.buttonCost)
                {
                    IncreaseCostOnButton(buttonIndex);
                }
            }
        }
    }

    // Get the current upgrade values based on the specified index
    private UpgradeValues GetCurrentUpgradeValues(int index)
    {
        if (index >= 0 && index < upgradeValuesList.Count)
        {
            return upgradeValuesList[index];
        }
        else
        {
            Debug.LogError("Invalid upgrade index");
            return null;
        }
    }

    public void IncreaseCostOnButton(int buttonIndex)
    {
        UpgradeValues currentUpgradeValues = GetCurrentUpgradeValues(buttonIndex);

        if (currentUpgradeValues != null && GameManager.GlobalGameManager.CurrentPlayerData.playerMoney >= currentUpgradeValues.buttonCost)
        {
            FindSpecificUpgrade(buttonIndex);
            GameManager.GlobalGameManager.CurrentPlayerData.playerMoney -= (int)currentUpgradeValues.buttonCost;
            currentUpgradeValues.buttonCost *= currentUpgradeValues.increaseButtonCost;

            // Update the button text after increasing the cost
            buttonText[buttonIndex].text = "Cost: " + FormatNumber(currentUpgradeValues.buttonCost) + "$";
        }
    }

    private string FormatNumber(float number)
    {
        string[] suffixes = { "", "K", "M", "B", "T" }; // Add more suffixes as needed
        int index = 0;
        float formattedNumber = number;

        while (formattedNumber >= 1000f && index < suffixes.Length - 1)
        {
            formattedNumber /= 1000f;
            index++;
        }

        return formattedNumber.ToString("0.#") + suffixes[index];
    }

    public void FindSpecificUpgrade(int button)
    {
        switch (button)
        {
            case 0:
                PassiveGainTimeUpgraded();
                break;
            case 1:
                IncreaseCriticalChance();
                break;
            case 2:
                IncreaseCriticalStrikeMultiplier();
                break;
            case 3:
                IncreaseClickValue();
                break;
            // Add more cases for other buttons as needed
            default:
                Debug.LogWarning("No specific upgrade method defined for button " + button);
                break;
        }
    }

    void PassiveGainTimeUpgraded()
    {
        GameManager.GlobalGameManager.CurrentPlayerData.passiveGainTime /= decreasePassiveGainTime;
    }

    void IncreaseCriticalChance()
    {
        GameManager.GlobalGameManager.CurrentPlayerData.criticalChance += increaseCriticalChance;
    }

    void IncreaseCriticalStrikeMultiplier()
    {
        GameManager.GlobalGameManager.CurrentPlayerData.criticalStrikeMultiplier += criticalStrikeValueIncrease;
    }

    void IncreaseClickValue()
    {
        GameManager.GlobalGameManager.CurrentPlayerData.playerClickValue += playerClickIncrease;
    }

    public void ToggleMode1(bool isToggelOn)
    {
        if(!isToggelOn)
        {
            xOption = isToggelOn;
        }
        else
        {
            xOption = isToggelOn;
        }
    }
}