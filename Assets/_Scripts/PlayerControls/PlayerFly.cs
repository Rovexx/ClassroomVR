using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    public Transform myCam;
    float moveX = 0f;
    float moveY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If head is tilted forward between 5 and 45 degrees
        if(myCam.rotation.eulerAngles.x > 40f && myCam.rotation.eulerAngles.x < 60f) {
            moveX++;
        }
        if(myCam.rotation.eulerAngles.x > 310f && myCam.rotation.eulerAngles.x < 330f) {
            moveX--;
        }

        // If head is tilted to the side
        if(myCam.rotation.eulerAngles.z > 40f && myCam.rotation.eulerAngles.z < 60f) {
            moveY--;
        }
        if(myCam.rotation.eulerAngles.z > 310f && myCam.rotation.eulerAngles.z < 330f) {
            moveY++;
        }
        moveX *= Time.deltaTime;
        moveY *= Time.deltaTime;

        this.transform.Translate(myCam.forward * moveX);
        this.transform.Translate(myCam.right * moveY);
        // Add some smoothing for accelaration
        moveX *= 0.996f;
        moveY *= 0.996f;
    }
}
