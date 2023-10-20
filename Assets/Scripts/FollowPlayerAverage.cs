using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerAverage : MonoBehaviour
{
    [Header("Player Position")]
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    [SerializeField] float upMore;

    float averageY;
    float averageX; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        averageY = (p1.transform.position.y + p2.transform.position.y)/2 + upMore;
        averageX = (p1.transform.position.x + p2.transform.position.x) / 2;
        Vector3 cameraFollowPosition = new Vector3 (averageX, averageY, 0f);
        transform.position = cameraFollowPosition;
    }
}
