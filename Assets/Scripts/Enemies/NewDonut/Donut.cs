using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Donut : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private List<Attack> attacks;
    
    private Attack _currentAttack;
    private int _attackIndex = 0;
    private bool _canChangeAttack = true;

    private void Start()
    {
        _currentAttack = attacks[0];
    }

    private void Update()
    {
        _currentAttack.AttackWeapon.OnMainShootStarted();
        if (_canChangeAttack)
            StartCoroutine(ChangeWeapon());
    }

    private IEnumerator ChangeWeapon()
    {
        _canChangeAttack = false;
        yield return new WaitForSeconds(_currentAttack.AttackTime);

        _currentAttack.AttackWeapon.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeBetweenAttacks);

        _attackIndex = (_attackIndex + 1) % attacks.Count;
        _currentAttack = attacks[_attackIndex];
        _canChangeAttack = true;
        _currentAttack.AttackWeapon.gameObject.SetActive(true);
    }
}

[Serializable]
public class Attack
{
    [SerializeField] private float attackTime;
    [SerializeField] private Weapon attackWeapon;
    
    public float AttackTime => attackTime;
    public Weapon AttackWeapon => attackWeapon;
}
