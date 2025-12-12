using UnityEngine;

public class CubeStart : MonoBehaviour
{
    public GameObject flipperRoot; // tout ton flipper
    public GameObject startCube;   // le cube à cliquer

    void Start()
    {
        // Au départ, le flipper est invisible
        if (flipperRoot != null)
            flipperRoot.SetActive(false);
    }

    void OnMouseDown()
    {
        // Active le flipper
        if (flipperRoot != null)
            flipperRoot.SetActive(true);

        // Cache le cube
        if (startCube != null)
            startCube.SetActive(false);
    }
}
