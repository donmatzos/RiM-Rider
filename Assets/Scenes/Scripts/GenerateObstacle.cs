using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GenerateObstacle : MonoBehaviour
{
    public GameObject CubeObj;
    public List<GameObject> obstacleList;
    private Vector3 Pos;
    private bool next;
    private int value = 1;
    public int lastpos = 1;
    public float[] posX;
    public float[] posZ;
    // Update is called once per frame
    private int[,]   obstacleMap;
    private const int obstacleMapLength = 1000;
    private int obstacleMapLanes = 3;
    private int obstacleStartId = 2;
    void FixedUpdate()
    {
      //  StartCoroutine(WaitSys());
    }

    private void Start()
    {
        InitMap();
    }

    private void InitMap()
    {
        obstacleMap = new int[obstacleMapLength,obstacleMapLanes];
        FillMap();
    }

    private void FillMap()
    {
        var startOffset = (int)(obstacleMapLength * 0.1);
        var curObstacleOffset = GetRandomOffsetObstacleNumber();;
        for (var i = startOffset; i < obstacleMapLength; i++)
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

    private int PlaceRandomObstacle(int pos, int lane)
    {
        
        var randomObjectNum = GetRandomObstacleNumber();
        
        var randomObject = GetRandomObject(randomObjectNum);
        float lengthObject = GetMaxSizeObstacle(randomObject);
        
        if(!IsMapPositionsEmpty(pos,pos+ (int)lengthObject)) return 0;
        
        for (int i = 0; i < lengthObject; i++)
        {
            if (pos + i < obstacleMap.Length)
            {
                obstacleMap[pos+i, lane] = randomObjectNum;
            }
            
        }
        float posY = 0.8F;
        if (randomObjectNum == (obstacleStartId + obstacleList.Count-1))
        {
            posY = 0F;
        }
        Vector3 Pos = new Vector3(posX[lane],posY,pos);
       
        GameObject clone = Instantiate(randomObject, Pos, randomObject.transform.rotation);
        clone.GetComponent<ObstacleScript>().myNum=randomObjectNum;
        clone.transform.SetParent(this.transform);
      
        
        return (int) lengthObject;
    }

    private GameObject GetRandomObject(int num)
    {
        return obstacleList[num - obstacleStartId];
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
        return obstacleMap[pos, 0] == 0 && obstacleMap[pos, 1] == 0 && obstacleMap[pos, 2] == 0;
    }

    private int GetRandomLaneNumber()
    {
       return Random.Range(0, obstacleMapLanes);
    }
    private int GetRandomObstacleNumber()
    {
        return Random.Range(obstacleStartId, obstacleStartId+obstacleList.Count);
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
