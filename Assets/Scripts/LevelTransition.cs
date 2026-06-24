using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // === A MÁGICA ACONTECE AQUI ===
            // Apaga os dados salvos do checkpoint da fase anterior
            PlayerPrefs.DeleteAll(); 
            
            // Nota: Se o seu SaveManager usar um código próprio para apagar 
            // em vez do PlayerPrefs padrão da Unity, chame-o aqui. 
            // Exemplo: SaveManager.ClearSave();

            Debug.Log("Saves limpos! Carregando a próxima fase...");
            
            // Carrega a nova fase
            SceneManager.LoadScene(nextSceneName);
        }
    }
}