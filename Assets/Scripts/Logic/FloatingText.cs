using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update
    public static float destroyTime = 1F;
    void Start()
    {
        //  gameObject.SetActive(false);
     
    }

    public void ShowText()
    {
        //gameObject.SetActive(true);
        Destroy(gameObject,destroyTime);
    }

    public void HideText()
    {
        Destroy((gameObject));
    }

    public void setText(string text)
    {
        GetComponent<TextMesh>().text = text;
    }

    public void setColor(Color color)
    {
        GetComponent<TextMesh>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
