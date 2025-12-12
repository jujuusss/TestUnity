using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    private Renderer rend;
    public float speed = 1f;

    private Color colorA = Color.yellow;
    private Color colorB = Color.red;

    private float t = 0f;
    private bool goingToB = true;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorA; // couleur de départ
    }

    void Update()
    {
        // Avance vers la couleur cible
        t += Time.deltaTime * speed;

        if (goingToB)
        {
            rend.material.color = Color.Lerp(colorA, colorB, t);

            // si on atteint la couleur B, on repart dans l'autre sens
            if (t >= 1f)
            {
                goingToB = false;
                t = 0f;
            }
        }
        else
        {
            rend.material.color = Color.Lerp(colorB, colorA, t);

            // lorsqu’on revient à A, on repart vers B
            if (t >= 1f)
            {
                goingToB = true;
                t = 0f;
            }
        }
    }
}
