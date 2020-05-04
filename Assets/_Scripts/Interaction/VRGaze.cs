using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Com.Roel.ClassroomVR
{
    public class VRGaze : MonoBehaviourPun
    {
        public Image imgGaze;
        public float totalTime = 2;
        public int distanceOfRay = 10;
        bool gvrStatus;
        float gvrTimer;
        private RaycastHit _hit;

        // Start is called before the first frame update
        void Start() {
            if (imgGaze == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Gaze image on player.", this);
            }
            if (photonView == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Photon View script on player.", this);
            }
        }

        // Update is called once per frame
        void Update() {
            // Dont do anything if this character is not controlled by the client
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                return;
            } else {
                if (gvrStatus) {
                    gvrTimer += Time.deltaTime;
                    imgGaze.fillAmount = gvrTimer / totalTime;
                }
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                if (Physics.Raycast(ray, out _hit, distanceOfRay)) {
                    // Is the targeted object teleportable
                    if (imgGaze.fillAmount == 1 && _hit.transform.CompareTag("Teleport")) {
                        _hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
                    }
                }
            }
        }

        public void GVROn() {
            Debug.Log("Player gazing on");
            gvrStatus = true;
        }
        public void GVROff() {
            gvrStatus = false;
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
        }
    }
}
