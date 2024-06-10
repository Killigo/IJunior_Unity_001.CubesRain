using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Platform : MonoBehaviour
{
    public Vector3 GetSize()
    {
        return GetComponent<Renderer>().bounds.size;
    }
}
