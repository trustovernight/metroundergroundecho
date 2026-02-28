using MetroUndergroundEcho.Core.Sound;

namespace MetroUndergroundEcho.Core
{
    public interface ISoundProducer
    {
        // Returns the world coordinate where the sound was produced
        SoundProducedEvent Produce();
    }
}
