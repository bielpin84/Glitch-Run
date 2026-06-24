using UnityEngine;

public class SawObstacle : MonoBehaviour, ITemporalFreezable
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 target;
    private bool isFrozen;

    private void Start()
    {
        if (pointA != null && pointB != null)
        {
            transform.position = pointA.position;
            target = pointB.position;
        }
    }

    private void Update()
    {
        if (isFrozen)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = target == pointA.position
                ? pointB.position
                : pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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