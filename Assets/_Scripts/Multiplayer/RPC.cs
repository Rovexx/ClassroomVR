using UnityEngine;

using Photon.Pun;

using Photon.Voice.Unity;
using Photon.Voice.PUN;

namespace Com.Roel.ClassroomVR
{
    public class RPC : MonoBehaviourPun
    {
        private PhotonView _photonView;

        void start() {
            // _photonView = PlayerManager.LocalPlayerInstance.GetPhotonView();
            // Debug.Log(PhotonView.Get(this));
            // Debug.Log(_photonView);
            _photonView = this.photonView;
        }

        [PunRPC]
        void MuteRecorder(bool allStudents)
        {
            Debug.Log("LOGGING: start mute");
            if (allStudents) {
                if (this.gameObject.tag == "Student") {
                    GameObject studentObject = this.gameObject;
                    Debug.Log("LOGGING: Photon Voice: Muting student: ");
                    Debug.Log(PhotonNetwork.LocalPlayer.NickName);
                    studentObject.GetComponent<Recorder>().TransmitEnabled = false;
                }
            }
        }

        [PunRPC]
        void UnmuteRecorder(bool allStudents)
        {
            Debug.Log("LOGGING: start unmute");
            if (allStudents) {
                if (this.gameObject.tag == "Student") {
                    // GameObject studentObject = this.gameObject;
                    GameObject studentObject = PlayerManager.LocalPlayerInstance.gameObject;
                    Debug.LogFormat("LOGGING: Photon Voice: Muting student: {0}", PhotonNetwork.LocalPlayer.NickName);
                    studentObject.GetComponent<Recorder>().TransmitEnabled = true;
                }
            }
        }

        public void MuteStudents() {
            viewCheck();
            // photonView.RPC("MuteRecorder", RpcTarget.AllBufferedViaServer, new object[] { 42, true });
            
            if (photonView.ViewID < 1) {
                return;
            } else {
                _photonView.RPC("MuteRecorder", RpcTarget.AllBufferedViaServer, new object[] {true});
            }
        }

        public void UnmuteStudents() {
            viewCheck();
            if (photonView.ViewID < 1) {
                return;
            } else {
                _photonView.RPC("UnmuteRecorder", RpcTarget.AllBufferedViaServer, new object[] {true});
            }
        }

        // Check if the PhotonView cache is recent enough
        private void viewCheck() {
            // _photonView = PlayerManager.LocalPlayerInstance.GetPhotonView();
            // Debug.Log(_photonView);
            Debug.Log(PhotonView.Get(this));
            if (photonView.ViewID < 1) {
                return;
            }
            // _photonView = PhotonView.Get(this);
            // if (_photonView.ViewID < 1) {
            //     Debug.Log("Invalid PhotonView, Refreshing...");
            //     _photonView = PlayerManager.LocalPlayerInstance.GetPhotonView();
            //     Debug.Log(_photonView);
            //     if (_photonView.ViewID > 0) {
            //         Debug.Log("PhotonView succesfully updated");
            //     } else {
            //         Debug.Log("PhotonView update FAILED!");
            //         return;
            //     }
            // }
        }
    }
}
