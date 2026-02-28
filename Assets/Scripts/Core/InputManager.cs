using UnityEngine;
using System;

namespace MetroUndergroundEcho.Core
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnSpacePressedSpace;
        public static event Action OnSpacePressedLeftShift;
        public static event Action OnSpacePressedLeftControl;
        public static event Action OnLeftShiftReleased;
        public static event Action OnLeftControlReleased;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Praced space");
                OnSpacePressedSpace?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Praced LeftShift");
                OnSpacePressedLeftShift?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("Praced LeftControl");
                OnSpacePressedLeftControl?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                OnLeftShiftReleased?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                OnLeftControlReleased?.Invoke();
            }
        }
    }
}