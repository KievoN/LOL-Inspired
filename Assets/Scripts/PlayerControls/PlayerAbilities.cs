using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;

    [Header("Abilities")]
    [SerializeField] private Ability _abilityQ;
    [SerializeField] private Ability _abilityW;
    [SerializeField] private Ability _abilityE;

    private float _abilityQCooldown = 0f, _abilityWCooldown = 0f, _abilityECooldown = 0f;

    #region Ability Inputs

    public void OnQ(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled || _abilityQCooldown > 0) return;
        SpawnAbility(_abilityQ);
    }

    public void OnW(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled || _abilityWCooldown > 0) return;
        SpawnAbility(_abilityW);
    }

    public void OnE(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled || _abilityECooldown > 0) return;
        SpawnAbility(_abilityE);
    }

    #endregion

    private void SpawnAbility(Ability ability)
    {
        ability.SpawnAbility(gameObject, ability, _spawnPoint.transform);

        StartCoroutine(CooldownTimer(ability._cooldown, ability));
    }

    private IEnumerator CooldownTimer(float timer, Ability ability)
    {
        do
        {
            timer = Mathf.Max(timer -= Time.deltaTime, 0f);

            float abilityHudFill = 1 - ((timer * 100) / (ability._cooldown)) / 100;

            switch (ability._key)
            {
                case Ability.AbilityKey.Q:
                    _abilityQCooldown = timer;
                    HudAbilityUpdate.Instance.AbilityQ.fillAmount = abilityHudFill;
                    break;
                case Ability.AbilityKey.W:
                    _abilityWCooldown = timer;
                    HudAbilityUpdate.Instance.AbilityW.fillAmount = abilityHudFill;
                    break;
                case Ability.AbilityKey.E:
                    _abilityECooldown = timer;
                    HudAbilityUpdate.Instance.AbilityE.fillAmount = abilityHudFill;
                    break;
                case Ability.AbilityKey.R:
                    break;
                default:
                    break;
            }

            yield return null;
        } while (timer > 0);
    }
}
