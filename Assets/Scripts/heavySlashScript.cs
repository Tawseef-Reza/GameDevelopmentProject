using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavySlashScript : MonoBehaviour
{
    private Transform playerLocationH;
    private PlayerController player;
    // Start is called before the first frame update

    // Start is called before the first frame update
    void Awake()
    {
        playerLocationH = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>();
        player = GameObject.FindWithTag("RealPlayer").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void heavyUpdate()
    {
        /*if (player.direction < 0)
        {
            
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
           
            transform.position = new Vector3(playerLocationH.position.x - 2.4f, playerLocationH.position.y, playerLocationH.position.z);
            print(transform.position);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = playerLocationH.position;
            print(transform.position);
        }*/
        transform.position = playerLocationH.position;
        
    }
}
