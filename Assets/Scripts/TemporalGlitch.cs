using System.Collections;
using UnityEngine;

public class TemporalGlitch : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] affectedObjects;
    [SerializeField] private float duration = 3f;
    [SerializeField] private float cooldown = 5f;

    private bool isActive;
    private float cooldownTimer;

    public bool IsActive => isActive;
    public float CooldownTimer => cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (!PauseManager.IsPaused && Input.GetKeyDown(KeyCode.E) && cooldownTimer <= 0f && !isActive)
        {
            StartCoroutine(FreezeRoutine());
        }
    }

    private IEnumerator FreezeRoutine()
    {
        isActive = true;
        SetFrozen(true);

        yield return new WaitForSeconds(duration);

        SetFrozen(false);
        isActive = false;
        cooldownTimer = cooldown;
    }

    private void SetFrozen(bool frozen)
    {
        foreach (MonoBehaviour affectedObject in affectedObjects)
        {
            if (affectedObject is ITemporalFreezable freezable)
            {
                freezable.SetTemporalFrozen(frozen);
            }
        }
    }
}
