using UnityEngine;
using MetroUndergroundEcho.Core;

namespace MetroUndergroundEcho.Gameplay 
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject pauseMenu;
        public bool isPaused {get; set;} = false;

        private void OnEnable() 
        {
            InputManager.OnPressedEscape += ReactToEscape;
        }

        private void OnDisable() 
        {
            InputManager.OnPressedEscape -= ReactToEscape;
        }

        private void ReactToEscape()
        {
            if (isPaused)
            {
                Resume();
            } else {
                Pause();
            } 
        }

        public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; 

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isPaused = true;
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; 

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isPaused = false;
        }
    }
}