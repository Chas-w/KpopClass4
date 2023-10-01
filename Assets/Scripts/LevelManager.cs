using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] timer levelTimer;
    [SerializeField] playerHealth p1Damage;
    [SerializeField] player2Health p2Damage;

    public float p1WinCount;
    public float p2WinCount;
    public float sceneCount = 1;


    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (levelTimer.currentTime <= 0 && p1Damage.damage > p2Damage.damage)
        {
            p2WinCount++;
            sceneCount++;
            levelTimer.currentTime = levelTimer.startTime;
            if (SceneManager.GetActiveScene().name == "inClassPlatformer") { SceneManager.LoadScene("LVL2"); }
            else if (SceneManager.GetActiveScene().name == "LVL2") { SceneManager.LoadScene("LVL3"); }
        }
        else if (levelTimer.currentTime <= 0 && p1Damage.damage < p2Damage.damage)
        {
            p1WinCount++;
            sceneCount++;
            levelTimer.currentTime = levelTimer.startTime;
            Debug.Log(p1WinCount);
            if (SceneManager.GetActiveScene().name == "inClassPlatformer") {SceneManager.LoadScene("LVL2");}
            else if (SceneManager.GetActiveScene().name == "LVL2") { SceneManager.LoadScene("LVL3"); }
        }
        
        if (sceneCount >= 3)
        {
            if (p2WinCount > p1WinCount)
            {   
                SceneManager.LoadScene("Player2WinEnd");
                Debug.Log("p2 Win");
                Destroy(this);
            }
            else if (p1WinCount > p2WinCount) ;
            {
                SceneManager.LoadScene("Player1WinEnd");
                Destroy(this);
            }
        }
    }
}
