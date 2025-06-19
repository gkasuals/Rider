using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text timeText;
    public Text SurfaceSpeedText;
    public Text CarSpeedText;
    public Text currentTImetext;
    public Text fastestTimeText;
    public Text coinText;
    public GameObject Panel;
    public Button restartButton;
    public Button exitButton; // Inspector에서 할당
    public GameObject mainMenuPanel;
    public Button startButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.GameRestart());
        var nav = restartButton.navigation;
        nav.mode = Navigation.Mode.None;
        restartButton.navigation = nav;

        // 종료 버튼 이벤트 연결
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
            var nav2 = exitButton.navigation;
            nav2.mode = Navigation.Mode.None;
            exitButton.navigation = nav2;
        }

        // Example usage of UpdateCoinText
        float totalCoinValue = 0f; // Replace with actual coin value
        UIManager.Instance.UpdateCoinText(totalCoinValue);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        ShowMainMenu(false);
        Time.timeScale = 1f;
    }

    public void UpdateSurfaceSpeedText(string surfacespeed)
    {
        SurfaceSpeedText.text = surfacespeed;
    }
    public void UpdateTimeText(string time)
    {
        timeText.text = time;
    }
    public void UpdateCarSpeedText(string carspeed)
    {
        CarSpeedText.text = carspeed;
    }
    public void UpdateCurrentTimeText(string currenttime)
    {
        currentTImetext.text = currenttime;
    }
    public void UpdateFastestTimeText(string fastesttime)
    {
        fastestTimeText.text = fastesttime;
    }
    public void UpdateCoinText(float coinValue)
    {
        if (coinText != null)
            coinText.text = $"Coin: {coinValue}";
    }
    public void ShowPanel(bool show)
    {
        Panel.SetActive(show);
        if (show)
        {
            restartButton.interactable = false;
            StartCoroutine(EnableButtonNextFrame());
        }
    }
    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void ShowMainMenu(bool show)
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(show);
    }

    private System.Collections.IEnumerator EnableButtonNextFrame()
    {
        yield return null;
        restartButton.interactable = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}