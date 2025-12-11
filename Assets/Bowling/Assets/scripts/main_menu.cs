using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public TextMeshProUGUI highestScoreText;

    void Start()
    {
        // Charger et afficher le meilleur score sauvegardé
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highestScoreText.text = "Highest Score : " + highScore;
    }

    public void StartGame()
    {
        // Lance la scène du jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Quitte l'application
        Application.Quit();
    }
}
