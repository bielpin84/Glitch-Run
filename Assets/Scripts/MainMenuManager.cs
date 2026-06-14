using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string[] levelSceneNames = { "Fase1", "Fase2", "Fase3", "Fase4" };

    public void NewGame()
    {
        SaveManager.ResetSave();
        LoadLevel(1);
    }

    public void ContinueGame()
    {
        int level =
            Mathf.Clamp(
                SaveManager.LastLevel,
                1,
                levelSceneNames.Length);

        LoadLevel(level);
    }

    public void LoadLevel(int levelNumber)
    {
        int arrayIndex = Mathf.Clamp(levelNumber, 1, levelSceneNames.Length) - 1;
        string sceneName = levelSceneNames[arrayIndex];

        if (!string.IsNullOrWhiteSpace(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
