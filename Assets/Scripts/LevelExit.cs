using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private LevelProgressionManager progressionManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController2D>() != null && progressionManager != null)
        {
            progressionManager.CompleteLevel();
        }
    }
}
