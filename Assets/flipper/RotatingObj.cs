using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f); // vitesse de rotation en degrés/seconde

    void Update()
    {
        // Appliquer la rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
