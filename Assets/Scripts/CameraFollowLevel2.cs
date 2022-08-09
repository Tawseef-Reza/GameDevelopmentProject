using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLevel2 : MonoBehaviour
{
    //private bool firstTouch;
    public Transform playerheading;
    // Start is called before the first frame update
    void Start()
    {
        playerheading = GameObject.Find("Heading").GetComponent<Transform>();

    }
    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(playerheading.position.x, -3.1f, 104.62f), Mathf.Clamp(playerheading.position.y, -16.5f, -16.5f), transform.position.z);
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
