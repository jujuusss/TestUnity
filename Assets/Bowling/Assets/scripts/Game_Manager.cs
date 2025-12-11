using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // ---------- Références ----------
    public GameObject ball;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI roundUI;
    public Slider powerSlider;            // UI de la jauge de puissance
    public float powerChargeSpeed = 1.5f; // vitesse de remplissage

    // ---------- Gameplay ----------
    GameObject[] pins;
    Vector3[] pinsStartPos;
    Vector3 ballStartPos;

    int score = 0;
    int currentRound = 1;
    const int maxRounds = 10;

    bool roundEnded = false;
    bool isCharging = false;

    float currentPower = 0f;  // valeur 0 → 1

    [SerializeField]
    float resetDelay = 2f;

    // ---------- START ----------
    void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("pin");

        pinsStartPos = new Vector3[pins.Length];
        for (int i = 0; i < pins.Length; i++)
            pinsStartPos[i] = pins[i].transform.position;

        ballStartPos = ball.transform.position;

        powerSlider.value = 0;

        UpdateUI();
    }

    // ---------- UPDATE ----------
    void Update()
    {
        MoveBall();

        HandlePowerCharge();

        if (!roundEnded &&
            (Input.GetKeyDown(KeyCode.Space) || ball.transform.position.y < -20f))
        {
            if (!isCharging) return; // empêche double détection

            LaunchBall();
        }
    }

    // ---------- Jauge puissance ----------
    void HandlePowerCharge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentPower = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            // augmentation continue
            currentPower += Time.deltaTime * powerChargeSpeed;

            // clamp
            currentPower = Mathf.Clamp01(currentPower);

            powerSlider.value = currentPower;
        }

        // Si on relâche → tirer
        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        isCharging = false;

        // conversion en force réelle
        float force = Mathf.Lerp(3700f, 4200f, currentPower);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * force);

        roundEnded = true;

        StartCoroutine(ResetRound());
    }

    // ---------- Déplacement ----------
    void MoveBall()
    {
        Vector3 pos = ball.transform.position;
        pos += Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -0.695f, 0.2845f);
        ball.transform.position = pos;
    }

    // ---------- Score ----------
    void CountPinsDown()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            bool pinTilted = Vector3.Dot(pins[i].transform.up, Vector3.up) < 0.9f;
            bool pinFallenOff = pins[i].transform.position.y < -1f;

            if (pins[i].activeSelf && (pinTilted || pinFallenOff))
                score++;
        }

        UpdateUI();
    }

    // ---------- Reset & gestion rounds ----------
    IEnumerator ResetRound()
    {
        yield return new WaitForSeconds(resetDelay);

        CountPinsDown();
        currentRound++;

        if (currentRound > maxRounds)
            EndGame();
        else
            ResetPinsAndBall();
    }

    void ResetPinsAndBall()
    {
        // Reset balle
        Rigidbody rbBall = ball.GetComponent<Rigidbody>();
        rbBall.linearVelocity = Vector3.zero;
        rbBall.angularVelocity = Vector3.zero;

        // Position aléatoire
        float randomX = Random.Range(-0.65f, 0.28f);
        Vector3 newPos = ballStartPos;
        newPos.x = randomX;

        ball.transform.position = newPos;

        // Reset quilles
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            pins[i].transform.position = pinsStartPos[i];
            pins[i].transform.rotation = Quaternion.identity;

            Rigidbody rbPin = pins[i].GetComponent<Rigidbody>();
            rbPin.linearVelocity = Vector3.zero;
            rbPin.angularVelocity = Vector3.zero;
        }

        powerSlider.value = 0;
        currentPower = 0;

        roundEnded = false;
        UpdateUI();
    }

    // ---------- Fin du jeu ----------
    void EndGame()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(0);
    }

    // ---------- UI ----------
    void UpdateUI()
    {
        scoreUI.text = "Score : " + score;
        roundUI.text = "Round : " + currentRound + "/" + maxRounds;
    }
}
