using UnityEngine;
using UnityEngine.Audio;

namespace Scenes.Scripts
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer AudioMixer;
    
        public void SetVolume(float volume)
        {
            AudioMixer.SetFloat("masterVolume", volume);
        }

    }
}
