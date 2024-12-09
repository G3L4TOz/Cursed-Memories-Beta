using UnityEngine;

public class BoxReset : MonoBehaviour
{
    public Vector3 InitialPosition { get; private set; }

    void Start()
    {
        InitialPosition = transform.position;
    }
}