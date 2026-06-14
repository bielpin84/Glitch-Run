using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressionManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private string nextSceneName;
    [SerializeField] private bool loadNextSceneAutomatically = true;

    public void CompleteLevel()
    {
        int nextLevel = currentLevel + 1;
        SaveManager.SaveLevelProgress(nextLevel, nextLevel);

        if (loadNextSceneAutomatically && !string.IsNullOrWhiteSpace(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
