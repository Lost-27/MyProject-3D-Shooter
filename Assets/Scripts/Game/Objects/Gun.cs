using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking(nameof(FireBullet)))
            {
                InvokeRepeating(nameof(FireBullet), 0f, 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke(nameof(FireBullet));
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = launchPosition.position;
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
    }
}