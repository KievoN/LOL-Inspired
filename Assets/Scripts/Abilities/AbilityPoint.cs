using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Point")]
public class AbilityPoint : Ability
{
    public float ProjectileSpeed = 1f;
    [Min(0)] public int[] Damage = { 30, 50, 70 };
    public int DistanceSpan = 100;
    public int AbilityRange = 550;
    public bool TeleportToPoint = false;

    public override void SpawnAbility(GameObject Instigator, Ability ability, Transform spawnPoint = null)
    {
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfo))
        {
            GameObject point = Instantiate(Resources.Load<GameObject>("Prefabs/Point"), CappedDistance(Instigator, hitInfo.point), Quaternion.identity);

            // Initialize stats
            point.GetComponent<PointController>().Initialize(Instigator, Damage[0], DistanceSpan, TeleportToPoint);
        }
    }

    private Vector3 CappedDistance(GameObject Instigator, Vector3 hitPoint)
    {
        Vector3 playerPos = Instigator.transform.position;
        Vector3 playerPosFlat = new(playerPos.x, 0f, playerPos.z);
        Vector3 hitPointFlat = new(hitPoint.x, 0f, hitPoint.z);

        // Get the direction and distance to target
        Vector3 targetDirection = (hitPointFlat - playerPosFlat).normalized;
        float targetDistance = Vector3.Distance(hitPointFlat, playerPosFlat);

        // Keep the ability point within the ability range limit
        if (targetDistance > AbilityRange / 100)
        {
            // Targeted point is out of range
            // Clamp it within the ability range
            Debug.Log("Direction: " + targetDirection + "   " + "Magnitude: " + targetDirection.magnitude);
            return targetDirection * AbilityRange / 100;
        }
        else return hitPoint;
    }
}