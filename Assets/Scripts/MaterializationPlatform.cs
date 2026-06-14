using UnityEngine;

public enum MaterializationGroup
{
    Blue,
    Red
}

[RequireComponent(typeof(Collider2D))]
public class MaterializationPlatform : MonoBehaviour
{
    [SerializeField] private MaterializationGroup group;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Collider2D platformCollider;

    public MaterializationGroup Group => group;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void SetActiveGroup(MaterializationGroup activeGroup)
    {
        bool active = group == activeGroup;
        platformCollider.enabled = active;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = active;
        }
    }
}
