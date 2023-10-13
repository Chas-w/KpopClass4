using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LevelManager : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject p1;
    public GameObject p2;

    public timer levelTimer;
    //public playerHealth p1Damage;
    //public player2Health p2Damage;

    public float p1WinCount;
    public float p2WinCount;
    //public float sceneCount = 1;


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
        p1 = GameObject.Find("player");
        p2 = GameObject.Find("player2");

        levelTimer = UIManager.GetComponent<timer>();
        /*
        p1Damage = p1.GetComponent<playerHealth>();
        p2Damage = p2.GetComponent<player2Health>();
        */
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        #region sceneManager
        if (levelTimer.currentTime <= 0 && p1Damage.damage > p2Damage.damage)
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
        else if (levelTimer.currentTime <= 0 && p1Damage.damage < p2Damage.damage)
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
        */
    }
}
