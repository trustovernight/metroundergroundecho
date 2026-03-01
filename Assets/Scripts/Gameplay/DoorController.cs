using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;

    private bool _isMoving = false;

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        float targetY = opening ? openRot : closeRot;

        // Check if door has reached target rotation
        float currentY = currentRot.y;
        float distance = Mathf.Abs(Mathf.DeltaAngle(currentY, targetY));

        if (distance < 0.5f) // Close enough to target
        {
            door.transform.localEulerAngles = new Vector3(currentRot.x, targetY, currentRot.z);
            _isMoving = false;
        }
        else
        {
            door.transform.localEulerAngles = new Vector3(
                currentRot.x,
                Mathf.MoveTowards(currentY, targetY, speed * Time.deltaTime),
                currentRot.z
            );
            _isMoving = true;
        }
    }

    public void Open()
    {
        opening = true;
    }

    public void Close()
    {
        opening = false;
    }

    /// <summary>
    /// Returns true if door is currently moving
    /// </summary>
    public bool IsMoving => _isMoving;

    /// <summary>
    /// Returns true if door is fully opened
    /// </summary>
    public bool IsOpen => !opening && !_isMoving;

    /// <summary>
    /// Returns true if door is fully closed
    /// </summary>
    public bool IsClosed => !opening && !_isMoving;
}