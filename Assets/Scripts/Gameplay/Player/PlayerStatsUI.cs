using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider staminaSlider;

    public float maxHealth = 100f;
    public float maxHunger = 100f;
    public float maxStamina = 100f;

    private float health = 100f;
    private float hunger = 100f;
    private float stamina = 100f;

    public Color emptyColor = Color.white;

    void Start()
    {
        StartCoroutine(WaitHungerUpdate());

        healthSlider.maxValue = maxHealth;
        hungerSlider.maxValue = maxHunger;
        staminaSlider.maxValue = maxStamina;

        healthSlider.value = maxHealth;
        hungerSlider.value = maxHunger;
        staminaSlider.value = maxStamina;

    }

    IEnumerator WaitHungerUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ModifyHunger(-0.5f);
            Debug.Log(hunger);
        }
    }

    public void ModifyHunger(float amount) 
    {
        hunger = Mathf.Clamp(hunger + amount, 0, maxHunger); 
        hungerSlider.value = hunger;
    }
}