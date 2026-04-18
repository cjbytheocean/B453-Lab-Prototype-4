using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallManager : MonoBehaviour
{
    public HingeJoint2D connection;
    public Transform post;
    public LineRenderer lr; 
    public GameObject goal;

    public bool allowRestart = false;
    public bool hitGoal = false;

    [SerializeField] TextMeshProUGUI resultText;

    Vector3[] spawnPositions = {
        new Vector3(-6f, -4f, 0f),
        new Vector3(-4f, -4f, 0f),
        new Vector3(-2f, -4f, 0f),
        new Vector3(2f, -4f, 0f),
        new Vector3(4f, -4f, 0f),
        new Vector3(6f, -4f, 0f)
    };

    void Start()
    {
        allowRestart = false;
        lr = GetComponent<LineRenderer>();
        SetGoalPosition();
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
            resultText.text = $"";
            StartCoroutine(SetText());
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

    IEnumerator SetText()
    {
        yield return new WaitForSeconds(2f);

        if (hitGoal)
        {
            resultText.text = $"You Win! Press 'R' to Restart.";
            allowRestart = true;
        }
        else
        {
            resultText.text = $"You Lose! Press 'R' to Retry."; 
            allowRestart = true;
        }
    }

    public void SetGoalPosition()
    {
        int rIndex = Random.Range(0, spawnPositions.Length);
        Instantiate(goal, spawnPositions[rIndex], Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Goal"))
        {
            hitGoal = true;
        }
    }
}
