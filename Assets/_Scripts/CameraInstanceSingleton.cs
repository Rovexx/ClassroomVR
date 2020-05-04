using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInstanceSingleton : MonoBehaviour
{
    public static CameraInstanceSingleton instance = null;

    void Start ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
