using UnityEngine;

public class LaserObstacle : MonoBehaviour, ITemporalFreezable
{
    [Header("Laser")]
    [SerializeField] private bool startsActive = true;

    [Header("Cycle")]
    [SerializeField] private bool useCycle = true;
    [SerializeField] private float activeTime = 2f;
    [SerializeField] private float inactiveTime = 2f;

    private bool isActive;
    private bool isFrozen;
    private float timer;

    private Collider2D laserCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        laserCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isActive = startsActive;
        UpdateLaserState();
    }

    private void Update()
    {
        if (isFrozen || !useCycle)
            return;

        timer += Time.deltaTime;

        if (isActive && timer >= activeTime)
        {
            timer = 0f;
            isActive = false;
            UpdateLaserState();
        }
        else if (!isActive && timer >= inactiveTime)
        {
            timer = 0f;
            isActive = true;
            UpdateLaserState();
        }
    }

    private void UpdateLaserState()
    {
        if (laserCollider != null)
            laserCollider.enabled = isActive;

        if (spriteRenderer != null)
            spriteRenderer.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        PlayerRespawn player = other.GetComponent<PlayerRespawn>();

        if (player != null)
        {
            player.Die();
        }
    }

    public void SetTemporalFrozen(bool frozen)
    {
        isFrozen = frozen;
    }
}