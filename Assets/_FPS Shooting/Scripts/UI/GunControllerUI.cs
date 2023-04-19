using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunControllerUI : MonoBehaviour
{
    public Image gunImage;
    public Image prevGunImage;
    public Image nextGunImage;
    public Image hitmarkerImage;
    public TextMeshProUGUI ammoText;

    CrosshairController crosshair;
    GunHandler currentGun;
    GunHandler prevGun;
    GunHandler nextGun;

    float showingHit = 0;

    private void Start()
    {
        hitmarkerImage.color = new Color(1f, 1f, 1f, 0f);
        crosshair = FindObjectOfType<CrosshairController>();
        // prevGun = FindObjectOfType<GunHandler>();
    }

    private void Update()
    {
        gunImage.enabled = (currentGun != null && currentGun.gun.gunIcon != null);
        ammoText.enabled = (currentGun != null);
        // prevGunImage.enabled = (prevGun != null && prevGun.gun.gunIcon != null);
        // nextGunImage.enabled = (nextGun != null && nextGun.gun.gunIcon != null);

        if (showingHit <= 0)
        {
            Color hitColor = hitmarkerImage.color;
            hitColor.a = Mathf.Lerp(hitColor.a, 0, Time.deltaTime * 16f);
            hitmarkerImage.color = hitColor;
        }
        else
            showingHit -= Time.deltaTime;

        if (currentGun == null) return;
        ammoText.text = currentGun.ammoInClip.ToString();
        if (currentGun.gun.startingClips >= 0)
            ammoText.text += " | " + currentGun.totalAmmo;
    }

    public void UpdateSelectedGunUI(GunHandler selectedGun)
    {
        if(selectedGun.gun.gunIcon != null)
            gunImage.sprite = selectedGun.gun.gunIcon;
        ammoText.rectTransform.offsetMax = new Vector2(-selectedGun.gun.ammoOffsetX, 0);
        currentGun = selectedGun;

        
    }

    public void UpdateNextPrevGuns(GunHandler prevGun ,GunHandler nextGun)
    {
        if(prevGun.gun.gunIcon != null)
            prevGunImage.sprite = prevGun.gun.gunIcon;
        if(nextGun.gun.gunIcon != null)
            nextGunImage.sprite = nextGun.gun.gunIcon;  
    }


    public void SetCrosshair(float s, bool h)
    {
        crosshair.SetCrosshair(s, h);
    }

    public void ShowHitmarker(bool killed)
    {
        hitmarkerImage.color = (killed) ? Color.red : Color.white;
        showingHit = 0.1f;
    }
}
