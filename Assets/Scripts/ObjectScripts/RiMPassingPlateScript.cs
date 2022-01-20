using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiMPassingPlateScript : MonoBehaviour
{
    private RiMController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<RiMController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //  GameManager.DoFloatingText(popupPos,"GOOD JOB", Color.green);
            GameManager.ShowFloatingTextPopup("GOOD RIMJOB!", Color.green);
        }
    }
}
