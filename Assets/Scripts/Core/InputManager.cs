using UnityEngine;
using System;

namespace MetroUndergroundEcho.Core
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnPressedSpace;
        public static event Action OnPressedLeftShift;
        public static event Action OnPressedLeftControl;
        public static event Action OnPressedZ;
        public static event Action OnLeftShiftReleased;
        public static event Action OnLeftControlReleased;
        public static event Action OnZReleased;
        public static event Action OnSpaceReleased;
        public static event Action OnPressedEscape;
        public static event Action OnPressedE;
        public static event Action OnEReleased;
        // public static event Action OnEscapeReleased;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Praced space");
                OnPressedSpace?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Praced LeftShift");
                OnPressedLeftShift?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("Praced LeftControl");
                OnPressedLeftControl?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                OnLeftShiftReleased?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                OnLeftControlReleased?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Z)) 
            {
                Debug.Log("Praced Z");
                OnPressedZ?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Z)) 
            {
                OnZReleased?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                OnSpaceReleased?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPressedEscape?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnPressedE?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                OnEReleased?.Invoke();
            }

        }
    }
}