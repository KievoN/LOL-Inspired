using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudAbilityUpdate : MonoBehaviour
{
    public static HudAbilityUpdate Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Image AbilityQ;
    public Image AbilityW;
    public Image AbilityE;

    private void Update()
    {
        if (AbilityQ.fillAmount >= 1) SetAbilityAlpha(AbilityQ, 1f);
        else SetAbilityAlpha(AbilityQ, 0.5f);

        if (AbilityW.fillAmount >= 1) SetAbilityAlpha(AbilityW, 1f);
        else SetAbilityAlpha(AbilityW, 0.5f);

        if (AbilityE.fillAmount >= 1) SetAbilityAlpha(AbilityE, 1f);
        else SetAbilityAlpha(AbilityE, 0.5f);
    }

    private void SetAbilityAlpha(Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
