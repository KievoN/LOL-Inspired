using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile")]
public class AbilityProjectile : Ability
{
    public float ProjectileSpeed = 1f;
    [Min(0)] public int[] Damage = { 30, 50, 70 };
    public int DistanceSpan = 100;

    public override void SpawnAbility(GameObject Instigator, Ability ability, Transform spawnPoint = null)
    {
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfo))
        {
            Vector3 mouseHitPointFlat = new(hitInfo.point.x, 0f, hitInfo.point.z);
            Vector3 spawnPointFlat = new(spawnPoint.position.x, 0f, spawnPoint.position.z);
            Vector3 forward = mouseHitPointFlat - spawnPointFlat;
            Debug.Log(forward);

            GameObject projectile = Instantiate(Resources.Load<GameObject>("Prefabs/Projectile"), spawnPoint.position,
                Quaternion.LookRotation(forward, Vector3.up));

            // Initialize projectile stats
            projectile.GetComponent<ProjectileController>().Initialize(Instigator, ProjectileSpeed, Damage[0], DistanceSpan);
        }

    }
}
