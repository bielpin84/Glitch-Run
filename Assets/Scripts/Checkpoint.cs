using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string checkpointId = "checkpoint_01";
    [SerializeField] private Transform spawnPoint;

    public string CheckpointId => checkpointId;
    public Vector3 SpawnPosition => spawnPoint != null ? spawnPoint.position : transform.position;

    public static Checkpoint FindById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.CheckpointId == id)
            {
                return checkpoint;
            }
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

        if (playerRespawn != null)
        {
            playerRespawn.SetCheckpoint(SpawnPosition, checkpointId);
        }
    }
}
