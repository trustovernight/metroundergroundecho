using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MetroUndergroundEcho.Gameplay;

public class SliderBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider staminaSlider;

    public float maxHealth = 100f;
    public float maxHunger = 100f;
    public float maxStamina = 100f;

    [HideInInspector] private float health = 100f;
    [HideInInspector] private float hunger = 100f;
    [HideInInspector] private float stamina = 100f;

    public Color emptyColor = Color.white;
    
    public float StaminaDelay = 0.5f;
    private float StaminaTime = 0f;

    public Player player;

    void Start()
    {
        
        healthSlider.maxValue = maxHealth;
        hungerSlider.maxValue = maxHunger;
        staminaSlider.maxValue = maxStamina;

        healthSlider.value = maxHealth;
        hungerSlider.value = maxHunger;
        staminaSlider.value = maxStamina;

        StartCoroutine(WaitUpdate());
    }

    private void FixedUpdate() {
        if (player.IsRunning == true) 
        {
            Debug.Log($"{stamina} stamina");
            ModifyHunger(-5f);

            while (player.IsRunning)
            {
                StaminaTime += Time.deltaTime;
                if (StaminaTime >= StaminaDelay) 
                {
                    ModifyStamina(-5f);
                    StaminaTime = 0;
                    Debug.Log($"{stamina} stamina");
                }
            }
        }
    }

    IEnumerator WaitUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ModifyHunger(-0.5f);
            Debug.Log($"{hunger} hunger");
        }
    }

    public void ModifyHunger(float amount) 
    {
        hunger = Mathf.Clamp(hunger + amount, 0, maxHunger); 
        hungerSlider.value = hunger;
    }

    public void ModifyStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0, maxStamina); 
        staminaSlider.value = stamina;
    }
}
    