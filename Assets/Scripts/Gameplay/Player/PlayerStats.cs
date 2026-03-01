using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MetroUndergroundEcho.Core;

namespace MetroUndergroundEcho.Gameplay 
{
    public class PlayerStats : MonoBehaviour
    {
        public Slider healthSlider;
        public Slider hungerSlider;
        public Slider staminaSlider;

        public float maxHealth = 100f;
        public float maxHunger = 100f;
        public float maxStamina = 100f;

        [HideInInspector] public float health = 100f;
        [HideInInspector] public float hunger = 100f;
        [HideInInspector] public float stamina = 100f;

        public Color emptyColor = Color.white;
        
        public float Delay = 0.5f;
        private float StaminaTimeRuning = 0f;
        private float HungerTime = 0f;

        private Player player;
        private PauseManager pauseManager;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            pauseManager = GameObject.Find("PauseObject").GetComponent<PauseManager>();

            healthSlider.maxValue = maxHealth;
            hungerSlider.maxValue = maxHunger;
            staminaSlider.maxValue = maxStamina;

            healthSlider.value = maxHealth;
            hungerSlider.value = maxHunger;
            staminaSlider.value = maxStamina;

            StartCoroutine(WaitUpdate());
        }

        private void Update() {
            if (player.IsRunning)
            {
                StaminaTimeRuning += Time.deltaTime;

                if (StaminaTimeRuning >= Delay)
                {
                    ModifyStamina(-5f);
                    StaminaTimeRuning = 0f;
                }
            }

            if (player.isInAir)
            {
                ModifyStamina(-20f);
                player.isInAir = false;
            }

            if (hunger <= 0f) 
            {
                stamina = 0f;
                HungerTime += Time.deltaTime;

                if (HungerTime >= Delay)
                {
                    ModifyHealth(-5f);
                    HungerTime = 0f;
                }
            }
        }

        private void FixedUpdate() {
            if (hunger > 20f)
            {
                if (stamina < 100f)
                {
                    ModifyStamina(0.1f);
                    ModifyHunger(-0.05f);
                    Debug.Log($"{stamina} stamina");
                }
            }
        }

        IEnumerator WaitUpdate()
        {
            while(true)
            {
                if (!pauseManager.isPaused)
                {
                    yield return new WaitForSeconds(1f);
                    ModifyHunger(-0.5f);
                    Debug.Log($"{hunger} hunger");
                }
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

        public void ModifyHealth(float amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxStamina); 
            healthSlider.value = health;
        }
    }
}