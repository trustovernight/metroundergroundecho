using UnityEngine;
using System;

namespace MetroUndergroundEcho.Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        // Event: position, producer
        public static event Action<Vector3, ISoundProducer> SoundProduced;

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
        public static void OnSoundProduced(ISoundProducer producer)
        {
            if (producer == null) return;
            Vector3 pos = producer.Produce();
            Instance?.InvokeSoundProduced(pos, producer);
        }

        // Alternative: call directly with position
        public static void OnSoundProduced(Vector3 position, ISoundProducer producer = null)
        {
            Instance?.InvokeSoundProduced(position, producer);
        }

        private void InvokeSoundProduced(Vector3 position, ISoundProducer producer)
        {
            SoundProduced?.Invoke(position, producer);
            Debug.Log($"[SoundManager] Sound produced at {position} by {producer?.GetType().Name}");
        }

        public static void Subscribe(Action<Vector3, ISoundProducer> handler)
        {
            SoundProduced += handler;
        }

        public static void Unsubscribe(Action<Vector3, ISoundProducer> handler)
        {
            SoundProduced -= handler;
        }
    }
}
