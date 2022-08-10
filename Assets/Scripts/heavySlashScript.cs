using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavySlashScript : MonoBehaviour
{
    private Transform playerLocationH;
    private PlayerController player;
    //private Vector3 stableScale;
    // Start is called before the first frame update

    // Start is called before the first frame update
    void Awake()
    {
        playerLocationH = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>();
        player = GameObject.FindWithTag("RealPlayer").GetComponent<PlayerController>();
        //stableScale = transform.localScale;
    }

    // Update is called once per frame
    public void heavyUpdate()
    {
        //heavySlashCollider Vector3(-12.7670879,-20.6295223,0)
        //player Vector3(-15.1000004,-20.5300007,0)
        //heavySlashCollider Vector3(-17.4799995,-20.5300007,0)
        if (player.direction < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationH.position.x - 2.38f, playerLocationH.position.y, playerLocationH.position.z);
                
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationH.position.x - 2.38f, playerLocationH.position.y, playerLocationH.position.z);
                
            }
        }
        else if (player.direction > 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationH.position.x + 2.333f, playerLocationH.position.y, playerLocationH.position.z);
                
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                transform.position = new Vector3(playerLocationH.position.x + 2.333f, playerLocationH.position.y, playerLocationH.position.z);
                
            }
        }
        else
        {
            
        }
        
        
    }
}
