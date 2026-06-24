using UnityEngine;

public enum GamePhase
{
    Fase1_Inicializacao,
    Fase2_Corrupcao,
    Fase3_Instabilidade,
    Fase4_Colapso,
    ChaseFinal
}

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    [Header("Fase Atual")]
    public GamePhase currentPhase;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetPhase(GamePhase.Fase1_Inicializacao);
    }

    // 🔥 FUNÇÃO PRINCIPAL
    public void SetPhase(GamePhase newPhase)
    {
        currentPhase = newPhase;

        Debug.Log("Fase atual: " + currentPhase);

        ApplyPhaseRules(newPhase);
    }

    // 🎯 REGRAS DE CADA FASE
    private void ApplyPhaseRules(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.Fase1_Inicializacao:
                Debug.Log("Fase 1: Movimento + Pulo");
                break;

            case GamePhase.Fase2_Corrupcao:
                Debug.Log("Fase 2: Libera Materialização (Q)");
                break;

            case GamePhase.Fase3_Instabilidade:
                Debug.Log("Fase 3: Libera Temporal (E)");
                break;

            case GamePhase.Fase4_Colapso:
                Debug.Log("Fase 4: Tudo ativo + preparação do chase");
                break;

            case GamePhase.ChaseFinal:
                StartChaseFinal();
                break;
        }
    }

    // 🏃 ATIVA PERSEGUIÇÃO FINAL
    private void StartChaseFinal()
    {
        Debug.Log("CHASE FINAL ATIVADA");

        NullChaseFinalController chase =
            FindFirstObjectByType<NullChaseFinalController>();

        if (chase != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                chase.StartFinalChase(player.transform);
            }
        }
    }

    // 🎬 CHAMAR QUANDO CHEGAR NO NÚCLEO
    public void OnReachCore()
    {
        FinalSequenceManager finalSystem =
            FindFirstObjectByType<FinalSequenceManager>();

        if (finalSystem != null)
        {
            finalSystem.ReachCore();
        }
    }
}