using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    [Header("Dynamic Values")]
    public int playerMoney = 0;
    public int playerClickValue = 1;
    public float passiveGainTime = 100f;
    public float criticalChance = 0.02f;
    public float criticalStrikeMultiplier = 2f;

    //[Header("Refrences")]
}

public class GameManager : MonoBehaviour
{
    public static GameManager GlobalGameManager = null;

    [Header("Static References")]
    public PlayerData CurrentPlayerData = null;
    [Header("StatsText")]
    public TextMeshProUGUI PlayerMoneyText = null;
    public TextMeshProUGUI passiveGainText = null;
    public TextMeshProUGUI criticalStrikeMultiplierText = null;
    public TextMeshProUGUI criticalChanceText = null;
    public TextMeshProUGUI clickValueText = null;

    [Header("Effects")]
    public ParticleSystem clickEffect = null;
    public ParticleSystem criticalEffect = null;

    public Button buttonToShake;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;


    private void Awake()
    {
        GlobalGameManager = this;
    }

    private void Start()
    {
        StartCoroutine(PassiveMoneyGain());
        StartCoroutine(UpdatePassiveGainUIText());
    }

    private void Update()
    {
        UpdatePlayerTextStats();
    }

    private void UpdatePlayerTextStats()
    {
        int playerMoney = CurrentPlayerData.playerMoney;
        PlayerMoneyText.text = FormatNumber(playerMoney) + "$";

        criticalStrikeMultiplierText.text = "Crit multiplier " + GlobalGameManager.CurrentPlayerData.criticalStrikeMultiplier.ToString() + "X";

        string convertedCriticalChance = (CurrentPlayerData.criticalChance * 100f).ToString(" 0") + "%";
        criticalChanceText.text = "Critical chance" + convertedCriticalChance;

        clickValueText.text = GlobalGameManager.CurrentPlayerData.playerClickValue.ToString() + "/click";
    }

    private string FormatNumber(int number)
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

    // Coroutine for passive money gain
    IEnumerator PassiveMoneyGain()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentPlayerData.passiveGainTime);
            CurrentPlayerData.playerMoney++; // Increase money
        }
    }

    // Coroutine for updating passive gain time UI
    IEnumerator UpdatePassiveGainUIText()
    {
        float remainingTime = CurrentPlayerData.passiveGainTime;

        while (true)
        {
            remainingTime -= 1f;

            if (remainingTime <= 0f)
            {
                remainingTime = CurrentPlayerData.passiveGainTime;
            }

            passiveGainText.text = "Next Gain In: " + FormatTime(remainingTime);
            yield return new WaitForSeconds(1f); // Update every second
        }
    }

    // Format time as minutes:seconds
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnClick()
    {
        // Check if a critical strike occurs based on a percentage chance
        if (Random.value < CurrentPlayerData.criticalChance)
        {
            float increasedClickValue = CurrentPlayerData.playerClickValue * CurrentPlayerData.criticalStrikeMultiplier;
            CurrentPlayerData.playerMoney += (int)increasedClickValue;
            criticalEffect.Play();
            
        }
        else
        {
            // Regular click without a critical strike
            CurrentPlayerData.playerMoney += CurrentPlayerData.playerClickValue;
            clickEffect.Play();
        }

        ShakeButton();
    }

    private void ShakeButton()
    {
        StartCoroutine(ShakeAnimation());
    }

    private IEnumerator ShakeAnimation()
    {
        Vector3 originalPosition = buttonToShake.transform.position;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-5f, 5f) * shakeMagnitude;

            buttonToShake.transform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        buttonToShake.transform.position = originalPosition;
    }
}

