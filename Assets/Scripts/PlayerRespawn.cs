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

        // === A NOVA MÁGICA DO SPAWN ===
        // Procura na fase se existe um objeto chamado "Spawn"
        GameObject spawnPoint = GameObject.Find("Spawn");

        if (spawnPoint != null)
        {
            // Se encontrar, teletransporta o jogador para lá e define como o Checkpoint Zero
            transform.position = spawnPoint.transform.position;
            currentCheckpoint = spawnPoint.transform.position;
            Debug.Log("Jogador posicionado automaticamente no Spawn do mapa!");
        }
        else
        {
            // Se não encontrar (ex: numa fase de testes), usa a posição em que foi deixado na cena
            currentCheckpoint = transform.position;
        }
    }

    private void Start()
    {
        // Se houver um save de checkpoint (porque o jogador tocou numa bandeira),
        // ele vai substituir a posição do "Spawn" por este save.
        // Como o nosso portal da mudança de fase apagou os saves, 
        // isto só vai rodar se ele já estiver a meio do nível!
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