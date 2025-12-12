using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HammerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float swingDuration = 0.15f;

    private bool isSwinging = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private int score = 0;
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        initialRotation = Quaternion.Euler(-90f, 0f, 0f);
        targetRotation = Quaternion.Euler(-180f, 0f, 0f);
        transform.rotation = initialRotation;

        // Affiche le score initial
        UpdateScoreText();
    }

    void Update()
    {
        Move();
        SwingOnSpace();
    }

    void Move()
    {
        if (Keyboard.current == null) return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.leftArrowKey.isPressed) x = 1f;
        if (Keyboard.current.rightArrowKey.isPressed) x = -1f;
        if (Keyboard.current.upArrowKey.isPressed) z = -1f;
        if (Keyboard.current.downArrowKey.isPressed) z = 1f;

        Vector3 move = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void SwingOnSpace()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isSwinging)
        {
            StartCoroutine(SwingCoroutine());
        }
    }

    System.Collections.IEnumerator SwingCoroutine()
    {
        isSwinging = true;

        float t = 0f;
        while (t < swingDuration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t / swingDuration);
            yield return null;
        }

        t = 0f;
        while (t < swingDuration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(targetRotation, initialRotation, t / swingDuration);
            yield return null;
        }

        transform.rotation = initialRotation;
        isSwinging = false;
    }

    // Utilisation de OnTriggerEnter pour détecter la taupe
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mole"))
        {
            // Incrémenter le score
            score++;
            UpdateScoreText();

            // Appeler HideMole sur le script Molemove
            Molemove moleIhit = other.GetComponent<Molemove>();
            if (moleIhit != null)
                moleIhit.HideMole();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;
    }
}
