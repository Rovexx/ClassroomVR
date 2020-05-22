using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Roel.ClassroomVR 
{
    public class Teleport : MonoBehaviour
    {
        [Tooltip("Position offset on X axis to apply after teleport")]
        public float teleportPositionOffsetX;
        [Tooltip("Position offset on Y axis to apply after teleport")]
        public float teleportPositionOffsetY;
        [Tooltip("Position offset on Z axis to apply after teleport")]
        public float teleportPositionOffsetZ;
        [Tooltip("Rotation correction on X axis to apply after teleport. Use to face the player in the correct direction")]
        public float teleportRotationX;
        [Tooltip("Rotation correction on X axis to apply after teleport. Use to face the player in the correct direction")]
        public float teleportRotationY;
        [Tooltip("Rotation correction on X axis to apply after teleport. Use to face the player in the correct direction")]
        public float teleportRotationZ;

        public void TeleportPlayer() {
            Debug.Log("Teleport");
            Vector3 teleportLocation = new Vector3(
                transform.position.x + teleportPositionOffsetX, 
                transform.position.y + teleportPositionOffsetY, 
                transform.position.z + teleportPositionOffsetZ
            );
            Vector3 teleportRotation = new Vector3 (
                teleportRotationX, 
                teleportRotationY, 
                teleportRotationZ
            );

            // Teleport the local player instance to the gazed at location
            PlayerManager.LocalPlayerInstance.transform.localPosition = teleportLocation;
            // Correct view direction based on target object rotation
            PlayerManager.LocalPlayerInstance.transform.localRotation = Quaternion.Euler(teleportRotation);
        }
    }
}

