using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private bool firstTouch;
    public Transform playerheading;
    // Start is called before the first frame update
    void Start()
    {
        playerheading = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>().GetChild(1);

    }
    // Update is called once per frame
    void Update()
    {
        
        /*if (playerheading == null)
        {
            playerheading = GameObject.FindWithTag("RealPlayer").GetComponent<Transform>().GetChild(1);
            print("playerheading set");
        }*/
        transform.position = new Vector3(Mathf.Clamp(playerheading.position.x, -6.52f, 166.6f), Mathf.Clamp(playerheading.position.y, -19, -16.14f), transform.position.z);
        /*print(transform.position.x + " is transform.position.x, "+ transform.position.y + " is transform.position.y");
        
        if (transform.position.x < player.position.x)
        {
            firstTouch = true;
        }
        if (transform.position.x >= -12.89f && transform.position.x <= 17.2f && transform.position.y <= 6.88 && firstTouch)
        {
            print("inside code is running");
            transform.position = new Vector3(player.position.x, player.position.y + 2.1f, transform.position.z);
        }*/
    }
}
