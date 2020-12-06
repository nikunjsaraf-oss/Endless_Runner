using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverObject = null;
    [SerializeField] GameObject startGameObject = null;
    [SerializeField] TMP_Text coinsText = null;

    [HideInInspector]
    public static bool isGameOver = false;
    [HideInInspector]
    public static bool isGameStarted = false;
    [HideInInspector]
    public static int coins = 0;

    private void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
        coins = 0;
    }

    private void Update()
    {
        if (isGameOver)
        {
            gameOverObject.SetActive(true);
            Time.timeScale = 0;

        }
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startGameObject);
        }
        coinsText.text = $"Coins: {coins}";
    }

}
