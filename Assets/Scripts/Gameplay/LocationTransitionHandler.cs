using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using MetroUndergroundEcho.Core;

public class LocationTransitionHandler : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private float valveSpinDuration = 2f;              // Duration for each spin rotation
    [SerializeField] private int valveSpinCount = 4;                    // Number of times valve spins
    [SerializeField] private float doorOpenDuration = 1.5f;             // Time for door to open
    [SerializeField] private float transitionDelay = 1f;                // Delay before loading new scene

    private GameObject _valve;
    private GameObject _door;
    private DoorController _doorController;
    private bool _isTransitioning = false;
    private bool _playerInRange = false;
    
    void Start()
    {
        _valve = GameObject.Find("Gate Valve");
        _door = GameObject.Find("Door");
        
        if (_door != null)
            _doorController = _door.GetComponent<DoorController>();

        if (_valve == null)
            Debug.LogError("[LocationTransitionHandler] Gate Valve not found in scene!");
        if (_door == null)
            Debug.LogError("[LocationTransitionHandler] Door not found in scene!");

        InputManager.OnPressedE += OnValveInteract;
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInRange = false;
    }

    /// <summary>
    /// Called when player interacts with the valve
    /// </summary>
    public void OnValveInteract()
    {
        if (!_playerInRange) return;

        if (_isTransitioning)
        {
            Debug.Log("[LocationTransitionHandler] Transition already in progress, ignoring input");
            return;
        }

        StartCoroutine(TransitionSequence());
    }

    /// <summary>
    /// Main coroutine that handles the complete transition sequence
    /// </summary>
    private IEnumerator TransitionSequence()
    {
        _isTransitioning = true;
        Debug.Log("[LocationTransitionHandler] Starting location transition...");

        // Step 1: Spin valve
        yield return StartCoroutine(SpinValve());
        Debug.Log("[LocationTransitionHandler] Valve spin complete");

        // Step 2: Open door
        yield return StartCoroutine(OpenDoor());
        Debug.Log("[LocationTransitionHandler] Door opened");

        // Step 3: Wait before loading new scene
        yield return new WaitForSeconds(transitionDelay);

        // Step 4: Load new scene and unload old scene
        yield return StartCoroutine(TransitionScene());
        
        _isTransitioning = false;
    }

    /// <summary>
    /// Coroutine that spins the valve 4-5 times with smooth rotation
    /// </summary>
    private IEnumerator SpinValve()
    {
        if (_valve == null)
            yield break;

        float totalSpinTime = valveSpinDuration * valveSpinCount;
        float elapsedTime = 0f;
        Vector3 initialRotation = _valve.transform.localEulerAngles;

        while (elapsedTime < totalSpinTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / totalSpinTime;
            
            // Calculate total rotation (4-5 full rotations = 1440-1800 degrees)
            float totalRotation = 360f * valveSpinCount;
            float currentRotation = totalRotation * progress;

            // Apply rotation around Z-axis (assuming valve spins around Z)
            _valve.transform.localEulerAngles = initialRotation + new Vector3(0, 0, currentRotation);

            yield return null;
        }

        // Ensure final rotation is exact
        _valve.transform.localEulerAngles = initialRotation + new Vector3(0, 360f * valveSpinCount, 0);
        Debug.Log($"[LocationTransitionHandler] Valve completed {valveSpinCount} spins");
    }

    /// <summary>
    /// Coroutine that opens the door
    /// </summary>
    private IEnumerator OpenDoor()
    {
        if (_doorController == null)
            yield break;

        _doorController.Open();
        
        // Wait for door to finish opening
        yield return new WaitForSeconds(doorOpenDuration);
        Debug.Log("[LocationTransitionHandler] Door is now open");
    }

    /// <summary>
    /// Loads the next scene and unloads the current scene
    /// </summary>
    private IEnumerator TransitionScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("[LocationTransitionHandler] Next scene name not set!");
            yield break;
        }

        Debug.Log($"[LocationTransitionHandler] Loading scene: {nextSceneName}");

        // Load new scene additively first
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        yield return loadOperation;

        Debug.Log($"[LocationTransitionHandler] Scene {nextSceneName} loaded");

        // Small delay to ensure new scene is ready
        yield return new WaitForSeconds(0.5f);

        // Unload current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"[LocationTransitionHandler] Unloading current scene: {currentSceneName}");
        
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentSceneName);
        yield return unloadOperation;

        Debug.Log($"[LocationTransitionHandler] Scene transition complete! New scene: {nextSceneName}");
    }

    /// <summary>
    /// Optional: Returns whether a transition is currently happening
    /// </summary>
    public bool IsTransitioning => _isTransitioning;
}
