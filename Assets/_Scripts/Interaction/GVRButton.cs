using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Com.Roel.ClassroomVR
{
    public class GVRButton : MonoBehaviour
    {
        public Image imgGaze;
        public UnityEvent GVRClick;
        public float totalTime = 2;
        bool gvrStatus;
        public float gvrTimer;

        // Update is called once per frame
        void Update()
        {
            if (gvrStatus) {
                gvrTimer += Time.deltaTime;
                imgGaze.fillAmount = gvrTimer / totalTime;
            }

            if (gvrTimer > totalTime) {
                GVRClick.Invoke();
                gvrTimer = 0;
            }
        }

        public void GvrOn() {
            gvrStatus = true;
        }

        public void GvrOff() {
            gvrStatus = false;
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}

