using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public static GameObject player;
    public static PlayerMovement playerMovement;
    public static GenerateObstacle Obstacle;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StopPlayer()
    {
        playerMovement.setSpeedMultiplikator(0);
        playerMovement.gameIsStopped = true;
    }
    public static void StartPlayer()
    {
        playerMovement.setSpeedMultiplikator(1);
    }

    public static void ResetGame()
    {
        if (playerMovement != null)
        {
            playerMovement.Reset();
            Obstacle.Reset();
        }
       
    }

 

    public static void ShowFloatingTextPopup(string text, Color c)
    {
        Debug.Log("Player="+player.transform.position);

        Transform cam = Camera.main.transform;
        Vector3 pos=cam.position;
        pos.y += 2;
        pos.z += 10;
        EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
        FloatingText floatingText = Instantiate( effectsManager.text, pos, Quaternion.identity ,  cam);
       // FloatingText floatingText = Instantiate( effectsManager.text, position, Quaternion.identity ,  player.GetComponent<Camera>().transform);
       // floatingText.GetComponent<TextMesh>().text = text;
        floatingText.setText(text);
        floatingText.setColor(c);
        floatingText.ShowText();
    }
    
    public static FloatingText ShowFloatingText(string text, Color c)
    {
        Debug.Log("Player="+player.transform.position);

        Transform cam = Camera.main.transform;
        Vector3 pos=cam.position;
        pos.y += 2;
        pos.z += 10;
        EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
        FloatingText floatingText = Instantiate( effectsManager.text, pos, Quaternion.identity ,  cam);
        // FloatingText floatingText = Instantiate( effectsManager.text, position, Quaternion.identity ,  player.GetComponent<Camera>().transform);
        // floatingText.GetComponent<TextMesh>().text = text;
        floatingText.setText(text);
        floatingText.setColor(c);
        return floatingText;
    }
    
}
