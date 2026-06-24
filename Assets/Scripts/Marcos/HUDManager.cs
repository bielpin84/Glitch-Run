using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fragmentText;
    [SerializeField] private TextMeshProUGUI materializationText;
    [SerializeField] private TextMeshProUGUI temporalText;

    [SerializeField] private MaterializationGlitch materializationGlitch;
    [SerializeField] private TemporalGlitch temporalGlitch;

    private void Update()
    {
        // Fragmentos
        fragmentText.text =
            $"Fragmentos: {SaveManager.CollectedFragments} / 30";

        // Materialização
        materializationText.text =
            materializationGlitch.ActiveGroup == MaterializationGroup.Blue
            ? "Materialização: Disponível"
            : "Materialização: Em uso";

        // Temporal
        if (temporalGlitch.IsActive)
        {
            temporalText.text = "Temporal: Ativo";
        }
        else if (temporalGlitch.CooldownTimer > 0)
        {
            temporalText.text = "Temporal: Em recarga";
        }
        else
        {
            temporalText.text = "Temporal: Disponível";
        }
    }
}