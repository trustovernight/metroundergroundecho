using UnityEngine;
using UnityEngine.UI;

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

        ModifyHunger(0f);
    }

    void FixedUpdate() 
    {
        // if(hungerSlider.value == 0)
        // {
        //     hungerSlider.fillRect.GetComponent<Image>().color = emptyColor;
        // }
    }

    public void ModifyHunger(float amount) { hungerSlider.value = amount; }
}