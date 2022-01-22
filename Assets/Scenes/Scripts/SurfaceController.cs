using System;
using UnityEngine;

namespace Scenes.Scripts
{
    public class SurfaceController : MonoBehaviour
    {
        public HouseScript house;
    
        // Start is called before the first frame update
        void Start()
        {
            Generate();  
        }

        void Generate()
        {
            float lengthSurface = GetTotalMeshFilterBounds(transform).size.z;
            float lengthHouse = GetMaxSizeHouse();
            float y = transform.position.y;
            float xLeft = transform.position.x-5.8F;
            float xRight = transform.position.x+5.8F;
            float z = 0;
            for (int i = 0; i < (lengthSurface/lengthHouse); i++)
            {
                Vector3 posLeft = new Vector3(xLeft, y, z);
                Vector3 posRight = new Vector3(xRight, y, z);
                Instantiate(house, posLeft, Quaternion.Euler(0f,270f,0f));
                Instantiate(house, posRight, Quaternion.Euler(0f,90f,0f));
                z += lengthHouse;
            }
        }

        private float GetMaxSizeHouse()
        {
            var retVal = 0f;
            foreach (var componentsInChild in house.GetComponentsInChildren<Renderer>())
            {
                retVal = Math.Max(componentsInChild.bounds.size.x, retVal);
            }
            return retVal;
        }

        private Bounds GetBoundsHouse(Transform objectTransform)
        {
            var meshFilter = objectTransform.GetComponentInChildren<MeshFilter>();
        
            var result = meshFilter != null ? meshFilter.sharedMesh.bounds : new Bounds();
 
            foreach (Transform transf in objectTransform)
            {
                var bounds = GetTotalMeshFilterBounds(transf);
                result.Encapsulate(bounds.min);
                result.Encapsulate(bounds.max);
            }
            var scaledMin = result.min;
            scaledMin.Scale(objectTransform.localScale);
            result.min = scaledMin;
            var scaledMax = result.max;
            scaledMax.Scale(objectTransform.localScale);
            result.max = scaledMax;
            return result;
        }
        private Bounds GetTotalMeshFilterBounds(Transform objectTransform)
        {
            var meshFilter = objectTransform.GetComponent<MeshFilter>();
            var result = meshFilter != null ? meshFilter.mesh.bounds : new Bounds();
 
            foreach (Transform transf in objectTransform)
            {
                var bounds = GetTotalMeshFilterBounds(transf);
                result.Encapsulate(bounds.min);
                result.Encapsulate(bounds.max);
            }
            var scaledMin = result.min;
            scaledMin.Scale(objectTransform.localScale);
            result.min = scaledMin;
            var scaledMax = result.max;
            scaledMax.Scale(objectTransform.localScale);
            result.max = scaledMax;
            return result;
        }
    }
}
