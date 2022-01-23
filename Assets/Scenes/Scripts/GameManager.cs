using UnityEngine;

namespace Scenes.Scripts
{
    public class GameManager : MonoBehaviour
    {
    
        // Start is called before the first frame update
        public static GameObject player;
        public static PlayerMovement playerMovement;
        public static GenerateObstacle Obstacle;
        private static FloatingText curFloatingText = null;
        public static AudioController AudioController;

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

        private static void DestroyFloatingText()
        {
            if (curFloatingText != null)
            {
                curFloatingText.HideText();
                
                curFloatingText = null;
            }
        }
    
        public static void ShowFloatingTextPopup(string text, Color c)
        {
            DestroyFloatingText();
            
            Transform cam = Camera.main.transform;
            Vector3 pos=cam.position;
            pos.y += 2;
            pos.z += 10;
            EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
            FloatingText floatingText = Instantiate( effectsManager.text, pos, Quaternion.identity ,  cam);
            // FloatingText floatingText = Instantiate( effectsManager.text, position, Quaternion.identity ,  player.GetComponent<Camera>().transform);
            // floatingText.GetComponent<TextMesh>().text = text;
            floatingText.SetText(text);
            floatingText.SetColor(c);
            floatingText.ShowText();
            curFloatingText = floatingText;
        }
    
        public static FloatingText ShowFloatingText(string text, Color c)
        {
           DestroyFloatingText();

            Transform cam = Camera.main.transform;
            Vector3 pos=cam.position;
            pos.y += 2;
            pos.z += 10;
            EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
            FloatingText floatingText = Instantiate( effectsManager.text, pos, Quaternion.identity ,  cam);
            // FloatingText floatingText = Instantiate( effectsManager.text, position, Quaternion.identity ,  player.GetComponent<Camera>().transform);
            // floatingText.GetComponent<TextMesh>().text = text;
            floatingText.SetText(text);
            floatingText.SetColor(c);
            curFloatingText = floatingText;
            return floatingText;
        }
    
    }
}
