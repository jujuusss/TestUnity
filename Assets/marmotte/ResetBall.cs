using UnityEngine;
using TMPro; // nécessaire pour TextMeshPro

public class ResetBallOnCollision : MonoBehaviour
{
    // Position où la balle sera replacée
    public Vector3 resetPosition = new Vector3(17.58872f, 2.36836f, -18.4136f);

    // Référence au texte du score
    public TextMeshProUGUI scoreText; // pour UI Canvas
    // public TextMeshPro scoreText3D; // si ton score est un TextMeshPro 3D dans la scène

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Réinitialise la vitesse
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Téléporte proprement la balle
                rb.position = resetPosition;
                rb.rotation = Quaternion.identity;
            }
            else
            {
                collision.gameObject.transform.position = resetPosition;
            }

            // Reset du score
            if (scoreText != null)
            {
                scoreText.text = "0";
            }
            // else if (scoreText3D != null)
            // {
            //     scoreText3D.text = "0";
            // }
        }
    }
}
