using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Com.Roel.ClassroomVR
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraWork : MonoBehaviourPun
    {
        // cached transform of the target
        private Transform _cameraTransform;
        private Transform _characterTransform;
        private Canvas _canvasWorld;
        private Canvas _canvasScreen;
        // Should the camera sync movement to the character
        private bool _syncCameraRotation = false;
        public bool turn = false;

        /// <summary>
        /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
        /// </summary>
        void Update() {
            if (photonView.IsMine) {
                SyncCameraPosition();
            }
        }

        public void AttachCamera() {
            Debug.Log("Attaching camera");
            _cameraTransform = Camera.main.transform;
            Transform _head = PlayerManager.LocalPlayerInstance.transform.Find("Head");
            if (_head != null) {
                _cameraTransform.SetParent(_head);
                // Now reattach the camera to the canvasses
    	        AttachCameraToCanvasses();
            } else {
                Debug.Log("Could not find head GameComponent to attach camera to");
            }
        }

        private void AttachCameraToCanvasses() {
            // Attach to worldspace canvas
            GameObject tempObject = GameObject.FindGameObjectWithTag("Canvas_world");
            if(tempObject != null){
                //If we found the object , get the Canvas component from it.
                _canvasWorld = tempObject.GetComponent<Canvas>();
                
                if(_canvasWorld == null){
                    Debug.Log("Could not locate worldspace Canvas component on " + tempObject.name);
                } else {
                    _canvasWorld.worldCamera = Camera.main;
                }
            }
            // Attach to screenspace canvas
            tempObject = GameObject.FindGameObjectWithTag("Canvas_screen");
            if(tempObject != null){
                //If we found the object , get the Canvas component from it.
                _canvasScreen = tempObject.GetComponent<Canvas>();
                if(_canvasScreen == null){
                    Debug.Log("Could not locate screenspace Canvas component on " + tempObject.name);
                } else {
                    _canvasScreen.worldCamera = Camera.main;
                }
            }
        }

        public void SyncCameraPosition() {
            _cameraTransform = Camera.main.transform;
            _characterTransform = PlayerManager.LocalPlayerInstance.transform;

            // Move camera to player
            Vector3 _moveTo = new Vector3(
                _characterTransform.localPosition.x,
                _characterTransform.localPosition.y + 1.5f,
                _characterTransform.localPosition.z
            );

            // Debug.Log("Moving to" + _moveTo);
            // Debug.Log("Move from" + _cameraTransform.localPosition);
            _cameraTransform.position = _moveTo;

            // Sync player head rotation with camera
            Vector3 _rotateTo = new Vector3(
                _cameraTransform.localEulerAngles.x*1,
                _cameraTransform.localEulerAngles.y*1,
                _cameraTransform.localEulerAngles.z*1
            );
            // _characterTransform.Find("Head").localEulerAngles = _rotateTo;
        }
    }
}
