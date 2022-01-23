using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts
{
    public class FinishLineScript : MonoBehaviour
    {
        public GameObject finishMenuUI;

        public void Replay()
        {
            finishMenuUI.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.StopPlayer();
                StartCoroutine(Wait());
                finishMenuUI.SetActive(true);
            }
        }
        
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
        }
    }
}
