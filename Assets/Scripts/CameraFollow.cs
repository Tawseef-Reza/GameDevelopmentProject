using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool firstTouch;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerMan").GetComponent<Transform>();
        firstTouch = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(player.position.x, -13, 17.5f), Mathf.Clamp(player.position.y, 5, 7), transform.position.z);
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