using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts
{
    public class UIGameMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        public void QuitGame()
        {
            Debug.Log("Game was quit.");
            Application.Quit();
        }
    }
}
