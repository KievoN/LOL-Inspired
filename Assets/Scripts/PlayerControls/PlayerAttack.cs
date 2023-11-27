using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStats PlayerStats = new();

    private GameObject _attackTarget;
    private Coroutine _attackCoroutine;

    private const float _projectileSpeed = 0.5f;

    private float _attackCooldown = 0f;

    private void Update()
    {
        if(_attackCooldown > 0f)
        {
            _attackCooldown = Mathf.Max(_attackCooldown - Time.deltaTime, 0f);
        }
    }

    public int GetAttackRange()
    {
        return PlayerStats.AttackRange / 100;
    }

    public void StartAttack(GameObject other)
    {
        _attackTarget = other;
        _attackCoroutine = StartCoroutine(Attack());
    }

    public void CancelAttack()
    {
        if (_attackCoroutine == null) return;

        StopCoroutine(_attackCoroutine);
        _attackTarget = null;
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            // Wait to attack
            while (_attackCooldown > 0f) yield return null;

            // Do attack animation
            yield return new WaitForSeconds(0.05f);

            // Shoot basic projectile
            SpawnProjectile(gameObject, transform);
            _attackCooldown = (1 / PlayerStats.AttackSpeed);

            yield return null;
        }
    }

    private void SpawnProjectile(GameObject Instigator, Transform spawnPoint = null)
    {
        ProjectileController projectile = 
            Instantiate(Resources.Load<GameObject>("Prefabs/BasicProjectile"), spawnPoint.position, Quaternion.identity).GetComponent<ProjectileController>();

        projectile.Initialize(Instigator, _projectileSpeed, PlayerStats.AttackDamage, _attackTarget.transform);
        projectile.FaceTarget();
    }
}
