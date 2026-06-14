using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TemporalFreezableRigidbody2D : MonoBehaviour, ITemporalFreezable
{
    private Rigidbody2D rb;
    private RigidbodyType2D originalBodyType;
    private Vector2 storedVelocity;
    private float storedAngularVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalBodyType = rb.bodyType;
    }

    public void SetTemporalFrozen(bool frozen)
    {
        if (frozen)
        {
            storedVelocity = rb.linearVelocity;
            storedAngularVelocity = rb.angularVelocity;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = originalBodyType;
            rb.linearVelocity = storedVelocity;
            rb.angularVelocity = storedAngularVelocity;
        }
    }
}
