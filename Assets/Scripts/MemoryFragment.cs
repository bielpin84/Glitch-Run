using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [SerializeField] private string fragmentId = "fase_1_fragmento_01";
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectClip;

    private void Start()
    {
        if (SaveManager.IsFragmentCollected(fragmentId))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController2D>() == null)
        {
            return;
        }

        SaveManager.CollectFragment(fragmentId);

        if (audioSource != null && collectClip != null)
        {
            AudioSource.PlayClipAtPoint(
                collectClip,
                transform.position);
        }

        gameObject.SetActive(false);
    }
}
