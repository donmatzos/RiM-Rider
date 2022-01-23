using System.Collections;
using UnityEngine;

namespace Scenes.Scripts
{
    public class ObstacleScript : MonoBehaviour
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
                GameManager.AudioController.PlayAudio(AudioFiles.CRASH);
                GameManager.StopPlayer();
                GameManager.ShowFloatingTextPopup("YOU CRASHED!", Color.red);
                StartCoroutine(CallbackReset());
            }
        }

        private IEnumerator CallbackReset()
        {
            yield return new WaitForSeconds(FloatingText.destroyTime);
            GameManager.ResetGame();
        }

    }
}
