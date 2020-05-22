using UnityEngine;

namespace Com.Roel.ClassroomVR
{
    public class AllowedPermissions : MonoBehaviour 
    {
        [SerializeField]
        private string requiredPermission = "None";

        public string GetRequiredPermission()
        {
            return requiredPermission;
        }
    }
}
