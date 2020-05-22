using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Com.Roel.ClassroomVR
{
    public class GVRButtonOffline : MonoBehaviour
    {
        [Tooltip("Image for gaze interaction")]
        public Image imgGaze;
        [Tooltip("How much time, in seconds, does it take to activate")]
        public float totalTime = 2;
        [Tooltip("UnityEvent to trigger on gaze completion")]
        public UnityEvent GVRClick;

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
            _gvrStatus = true;
        }

        public void GvrOff() {
            _gvrStatus = false;
            _gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}

