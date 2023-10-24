using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LevelManager : MonoBehaviour
{
    public GameObject UIManager;

    [Header("player values")]
    public GameObject player1;
    public GameObject player2;
    public PlayerManager p1Health;
    public PlayerManager p2Health;
    public float p1WinCount;
    public float p2WinCount;

    [Header("world")]
    public timer levelTimer;
    public float sceneCount = 1;


    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {

    }

    void Update()
    {
        UIManager = GameObject.Find("UI Manager");
        player1 = GameObject.Find("player");
        player2 = GameObject.Find("player2");
        p1Health = player1.GetComponent<PlayerManager>();
        p2Health = player2.GetComponent<PlayerManager>();

        levelTimer = UIManager.GetComponent<timer>();
        /*
        p1Damage = p1.GetComponent<playerHealth>();
        p2Damage = p2.GetComponent<player2Health>();
        */
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        #region sceneManager
        if (levelTimer.currentTime <= 0 && p1Health.healthCurrent > p2Health.healthCurrent)
        {
            p2WinCount++;
            //sceneCount++;
            levelTimer.currentTime = levelTimer.startTime;
            if (SceneManager.GetActiveScene().name == "inClassPlatformer") { SceneManager.LoadScene("LVL2"); }
            if (SceneManager.GetActiveScene().name == "LVL2") { SceneManager.LoadScene("LVL3"); }
            if (SceneManager.GetActiveScene().name == "LVL3") 
            {
                if (p1WinCount > p2WinCount)
                {
                    SceneManager.LoadScene("Player1WinEnd");
                    Destroy(gameObject);
                }
                else if (p1WinCount < p2WinCount)
                {
                    SceneManager.LoadScene("Player2WinEnd");
                    Destroy(gameObject);
                }
            }
        }
        else if (levelTimer.currentTime <= 0 && p1Health.healthCurrent < p2Health.healthCurrent)
        {
            p1WinCount++;
            //sceneCount++;
            levelTimer.currentTime = levelTimer.startTime;
            Debug.Log(p1WinCount);
            if (SceneManager.GetActiveScene().name == "inClassPlatformer") {SceneManager.LoadScene("LVL2");}
            if (SceneManager.GetActiveScene().name == "LVL2") { SceneManager.LoadScene("LVL3"); }
            if (SceneManager.GetActiveScene().name == "LVL3")
            {
                if (p1WinCount > p2WinCount)
                {
                    //p2.myAnim.SetBool("dying", true);
                    SceneManager.LoadScene("Player1WinEnd");
                    Destroy(gameObject);
                }
                else if (p1WinCount < p2WinCount)
                {
                    SceneManager.LoadScene("Player2WinEnd");
                    Destroy(gameObject);
                }
            }
        }
        #endregion
        
    }
}
