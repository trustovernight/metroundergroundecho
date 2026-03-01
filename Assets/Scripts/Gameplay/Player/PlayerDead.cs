using UnityEngine;
using MetroUndergroundEcho.Gameplay;
using MetroUndergroundEcho.Core;

public class PlayerDead : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject youAreDeadCanvas;

    private PlayerStats playerStats;
    private PauseManager pauseManager;

    public bool isDead = false;

    private void Start() 
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        pauseManager = GameObject.Find("PauseObject").GetComponent<PauseManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerStats.health >= 0){
                playerStats.ModifyHealth(-51f);
                playerStats.ModifyStamina(100f);
                if (playerStats.health <= 0) 
                {
                    Dead();
                }
            } 
                
            
        }
    }

    private void Dead()
    {
        isDead = true;
        mainCanvas.SetActive(false);
        youAreDeadCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
         pauseManager.isPaused = false;
    }
}

