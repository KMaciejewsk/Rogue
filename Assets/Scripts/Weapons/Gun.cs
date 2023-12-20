using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour, IWeapon
{
    private Transform target;

    public float fireRate = 0.2f;

    private bool canFire = true;

    public float GetFireRate()
    {
        return fireRate;
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            PointAtMouse();
            MoveToPlayer();
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            Shoot();
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void PointAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePos - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MoveToPlayer()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 1);

        transform.position = smoothedPosition;
    }

    private void Shoot()
    {
        GameObject BulletGameObject = Instantiate(Resources.Load<GameObject>("Bullet"), transform.position, transform.rotation);
        Bullet bulletScript = BulletGameObject.GetComponent<Bullet>();
        
        bulletScript.SetDirection(transform.right);
    }

    private IEnumerator ReloadCoroutine()
    {
        canFire = false;

        yield return new WaitForSeconds(fireRate);

        canFire = true;
    }
}
