using UnityEngine;

namespace MetroUndergroundEcho.Core
{
    public interface ISoundProducer
    {
        // Returns the world coordinate where the sound was produced
        Vector3 Produce();
    }
}
