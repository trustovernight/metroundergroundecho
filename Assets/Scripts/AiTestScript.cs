using BreadcrumbAi;
using UnityEngine;

public class AiTestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("MoveState: " + GetComponent<Ai>().moveState);
    }
}
