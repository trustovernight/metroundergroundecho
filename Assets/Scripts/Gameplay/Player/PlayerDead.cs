using UnityEngine;
using MetroUndergroundEcho.Gameplay;

public class PlayerDead : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject youAreDeadCanvas;

    private PlayerStats playerStats;
    private PauseManager pauseManager;

    private void Start() 
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        pauseManager = GameObject.Find("PauseObject").GetComponent<PauseManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerStats.health <= 0){
                Dead_();
            } else{
                playerStats.ModifyHealth(-50f);
                playerStats.ModifyStamina(100f);
            }
        }
    }

    private void Dead_()
    {
        mainCanvas.SetActive(false);
        youAreDeadCanvas.SetActive(true);
    }
}

