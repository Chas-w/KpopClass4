using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] Collider2D zoomInTrigger;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float movePlayerDist = 10f;

    float playerDist;
    float minDist = 5f;
    

    bool zoomingOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        playerDist = Vector3.Distance(player1.position, player2.position);
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        if (playerDist > followCam.m_Lens.OrthographicSize)
        {
            followCam.m_Lens.OrthographicSize += zoomSpeed;
        }
        /*
        else if (playerDist < followCam.m_Lens.OrthographicSize && playerDist >= minDist)
        {
            followCam.m_Lens.OrthographicSize -= zoomSpeed;
        }
        */
    }
}
