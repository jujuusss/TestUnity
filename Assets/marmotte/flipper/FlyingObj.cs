using UnityEngine;

public class CubeFloat : MonoBehaviour
{
    public float amplitude = 1f; // hauteur du mouvement
    public float speed = 2f;     // vitesse de l'oscillation

    private Vector3 startPos;

    void Start()
    {
        // Sauvegarde la position initiale du cube
        startPos = transform.position;
    }

    void Update()
    {
        // Déplacement vertical avec sin
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
