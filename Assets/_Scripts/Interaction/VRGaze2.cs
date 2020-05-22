using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;

namespace Com.Roel.ClassroomVR
{
    public class VRGaze2 : MonoBehaviourPun
    {
        [Tooltip("Image for gaze interaction")]
        public Image imgGaze;
        [Tooltip("How much time, in seconds, does it take to activate")]
        public float totalTime = 2;        
        [Tooltip("Distance to check")]
        public int distanceOfRay = 10;
        [Tooltip("Toggles drawing of debug raycast")]
        public bool DrawDebugRay = false;

        private string _requiredPermissionTag = "None";
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
        private PhotonView playerPhotonView;// This can be replaced by using this.photonview once this script is on the player

        void Start() {
            playerPhotonView = PlayerManager.LocalPlayerInstance.GetPhotonView();
            
            if (imgGaze == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Gaze image on player.", this);
            }
            if (playerPhotonView == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Photon View script on player.", this);
            }
        }

        void Update() {
            // if (playerPhotonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            //     Debug.Log("LOGGING: Not my player");
            //     return;
            // } else {
            // Increase gaze circle
            if (_gvrStatus) {
                _gvrTimer += Time.deltaTime;
                imgGaze.fillAmount = _gvrTimer / totalTime;
            }

            // Cast a ray
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (DrawDebugRay) {
                Debug.DrawRay(ray.origin, ray.direction*100, Color.green);
            }

            // Check for ray colisions
            if (Physics.Raycast(ray, out _hit, distanceOfRay)) {
                // Is the targeted object teleportable
                if (_hit.transform.CompareTag("Teleport")) {
                    _targetType = _targetTypes.TELEPORT;
                    _gazedObject = _hit.transform.gameObject;
                    GVROn();
                }
                // Is the targeted object a button
                else if (_hit.transform.CompareTag("GazeButton")) {
                    _targetType = _targetTypes.GAZEBUTTON;
                    _gazedObject = _hit.transform.gameObject;
                    GVROn();
                } else {
                    GVROff();
                }
            } else {
                GVROff();
            }

            // Invoke action on a complete gaze
            if (_gvrTimer > totalTime) {
                switch (_targetType)
                {
                    case _targetTypes.GAZEBUTTON:
                        Debug.Log("Button");
                        _gazedObject.GetComponent<Button>().onClick.Invoke();
                        break;
                    case _targetTypes.TELEPORT:
                        Debug.Log("Teleport");
                        _gazedObject.GetComponent<Teleport>().TeleportPlayer();
                        break;
                }
                _gvrTimer = 0;
            }
        }

        public void GVROn() {
            _requiredPermissionTag = _gazedObject.GetComponent<AllowedPermissions>().GetRequiredPermission();
            if (_requiredPermissionTag == null) {
                Debug.Log("Missing permissions");
            } else {
                if (PlayerManager.LocalPlayerInstance.tag == _requiredPermissionTag || _requiredPermissionTag == "None")
                {
                    _gvrStatus = true;
                }
            }
        }
        public void GVROff() {
            _gvrStatus = false;
            _gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}
