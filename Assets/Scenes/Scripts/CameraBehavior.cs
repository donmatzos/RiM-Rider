using UnityEngine;

namespace Scenes.Scripts
{
    public class CameraBehavior : MonoBehaviour
    {
        public bool lockCamera;

        void LateUpdate()
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

 
    }
}
