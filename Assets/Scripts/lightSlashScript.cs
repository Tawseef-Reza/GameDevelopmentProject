using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSlashScript : MonoBehaviour
{
    private Transform playerLocationL;
    private PlayerController player;
    // Start is called before the first frame update
    
    void Awake()
    {
        playerLocationL = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>();
        player = GameObject.FindWithTag("RealPlayer").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void lightUpdate()
    {
        
        transform.position = playerLocationL.position;
    }
}
