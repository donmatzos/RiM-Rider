using System.Collections;
using System.Collections.Generic;
using Scenes.Scripts;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //  GameManager.DoFloatingText(popupPos,"GOOD JOB", Color.green);
            GameManager.ShowFloatingTextPopup("LEVEL COMPLETED!", Color.green);
            GameManager.StopPlayer();
        }
    }
}
