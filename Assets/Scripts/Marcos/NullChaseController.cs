using UnityEngine;

public class NullChaseFinalController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform player;

    [Header("Sequência de Fuga")]
    [SerializeField] private Transform[] chasePoints;
    private int currentPoint = 0;

    [Header("Movimento")]
    [SerializeField] private float baseSpeed = 4f;
    [SerializeField] private float speedIncrease = 0.5f;
    [SerializeField] private float maxSpeed = 10f;

    private float currentSpeed;

    private bool chaseActive = false;
    private PlayerRespawn playerRespawn;

    void Start()
    {
        if (player != null)
        {
            playerRespawn = player.GetComponent<PlayerRespawn>();
        }

        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (!chaseActive || player == null)
            return;

        currentSpeed = Mathf.Min(currentSpeed + speedIncrease * Time.deltaTime, maxSpeed);

        Vector3 target = chasePoints[currentPoint].position;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            currentSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target) < 0.5f)
        {
            currentPoint++;

            if (currentPoint >= chasePoints.Length)
            {
                currentPoint = chasePoints.Length - 1;
            }
        }

        if (Vector2.Distance(transform.position, player.position) < 0.5f)
        {
            if (playerRespawn != null)
            {
                playerRespawn.Die();
            }
        }
    }

    public void StartFinalChase(Transform playerTarget)
    {
        player = playerTarget;
        chaseActive = true;
        currentPoint = 0;
        currentSpeed = baseSpeed;
    }
}