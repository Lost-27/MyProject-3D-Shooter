using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    public bool isUpgraded;
    public float upgradeTime = 10.0f;

    private float _currentTime;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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

        _currentTime += Time.deltaTime;
        if (_currentTime > upgradeTime && isUpgraded)
        {
            isUpgraded = false;
        }
    }

    public void UpgradeGun()
    {
        isUpgraded = true;
        _currentTime = 0;
    }

    private void FireBullet()
    {
        Rigidbody bullet = СreateBullet();
        bullet.velocity = transform.parent.forward * 100;

        if (isUpgraded)
        {
            Rigidbody bullet2 = СreateBullet();
            bullet2.velocity = (transform.right + transform.forward / 0.5f) * 50;
            Rigidbody bullet3 = СreateBullet();
            bullet3.velocity = ((transform.right * -1) + transform.forward / 0.5f) * 50;
        }

        if (isUpgraded)
        {
            _audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            _audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

    private Rigidbody СreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }
}