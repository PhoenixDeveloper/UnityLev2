using UnityEngine;

public class Gun : BaseWeapon
{
    private int _bulletCount = 30;
    private float _shootDistance = 1000f;
    private float _damage = 20;
    private KeyCode reloadKey = KeyCode.R;

    void Start()
    {
        
    }

    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            _bulletCount = 30;
        }
    }

    private void SetDamage(ISetDamage obj)
    {
        if (obj != null)
        {
            obj.SetDamage(_damage);
        }
    }

    public override void Fire()
    {
        if((_bulletCount > 0) && _isFire)
        {
            _muzzleFlash.Play();
            _bulletCount--;
            RaycastHit hit;
            Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
            if(Physics.Raycast(ray, out hit, _shootDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    return;
                }
                else
                {
                    SetDamage(hit.collider.GetComponent<ISetDamage>());
                }

                GameObject tempHit = Instantiate(_hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                tempHit.transform.parent = hit.transform;
                Destroy(tempHit, 0.5f);
            }
        }
    }
}
