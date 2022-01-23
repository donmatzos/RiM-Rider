using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
    
        public GameObject pauseMenuUI;
        public GameObject pauseButtonUI;

        public void Pause()
        {
            pauseButtonUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void Resume()
        {
            pauseButtonUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
