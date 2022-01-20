using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int myNum;
    private GenerateObstacle generate;
    private Renderer rend;
    private void Start()
    {
        generate = GetComponentInParent<GenerateObstacle>();
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          generate.Message(myNum);
          rend.material.color = Color.green;
        }
    }
}
