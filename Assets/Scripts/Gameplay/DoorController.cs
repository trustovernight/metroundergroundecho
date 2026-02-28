using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        float targetY = opening ? openRot : closeRot;

        door.transform.localEulerAngles = new Vector3(
            currentRot.x,
            Mathf.MoveTowards(currentRot.y, targetY, speed * Time.deltaTime),
            currentRot.z
        );
    }

    public void Open()
    {
        opening = true;
    }
}