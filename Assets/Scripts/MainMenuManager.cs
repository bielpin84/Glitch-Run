using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [Header("Paineis")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject painelCreditos;

    // ==========================
    // NOVO JOGO
    // ==========================
    public void NovoJogo()
    {
        // Apaga ABSOLUTAMENTE TUDO o que foi salvo no PlayerPrefs (fases, checkpoints, etc.)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Inicia na primeira fase
        SceneManager.LoadScene("level_01");
    }

    // ==========================
    // CONTINUAR
    // ==========================
    public void Continuar()
    {
        int ultimaFase = PlayerPrefs.GetInt("UltimaFase", 1);

        switch (ultimaFase)
        {
            case 1:
                SceneManager.LoadScene("level_01");
                break;

            case 2:
                SceneManager.LoadScene("level_02");
                break;

            case 3:
                SceneManager.LoadScene("level_03");
                break;

            case 4:
                SceneManager.LoadScene("level_04");
                break;

            default:
                SceneManager.LoadScene("level_01");
                break;
        }
    }

    // ==========================
    // SAIR
    // ==========================
    public void SairJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}