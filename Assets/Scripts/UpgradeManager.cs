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

    private void Start()
    {
        for (int i = 0; i < buttonText.Count && i < upgradeValuesList.Count; i++)
        {
            buttonText[i].text = "Cost: " + upgradeValuesList[i].buttonCost.ToString("F2");
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
            buttonText[buttonIndex].text = "Cost: " + currentUpgradeValues.buttonCost.ToString("F2");
        }
    }

    public void FindSpecificUpgrade(int button)
    {
        switch (button)
        {
            case 0:
                Debug.Log("Button0");
                PassiveGainTimeUpgraded();
                break;
            case 1:
                Debug.Log("Button1");
                break;
            case 2:
                Debug.Log("Button2");
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
        GameManager.GlobalGameManager.CurrentPlayerData.passiveGainTime /= 1.2f;
    }

    void IncreaseClickValue()
    {
        GameManager.GlobalGameManager.CurrentPlayerData.playerClickValue++;
    }
}