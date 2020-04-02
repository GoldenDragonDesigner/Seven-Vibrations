using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : Health
{
    [SerializeField]
    float maxShield = 5;
    float currentShield;

    public DelOneParam onShieldHit = new DelOneParam();
    public DelOneParam onShieldDestoryed = new DelOneParam();

    public override void TakeDamage(float _amount)
    {
        if(currentShield == 0)
        {
            base.TakeDamage(_amount);
            return;
        }

        currentShield = Mathf.Min(currentShield - _amount, maxShield);

        if(0 >= currentShield)
        {
            onShieldDestoryed.CallEvent(0);
            base.TakeDamage(currentShield * -1.0f);
            currentShield = 0;
        }
        else
        {
            onShieldHit.CallEvent(currentShield / maxShield);
        }
    }
}
