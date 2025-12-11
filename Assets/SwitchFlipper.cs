using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeLoadScene : MonoBehaviour
{
    public string sceneName;

    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
