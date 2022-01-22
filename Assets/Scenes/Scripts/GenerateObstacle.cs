using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Scripts
{
    public class GenerateObstacle : MonoBehaviour
    {
        public GameObject CubeObj;
        public GameObject RiMGameObject;
        public List<GameObject> obstacleList;
        private List<GameObject> _gameObjects;
        private Vector3 Pos;
        private bool next;
        private int value = 1;
        public int lastpos = 1;
        public float[] posX;
        public float[] posZ;
        private int[,]   obstacleMap;
        private const int ObstacleMapLength = 1000;
        private const int ObstacleMapLanes = 3;
        private const int ObstacleStartId = 2;

        private void Start()
        {
            GameManager.Obstacle = this;
            InitMap();
        }

        private void InitMap()
        {
            _gameObjects = new List<GameObject>();
            obstacleMap = new int[ObstacleMapLength,ObstacleMapLanes];
            GenerateRims();
            FillMap();
        }

        public void Reset()
        {
            foreach (var o in _gameObjects)
            {
                Destroy(o);
            }
            InitMap();
        }

        private void FillMap()
        {
            var startOffset = (int)(ObstacleMapLength * 0.05);
            var endOffset = 15;
            var curObstacleOffset = GetRandomOffsetObstacleNumber();;
            for (var i = startOffset; i < ObstacleMapLength-endOffset; i++)
            {
                if (curObstacleOffset == 0)
                {
                    //Place obstacle here
                    curObstacleOffset=PlaceRandomObstacle(i,GetRandomLaneNumber());
                    curObstacleOffset += GetRandomOffsetObstacleNumber();
                }
                curObstacleOffset--;
            }
        }

        private void GenerateRims()
        {
            var maxRims = 5;
            var step = ObstacleMapLength/maxRims;
            var range = 50;
            for (var i = 1; i <= maxRims; i++)
            {
                var pos=Random.Range((step*i) - range, (step*i) + range);
                PlaceRim(pos);
            }
        }
    
        private int PlaceRim(int pos)
        {
            var lane = 1;
            var randomObjectNum = 1;
            var randomObject = RiMGameObject;
            float lengthObject = 50;
            Debug.Log("RimLeng="+lengthObject);
            if(!IsMapPositionsEmpty(pos,pos+ (int)lengthObject)) return 0;
        
            for (var i = -((int)lengthObject/2); i < (int)lengthObject/2; i++)
            {
                if (pos + i < obstacleMap.Length)
                {
                    obstacleMap[pos+i, lane] = randomObjectNum;
                }
            }
            float posY = 0.1F;
            Vector3 Pos = new Vector3(0,posY,pos);
            GameObject clone = Instantiate(randomObject, Pos, randomObject.transform.rotation);
            clone.transform.SetParent(this.transform);
            Debug.Log("rimpos="+clone.transform.position.z);
            _gameObjects.Add(clone);
            return (int) lengthObject;
        }

        private int PlaceRandomObstacle(int pos, int lane)
        {
            var randomObjectNum = GetRandomObstacleNumber();
            var randomObject = GetRandomObject(randomObjectNum);
            float lengthObject = GetMaxSizeObstacle(randomObject);
        
            if (!IsMapPositionsEmpty(pos,pos+ (int)lengthObject)) return 0;
            for (int i = 0; i < lengthObject; i++)
            {
                if (pos + i < obstacleMap.Length)
                {
                    obstacleMap[pos+i, lane] = randomObjectNum;
                }
            
            }
            float posY = 0.8F;
            if (randomObjectNum == (ObstacleStartId + obstacleList.Count-1))
            {
                posY = 0F;
            }
            Vector3 Pos = new Vector3(posX[lane],posY,pos);
       
            GameObject clone = Instantiate(randomObject, Pos, randomObject.transform.rotation);
            clone.GetComponent<ObstacleScript>().myNum=randomObjectNum;
            clone.transform.SetParent(this.transform);
            _gameObjects.Add(clone);
        
            return (int) lengthObject;
        }

        private GameObject GetRandomObject(int num)
        {
            return obstacleList[num - ObstacleStartId];
        }

        private bool IsMapPositionsEmpty(int startPos, int endPos)
        {
            for (int i = startPos; i < endPos; i++)
            {
                if (!IsMapPositionEmpty(i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsMapPositionEmpty(int pos)
        {
            if (pos < ObstacleMapLength)
            {
                return obstacleMap[pos, 0] == 0 && obstacleMap[pos, 1] == 0 && obstacleMap[pos, 2] == 0;
            }
            return false;
        }

        private int GetRandomLaneNumber()
        {
            return Random.Range(0, ObstacleMapLanes);
        }
    
        private int GetRandomObstacleNumber()
        {
            return Random.Range(ObstacleStartId, ObstacleStartId+obstacleList.Count);
        }
    
        private int GetRandomOffsetObstacleNumber()
        {
            return Random.Range(5, 20);
        }

        IEnumerator WaitSys()
        {
            yield return new WaitForSeconds(1f);
            next = true;
            Generate();
        }
    

        void Generate()
        {
            if (!next)
            {
                return;
            }
            int i = Random.Range(0, 3);
            Pos.x = posX[i];
            Pos.z += posZ[i];
            GameObject cubeClone = Instantiate(CubeObj, Pos, CubeObj.transform.rotation);
            cubeClone.GetComponent<CubeScript>().myNum=value;
            cubeClone.transform.SetParent(this.transform);
            value += 1;
            next = false;
        }

        public void Message(int i)
        {
            if (lastpos == i)
            {
                lastpos += 1;
                Debug.Log("FOUND!");
            }
            else
            {
                Debug.Log("NOT FOUND!");
            }
        }
    
        private Bounds GetBoundsOfObject(Transform objectTransform)
        {
            var meshFilter = objectTransform.GetComponentInChildren<MeshFilter>();
        
            var result = meshFilter != null ? meshFilter.sharedMesh.bounds : new Bounds();
 
            foreach (Transform transform in objectTransform)
            {
                var bounds = GetTotalMeshFilterBounds(transform);
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
 
            foreach (Transform transform in objectTransform)
            {
                var bounds = GetTotalMeshFilterBounds(transform);
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
        private float GetMaxSizeObstacle(GameObject gameObject)
        {
            var retVal = 0f;
            foreach (var componentsInChild in gameObject.GetComponentsInChildren<Renderer>())
            {
                retVal = Math.Max(componentsInChild.bounds.size.z, retVal);
            }
            return retVal;
        }
    
    }
}
