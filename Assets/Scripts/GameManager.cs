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
    public float passiveGainTime = 100f; // Change this to 10 seconds
    [Header("Refrences")]
    public GameObject clickEffect = null;
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
        CurrentPlayerData.playerMoney += CurrentPlayerData.playerClickValue;
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

