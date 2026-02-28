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

    public Color emptyColor = Color.white;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        hungerSlider.maxValue = maxHunger;
        staminaSlider.maxValue = maxStamina;

        healthSlider.value = maxHealth;
        hungerSlider.value = maxHunger;
        staminaSlider.value = maxStamina;

    }

    void FixedUpdate() 
    {
        StartCoroutine(WaitHungerUpdate());
    }

    IEnumerator WaitHungerUpdate()
    {
        yield return new WaitForSeconds(5f);
        ModifyHunger(-5f);
    }

    public void ModifyHunger(float amount) { hungerSlider.value = amount; }
}