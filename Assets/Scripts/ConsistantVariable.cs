using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistantVariable : MonoBehaviour
{
    // Start is called before the first frame update
    public float p1WinCount;
    public float p2WinCount;
    public float sceneCount = 1;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
