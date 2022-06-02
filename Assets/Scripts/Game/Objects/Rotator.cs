using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}