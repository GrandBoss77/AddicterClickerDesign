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
    public TextMeshProUGUI PlayerMoneyText = null;

    public Button buttonToShake;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;


    private void Awake()
    {
        GlobalGameManager = this;
    }

    private void Update()
    {
        PlayerMoneyText.text = CurrentPlayerData.playerMoney.ToString() + "$";
    }

    private void Start()
    {
        // Start the coroutine for passive money gain
        StartCoroutine(PassiveMoneyGain());
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

    public void OnClick()
    {
        // Check if a critical strike occurs based on a percentage chance
        if (Random.value < CurrentPlayerData.criticalChance)
        {
            float increasedClickValue = CurrentPlayerData.playerClickValue * CurrentPlayerData.criticalStrikeMultiplier;
            CurrentPlayerData.playerMoney += (int)increasedClickValue;
        }
        else
        {
            // Regular click without a critical strike
            CurrentPlayerData.playerMoney += CurrentPlayerData.playerClickValue;
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

