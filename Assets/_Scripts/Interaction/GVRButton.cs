﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Com.Roel.ClassroomVR
{
    public class GVRButton : MonoBehaviour
    {
        [Tooltip("Image for gaze interaction")]
        public Image imgGaze;
        [Tooltip("How much time, in seconds, does it take to activate")]
        public float totalTime = 2;
        [Tooltip("This is the tag that will be searched for on the player to check if it has the required permissions to activate this")]
        public string requiredPermissionTag = "None";
        [Tooltip("UnityEvent to trigger on gaze completion")]
        public UnityEvent GVRClick;
        [Tooltip("Distance to check")]
        public int distanceOfRay = 10;

        private bool _gvrStatus = false;
        private float _gvrTimer;

        void Start() {
            if (imgGaze == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> imgGaze Reference",this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_gvrStatus) {
                _gvrTimer += Time.deltaTime;
                imgGaze.fillAmount = _gvrTimer / totalTime;
            }

            if (_gvrTimer > totalTime) {
                GVRClick.Invoke();
                _gvrTimer = 0;
            }
        }

        public void GvrOn() {
            if (PlayerManager.LocalPlayerInstance.tag == requiredPermissionTag || requiredPermissionTag == "None") {
                _gvrStatus = true;
            }
        }

        public void GvrOff() {
            _gvrStatus = false;
            _gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}

