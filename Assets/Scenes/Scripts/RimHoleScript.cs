using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RimHoleScript : MonoBehaviour
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
            Debug.Log("HOLE DETECED");
           // Vector3 popupPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
           Vector3 popupPos = other.transform.position;
           popupPos.y += 2;
         //  GameManager.DoFloatingText(popupPos,"GOOD JOB", Color.green);
         GameManager.StopPlayer();
         GameManager.ShowFloatingTextPopup("YOU GOT RIMMED!", Color.red);
         StartCoroutine(CallbackReset());
        }
    }

    private IEnumerator CallbackReset()
    {
        yield return new WaitForSeconds(FloatingText.destroyTime);
        GameManager.ResetGame();
    }

    

   
    
}
