using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public Transform warpDestination;
    public bool warpToNewScene = false;
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (warpToNewScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
            else
            {
                other.transform.position = warpDestination.position;
            }
        }
    }
}