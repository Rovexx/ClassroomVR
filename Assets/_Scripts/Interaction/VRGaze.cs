using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Com.Roel.ClassroomVR
{
    public class VRGaze : MonoBehaviourPun
    {
        [Tooltip("Image for gaze interaction")]
        public Image imgGaze;
        [Tooltip("How much time, in seconds, does it take to activate")]
        public float totalTime = 2;
        [Tooltip("This is the tag that will be searched for on the player to check if it has the required permissions to activate this")]
        public string requiredPermissionTag = "None";
        [Tooltip("Distance to check")]
        public int distanceOfRay = 10;

        private bool _gvrStatus = false;
        private float _gvrTimer;
        private RaycastHit _hit;
        private GameObject _gazedObject;
        private _targetTypes _targetType;
        private enum _targetTypes
        {
            GAZEBUTTON,
            TELEPORT
        }

        void Start() {
            if (imgGaze == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Gaze image on player.", this);
            }
            if (photonView == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Photon View script on player.", this);
            }
        }

        void Update() {
            // Dont do anything if this character is not controlled by the client
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                return;
            } else {
                if (_gvrStatus) {
                    _gvrTimer += Time.deltaTime;
                    imgGaze.fillAmount = _gvrTimer / totalTime;
                }
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                if (Physics.Raycast(ray, out _hit, distanceOfRay)) {
                    // Is the targeted object teleportable
                    if (_hit.transform.CompareTag("Teleport")) {
                        _targetType = _targetTypes.TELEPORT;
                        _gazedObject = _hit.transform.gameObject;
                        
                    }
                    // Is the targeted object a button
                    if (_hit.transform.CompareTag("GazeButton")) {
                        _targetType = _targetTypes.GAZEBUTTON;
                        _gazedObject = _hit.transform.gameObject;
                    }
                }
                // Invoke action on a complete gaze
                if (_gvrTimer > totalTime) {
                    switch (_targetType)
                    {
                        case _targetTypes.GAZEBUTTON:
                            Debug.Log("Button");
                            _gazedObject.GetComponent<Button>();
                            break;
                        case _targetTypes.TELEPORT:
                            Debug.Log("Teleport");
                            _gazedObject.GetComponent<Teleport>().TeleportPlayer();
                            break;
                    }
                    _gvrTimer = 0;
                }
            }
        }

        public void GVROn() {
            _gvrStatus = true;
        }
        public void GVROff() {
            _gvrStatus = false;
            _gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}
