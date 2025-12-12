using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text timerText;
    public float gameTimer = 30f;
    private float initialGameTimer;

    public GameObject moleContainer;
    private Molemove[] moles;

    public float showMoleInterval = 1.5f;
    private float showMoleTimer;

    void Start()
    {
        moles = moleContainer.GetComponentsInChildren<Molemove>();
        showMoleTimer = showMoleInterval;
        initialGameTimer = gameTimer; // sauvegarder la valeur initiale
    }

    void Update()
    {
        // Réinitialisation du jeu si on appuie sur R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        // Timer de jeu
        if (gameTimer > 0f)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = "TIMER: " + Mathf.Floor(gameTimer);

            // Timer d'apparition des moles
            showMoleTimer -= Time.deltaTime;
            if (showMoleTimer <= 0f)
            {
                ShowRandomMoles();
                showMoleTimer = showMoleInterval;
            }
        }
        else
        {
            timerText.text = "GAME OVER";
        }
    }

    void ShowRandomMoles()
    {
        int numMolesToShow = Random.Range(1, 4);

        for (int i = 0; i < numMolesToShow; i++)
        {
            Molemove mole = moles[Random.Range(0, moles.Length)];

            if (mole.transform.localPosition.y <= mole.hiddenYHeight + 0.01f)
            {
                mole.ShowMole();
            }
            else
            {
                i--; 
            }
        }
    }

    void ResetGame()
    {
        gameTimer = initialGameTimer; // réinitialiser le timer
        showMoleTimer = showMoleInterval;

        // Remettre toutes les moles à leur position cachée
        foreach (Molemove mole in moles)
        {
            mole.HideMole(); // il faut que ta classe Molemove ait une fonction HideMole()
        }

        timerText.text = "TIMER: " + Mathf.Floor(gameTimer);
    }
}
