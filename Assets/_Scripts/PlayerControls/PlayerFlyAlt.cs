using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyAlt : MonoBehaviour
{
    public Transform myCam;
    public int moveX;
    public int moveY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If head is tilted forward between 5 and 45 degrees
        if(myCam.rotation.eulerAngles.x > 40f && myCam.rotation.eulerAngles.x < 60f) {
            moveX = 2;
        } else {
            moveX = 0;
        }
        if(myCam.rotation.eulerAngles.x > 310f && myCam.rotation.eulerAngles.x < 330f) {
            moveX = -2;
        } else {
            moveX = 0;
        }

        // If head is tilted to the side
        if(myCam.rotation.eulerAngles.z > 40f && myCam.rotation.eulerAngles.z < 60f) {
            moveY = 2;
        } else {
            moveY = 0;
        }
        if(myCam.rotation.eulerAngles.z > 310f && myCam.rotation.eulerAngles.z < 330f) {
            moveY = 2;
        } else {
            moveY = 0;
        }

        transform.position = transform.position + Camera.main.transform.forward * moveX * Time.deltaTime;
        // transform.position = transform.position + Camera.main.transform.right * moveY * Time.deltaTime;
    }
}
