using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action OnSpacePressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }
    }
}
