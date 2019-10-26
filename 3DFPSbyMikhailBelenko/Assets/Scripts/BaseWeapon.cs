using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : BaseObject
{
    [SerializeField] protected Transform _gunT;
    [SerializeField] protected float _force = 300;

    [SerializeField] protected ParticleSystem _muzzleFlash;
    [SerializeField] protected GameObject _hitParticle;

    protected Timer _rechargeTimer = new Timer();
    protected bool _isFire = true;

    public abstract void Fire();

    protected override void Awake()
    {
        base.Awake();
        _gunT = _GOTransform.GetChild(2);
        _muzzleFlash = GetComponentInChildren<ParticleSystem>();
        _hitParticle = Resources.Load<GameObject>("Flare");
    }

    protected virtual void Update()
    {
        _rechargeTimer.Update();

        if (_rechargeTimer.IsEvent)
        {
            _isFire = true;
        }
    }
}
