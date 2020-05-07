using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Photon.Voice.Unity;
using Photon.Voice.PUN;

namespace Com.Roel.ClassroomVR
{
    public class RPC : MonoBehaviour
    {
        private PhotonView photonView;
        [PunRPC]
        void MuteRecorder(bool allStudents)
        {
            Debug.Log("Handling request");
            if (allStudents) {
                if (this.gameObject.tag == "Student") {
                    GameObject studentObject = this.gameObject;
                    Debug.Log("Photon Voice: Muting student: ");
                    Debug.Log(PhotonNetwork.LocalPlayer.NickName);

                    studentObject.GetComponent<Recorder>().TransmitEnabled = false;
                }
            }
        }

        void UnmuteRecorder(bool allStudents)
        {
            if (allStudents) {
                if (this.gameObject.tag == "Student") {
                    // GameObject studentObject = this.gameObject;
                    GameObject studentObject = PlayerManager.LocalPlayerInstance.gameObject;
                    Debug.LogFormat("Photon Voice: Muting student: {0}", PhotonNetwork.LocalPlayer.NickName);
                    studentObject.GetComponent<Recorder>().TransmitEnabled = true;
                }
            }
        }

        
        public void MuteStudents() {
            viewCheck();
            // photonView.RPC("MuteRecorder", RpcTarget.AllBufferedViaServer, new object[] { 42, true });
            photonView.RPC("MuteRecorder", RpcTarget.AllBufferedViaServer, new object[] {true});
        }

        public void UnmuteStudents() {
            viewCheck();
            photonView.RPC("UnmuteRecorder", RpcTarget.AllBufferedViaServer, new object[] {true});
        }

        // Check if the PhotonView cache is recent enough
        private void viewCheck() {
            photonView = PhotonView.Get(this);
            if (photonView.ViewID < 1) {
                Debug.Log("Invalid PhotonView, Refreshing...");
                photonView = PlayerManager.LocalPlayerInstance.GetPhotonView();
                if (photonView.ViewID > 0) {
                    Debug.Log("PhotonView succesfully updated");
                } else {
                    Debug.Log("PhotonView update FAILED!");
                    return;
                }
            }
        }
    }
}
