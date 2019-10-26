using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : BaseObject, ISetDamage
{
    [SerializeField] private float _health;

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            if(value > 0)
            {
                _health = value;
            }
            else
            {
                _health = 0;
                // TODO: Необходимо вызвать функцию смерти
                if(tag != "Player")
                {
                    Destroy(_GOInstance);
                }
            }
        }
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
    }
}
