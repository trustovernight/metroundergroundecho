using UnityEngine;
using System;
using MetroUndergroundEcho.Core.Sound;

namespace MetroUndergroundEcho.Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; } = new();

        public static event Action<SoundProducedEvent> SoundProduced;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Convenience: called by ISoundProducer implementations
        public static void OnSoundProduced(SoundProducedEvent soundEvent, ISoundProducer producer = null)
        {
            if (soundEvent == null) return;
            Instance.InvokeSoundProduced(soundEvent);
        }
        
        // Alternative: call directly with position
        public static void OnSoundProduced(SoundProducedEvent soundEvent)
        {
            Instance.InvokeSoundProduced(soundEvent);
        }

        private void InvokeSoundProduced(SoundProducedEvent soundEvent)
        {
            SoundProduced?.Invoke(soundEvent);
            Debug.Log($"[SoundManager] Sound produced at {soundEvent.Position} with volume {soundEvent.Volume}");
        }

        public static void Subscribe(Action<SoundProducedEvent> handler)
        {
            SoundProduced += handler;
        }

        public static void Unsubscribe(Action<SoundProducedEvent> handler)
        {
            SoundProduced -= handler;
        }
    }
}
