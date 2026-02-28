using UnityEngine;

namespace MetroUndergroundEcho.Core.Sound
{
    public class SoundProducedEvent
    {
        public Vector3 Position { get; }
        public float Volume { get; }

        public SoundProducedEvent(Vector3 position, float volume)
        {
            Position = position;
            Volume = volume;
        }
    }
}