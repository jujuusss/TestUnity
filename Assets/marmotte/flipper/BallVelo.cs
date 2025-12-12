using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceForce = 10f;
    public float speedMultiplier = 1.1f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 normal = collision.contacts[0].normal;

            rb.AddForce(-normal * bounceForce, ForceMode.Impulse);

            rb.linearVelocity *= speedMultiplier;

            ScoreManager.instance.AddScore(100);
        }
        if (collision.gameObject.CompareTag("ObstacleBig"))
        {
            Vector3 normal = collision.contacts[0].normal;

            rb.AddForce(-normal * bounceForce, ForceMode.Impulse);

            rb.linearVelocity *= speedMultiplier;

            ScoreManager.instance.AddScore(50);
        }
        if (collision.gameObject.CompareTag("ObstacleSmall"))
        {
            Vector3 normal = collision.contacts[0].normal;

            rb.AddForce(-normal * bounceForce, ForceMode.Impulse);

            rb.linearVelocity *= speedMultiplier;

            ScoreManager.instance.AddScore(200);
        }
    }
}
