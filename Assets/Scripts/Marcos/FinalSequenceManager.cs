using UnityEngine;

public class FinalSequenceManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nullEntity;

    [SerializeField] private GameObject normalEndingVFX;
    [SerializeField] private GameObject trueEndingVFX;

    private bool coreReached = false;

    public void ReachCore()
    {
        coreReached = true;
        TryFinish();
    }

    private void TryFinish()
    {
        if (!coreReached) return;

        int collected = SaveManager.CollectedFragments;

        if (collected >= 30)
        {
            TrueEnding();
        }
        else
        {
            NormalEnding();
        }
    }

    private void NormalEnding()
    {
        Debug.Log("Final normal");

        player.GetComponent<PlayerController2D>().enabled = false;
        nullEntity.SetActive(false);

        normalEndingVFX.SetActive(true);
    }

    private void TrueEnding()
    {
        Debug.Log("FINAL ESTENDIDO");

        player.GetComponent<PlayerController2D>().enabled = false;
        nullEntity.SetActive(false);

        trueEndingVFX.SetActive(true);

        // aqui você pode liberar:
        // - arquivos extras
        // - cutscene secreta
        // - lore de NULL
    }
}