using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

namespace Com.Roel.ClassroomVR
{
    public class VoiceManager : MonoBehaviourPun
    {
        // Start is called before the first frame update
        // private PhotonVoiceNetwork punVoiceNetwork;
        void Start()
        {
            // this.punVoiceNetwork = PhotonVoiceNetwork.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // private GameObject[] studentObjects;
        private Photon.Realtime.Player[] studentObjects;
        public void muteStudents() {
            // studentObjects = PhotonNetwork.PlayerListOthers;
            // studentObjects = GameObject.FindGameObjectsWithTag("Student");
            // foreach (var item in studentObjects)
            // {
            //     Debug.Log(item);
            // }

            // if (studentObjects.Length > 0) {
            //     foreach (var student in studentObjects)
            //     {
            //         Debug.Log("Muting student");
            //         Debug.Log(student.GetComponent<Recorder>().TransmitEnabled);
            //         student.GetComponent<Recorder>().TransmitEnabled = false;
            //         Debug.Log(student.GetComponent<Recorder>().TransmitEnabled);
            //     }
            // } else {
            //     Debug.Log("No students to mute");
            // }
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("MyRemoteMethod", RpcTarget.AllBufferedViaServer, new object[] { 42, true });
        }
    }
}
