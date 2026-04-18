using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    public HingeJoint2D connection;

    public Transform post;
    public LineRenderer lr; 

    public bool allowRestart = false;

    void Start()
    {
        allowRestart = false;
        lr = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        if (lr != null)
        {
            lr.SetPosition(0, post.position);
            lr.SetPosition(1, transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(lr);
            Destroy(connection);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
    
            RestartTheGame(); 
        }
    }

    void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
