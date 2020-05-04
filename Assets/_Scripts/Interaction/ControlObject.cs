using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate object on click
        if (Input.GetButtonDown ("Fire1")) {
            transform.Rotate(transform.rotation.eulerAngles + new Vector3 (0f, 0.1f, 0f));
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move in horizontal and vertical position
        transform.position += new Vector3(h * 0.05f, v * 0.05f, 0f);
    }
}
