using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObstacle : MonoBehaviour, ITemporalFreezable
{
    [Header("Pontos de Movimento")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Comportamento")]
    [SerializeField] private float waitAtPoints = 0f;

    private Vector2 target;
    private Rigidbody2D rb;
    private Vector2 velocity;

    private bool isWaiting;
    private bool isFrozen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        target = pointB.position;
    }

    private void FixedUpdate()
    {
        if (isWaiting || isFrozen)
            return;

        Vector2 newPos = Vector2.SmoothDamp(
            rb.position,
            target,
            ref velocity,
            smoothTime,
            moveSpeed
        );

        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, target) < 0.05f)
        {
            StartCoroutine(SwitchTargetWithDelay());
        }
    }

    private IEnumerator SwitchTargetWithDelay()
    {
        isWaiting = true;

        if (waitAtPoints > 0f)
        {
            yield return new WaitForSeconds(waitAtPoints);
        }

        target = (target == (Vector2)pointA.position)
            ? (Vector2)pointB.position
            : (Vector2)pointA.position;

        isWaiting = false;
    }

    public void SetTemporalFrozen(bool frozen)
    {
        isFrozen = frozen;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}