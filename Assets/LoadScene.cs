using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform target;
    public int sceneNumber;
    // Update is called once per frame
    void Update()
    {
        if (target.position.x > transform.position.x 
            && ( target.position.y < (transform.position.y + 1) && target.position.y > (transform.position.y - 1)))
            SceneManager.LoadScene(sceneNumber);
    }
}
