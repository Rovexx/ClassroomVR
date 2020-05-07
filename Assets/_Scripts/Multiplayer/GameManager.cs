using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.Roel.ClassroomVR
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields*
        public static GameManager Instance;
        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        #endregion

        #region Photon Callbacks
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Public Methods
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion

        #region Private Methods
        void Start()
        {
            // in case we started this with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("Wrong scene loaded, Returning to Launcher");
                SceneManager.LoadScene("Launcher");
                return;
            }
            Instance = this;

            if (playerPrefab == null){
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
            } else {
                if (PlayerManager.LocalPlayerInstance == null){
                    if (PhotonNetwork.IsMasterClient) {
                        Debug.Log("I am the teacher");
                        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,8f), Quaternion.identity, 0);
                        PlayerManager.LocalPlayerInstance.gameObject.tag = "Teacher";
                        // Debug.Log(PlayerManager.LocalPlayerInstance.GetPhotonView().ViewID);
                    } else {
                        Debug.LogFormat("I am a student");
                        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,-0.7f), Quaternion.identity, 0);
                        PlayerManager.LocalPlayerInstance.gameObject.tag = "Student";
                    }
                    // TODO
                    // Set character position random
                    // TODO
                    // Change master client (teacher) avatar
                } else {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        void LoadRoom()
        {
            // Load a bigger class if neccesairy
            if (PhotonNetwork.CurrentRoom.PlayerCount > 5) {
                Debug.Log("PhotonNetwork : Loading Level : Class 2");
                PhotonNetwork.LoadLevel("Class 2");
                // TODO change voice channel for these so they dont talk to class 1
            } else {
                Debug.Log("PhotonNetwork : Loading Level : Class 1");
                PhotonNetwork.LoadLevel("Class 1");
            }
        }
        #endregion

        #region Photon Callbacks
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadRoom();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadRoom();
            }
        }
        #endregion

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            // "back" button of phone equals "Escape". quit app if that's pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
