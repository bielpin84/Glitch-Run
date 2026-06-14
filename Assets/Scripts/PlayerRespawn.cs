using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathClip;

    private Rigidbody2D rb;
    private PlayerController2D controller;
    private Vector3 currentCheckpoint;
    private bool isRespawning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController2D>();
        currentCheckpoint = transform.position;
    }

    private void Start()
    {
        Checkpoint savedCheckpoint = Checkpoint.FindById(SaveManager.LastCheckpointId);

        if (savedCheckpoint != null)
        {
            currentCheckpoint = savedCheckpoint.SpawnPosition;
            transform.position = currentCheckpoint;
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition, string checkpointId)
    {
        currentCheckpoint = checkpointPosition;
        SaveManager.SaveCheckpoint(checkpointId);
    }

    public void Die()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;
        controller?.SetInputLocked(true);
        rb.linearVelocity = Vector2.zero;

        if (deathEffect != null)
        {
            deathEffect.Play();
        }

        if (audioSource != null && deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        yield return new WaitForSeconds(respawnDelay);

        transform.position = currentCheckpoint;
        rb.linearVelocity = Vector2.zero;
        controller?.SetInputLocked(false);
        isRespawning = false;
    }
}
