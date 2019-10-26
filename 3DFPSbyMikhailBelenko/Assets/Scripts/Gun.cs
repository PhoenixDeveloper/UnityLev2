using UnityEngine;

public class Gun : BaseWeapon
{
    private int _bulletCount = 30;
    private float _shootDistance = 100f;
    private float _damage = 20;
    private float _currentDamage;
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
            obj.SetDamage(_currentDamage);
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
                GameObject tempHit = Instantiate(_hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                tempHit.transform.parent = hit.transform;
                if (hit.collider.tag == "Player")
                {
                    return;
                }
                else
                {
                    _currentDamage = _damage * (1f - 0.1f*(Vector3.Distance(MainCamera.transform.position, tempHit.transform.position)/10f));
                    SetDamage(hit.collider.GetComponent<ISetDamage>());
                }
                Destroy(tempHit, 0.5f);
            }
        }
    }
}
