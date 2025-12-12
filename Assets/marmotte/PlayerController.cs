using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10f;

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Flèches gauche/droite
        if (Keyboard.current.leftArrowKey.isPressed)
            horizontal = 1f; // gauche
        if (Keyboard.current.rightArrowKey.isPressed)
            horizontal = -1f;  // droite

        // Flèches haut/bas
        if (Keyboard.current.upArrowKey.isPressed)
            vertical = -1f;  // haut
        if (Keyboard.current.downArrowKey.isPressed)
            vertical = 1f; // bas

        // Déplacement sur axes X et Z
        Vector3 move = new Vector3(horizontal, 0f, vertical);

        // Ici on inverse le signe si ça va à l'envers
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
