using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float _destructTime = 3.0f;
    
    public void Initiate()
    {
        Invoke(nameof(DestructHead), _destructTime);
    }
    private void DestructHead()
    {
        Destroy(gameObject);
    }
}
