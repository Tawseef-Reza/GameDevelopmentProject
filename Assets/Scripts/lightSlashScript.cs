using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSlashScript : MonoBehaviour
{
    private Transform playerLocationL;
    private PlayerController player;
    // Start is called before the first frame update

    // lightslashcollider Vector3(-15.1000004,-20.5300007,0)
    // player Vector3(-15.1000004,-20.5300007,0)
    // lightslashcollider Vector3(-15.1000004,-20.5300007,0)
    void Awake()
    {
        playerLocationL = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>();
        player = GameObject.FindWithTag("RealPlayer").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void lightUpdate()
    {
        if (player.direction < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationL.position.x, playerLocationL.position.y, playerLocationL.position.z);
                
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationL.position.x, playerLocationL.position.y, playerLocationL.position.z);
                
            }
        }
        else if (player.direction > 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationL.position.x, playerLocationL.position.y, playerLocationL.position.z);
                
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationL.position.x, playerLocationL.position.y, playerLocationL.position.z);
                
            }
        }
        else
        {

        }
    }
}
