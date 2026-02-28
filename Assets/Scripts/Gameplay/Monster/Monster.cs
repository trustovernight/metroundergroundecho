using BreadcrumbAi;
using MetroUndergroundEcho.Core.Sound;
using UnityEngine;
using System.Collections;
using MetroUndergroundEcho.Core;

namespace MetroUndergroundEcho.Gameplay
{
    [RequireComponent(typeof(Ai))]
    [RequireComponent(typeof(Rigidbody))]
    public class Monster : MonoBehaviour
    {
        private Ai ai;
        private Coroutine soundResponseCoroutine;

        // Configuration for sound response behavior
        private float soundHandlingDelay = 1f;              // delay before moving to sound
        private float soundResponseSpeed = 5f;
        private float soundResponseStoppingDistance = 0.5f;
        private float soundResponseTimeout = 10f;
        private float lookAroundDuration = 3f;              // time spent looking around at the sound location
        
        private void Start()
        {
            ai = GetComponent<Ai>();
        }

        private void OnEnable()
        {
            SoundManager.Subscribe(OnSoundProduced);
        }

        private void OnDisable()
        {
            SoundManager.Unsubscribe(OnSoundProduced);
        }

        private void OnSoundProduced(SoundProducedEvent e)
        {
            if (!IsWithinSoundRange(transform.position, e.Position, e.Volume)) return;
            
            // Interrupt current movement and respond to sound
            RespondToSound(e.Position);
        }

        /// <summary>
        /// Interrupts current AI behavior and moves towards the sound source position
        /// </summary>
        private void RespondToSound(Vector3 soundPosition)
        {
            Debug.Log($"[Monster] Responding to sound at {soundPosition}, ignoreMovement set to true");
            
            // disable ai movement so we can manually control the rigidbody
            if (ai != null)
            {
                ai.ignoreMovement = true;
            }

            // immediately stop any current motion
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = Vector3.zero;
            }

            // Stop any previous sound response coroutine
            if (soundResponseCoroutine != null)
            {
                StopCoroutine(soundResponseCoroutine);
            }

            // Start new coroutine to move towards sound
            soundResponseCoroutine = StartCoroutine(MoveToSoundCoroutine(soundPosition));
        }

        /// <summary>
        /// Coroutine that interrupts AI movement and redirects towards sound position
        /// </summary>
        private IEnumerator MoveToSoundCoroutine(Vector3 soundPosition)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            float elapsedTime = 0f;

            // initial handling delay
            float delayTimer = 0f;
            while (delayTimer < soundHandlingDelay)
            {
                // if player seen, abort early
                if (ShouldAbortForPlayer())
                    break;

                delayTimer += Time.deltaTime;
                yield return null;
            }

            // Continue moving towards sound until reaching destination or timeout
            while (elapsedTime < soundResponseTimeout)
            {
                // if player seen, abort and let AI resume
                if (ShouldAbortForPlayer())
                    break;

                float distanceToSound = Vector3.Distance(transform.position, soundPosition);

                // Stop if reached the sound position
                if (distanceToSound <= soundResponseStoppingDistance)
                {
                    break;
                }

                // Calculate direction to sound position
                Vector3 directionToSound = (soundPosition - transform.position).normalized;

                // Move towards sound position directly, bypassing normal AI logic
                rb.MovePosition(rb.position + directionToSound * soundResponseSpeed * Time.deltaTime);

                // Rotate to face sound position
                Vector3 lookDirection;
                if (ai._IsGround)
                {
                    // Keep Y rotation only for ground units
                    lookDirection = new Vector3(soundPosition.x, transform.position.y, soundPosition.z) - transform.position;
                }
                else
                {
                    // Allow full 3D rotation for air units
                    lookDirection = soundPosition - transform.position;
                }

                if (lookDirection != Vector3.zero)
                {
                    rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), ai.rotationSpeed));
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // if we reached position (or timeout) without seeing player, pause and look around
            if (!ShouldAbortForPlayer())
            {
                float lookTimer = 0f;
                rb.linearVelocity = Vector3.zero;

                while (lookTimer < lookAroundDuration)
                {
                    // if player appears while looking, exit early
                    if (ShouldAbortForPlayer())
                        break;

                    // simple yaw rotation over time
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, 120f * Time.deltaTime, 0f));
                    lookTimer += Time.deltaTime;
                    yield return null;
                }
            }

            // restore AI movement and clear coroutine
            if (ai != null)
            {
                ai.ignoreMovement = false;
                Debug.Log("[Monster] Sound sequence complete, ignoreMovement set back to false");
            }

            soundResponseCoroutine = null;
        }

        private bool IsWithinSoundRange(Vector3 monsterPosition, Vector3 soundPosition, float soundRange)
        {
            float distance = Vector3.Distance(monsterPosition, soundPosition);
            return distance <= soundRange;
        }

        /// <summary>
        /// Returns true if AI can or does see the player and should abandon sound handling.
        /// </summary>
        private bool ShouldAbortForPlayer()
        {
            if (ai == null)
                return false;

            if (ai._CanFollowPlayer && ai.Player != null)
            {
                // use the Ai's range check so we mirror its vision settings
                if (ai.InRange(ai.Player.position, ai.visionDistance))
                    return true;
            }

            return false;
        }
    }
}