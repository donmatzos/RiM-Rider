using UnityEngine;

namespace Scenes.Scripts
{
    public class FloatingText : MonoBehaviour
    {
        public static float destroyTime = 1F;

        public void ShowText()
        {
            //gameObject.SetActive(true);
            Destroy(gameObject, destroyTime);
        }

        public void HideText()
        {
            Destroy((gameObject));
        }

        public void SetText(string text)
        {
            GetComponent<TextMesh>().text = text;
        }

        public void SetColor(Color color)
        {
            GetComponent<TextMesh>().color = color;
        }
    }
}
