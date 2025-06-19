using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float elapsedTime = 0f;
    private float fatestTime = float.MaxValue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateTimeText(FormatElapsedTime(elapsedTime));                  
    }


    private string FormatElapsedTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        int milliseconds = (int)((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    public void GameStop()
    {
        Time.timeScale = 0f;
        if (elapsedTime < fatestTime)
        {
            fatestTime = elapsedTime;
            Debug.Log("New fastest time: " + FormatElapsedTime(fatestTime));
        }

        // 패널에 표시할 값 전달
        UIManager.Instance.UpdateFastestTimeText($"Fastest Time: {FormatElapsedTime(fatestTime)}");
        UIManager.Instance.UpdateCurrentTimeText($"Current Time: {FormatElapsedTime(elapsedTime)}");

        UIManager.Instance.ShowPanel(true);

        elapsedTime = 0f; // 기록 초기화(재시작 대비)
    }
    public void GameRestart()
    {
        UIManager.Instance.HidePanel();
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(     
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
        elapsedTime = 0f;
    }
    void Start()
    {
        Time.timeScale = 0f;
        if (UIManager.Instance != null)
            UIManager.Instance.ShowMainMenu(true);
    }
}
