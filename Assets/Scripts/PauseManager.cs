using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }
    
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Update()
    {
        if (InputReader.Instance.PausePressed)
        {
            SetPaused(!isPaused);
        }
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
        IsPaused = paused;

        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
