using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TextManager : MonoBehaviour
{
    public int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject[] gunsIcons;
    public Text[] ammo;

    public GameObject weaponHolder;
    public GameObject currentGun;

    public string maxAmmo;
    private string currentAmmo;

    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        gunsIcons = new GameObject[totalWeapons];
        ammo = new Text[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            gunsIcons[i] = weaponHolder.transform.GetChild(i).gameObject;
            ammo[i] = weaponHolder.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
            gunsIcons[i].SetActive(false);
        }

        gunsIcons[0].SetActive(true);
        currentGun = gunsIcons[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponIndex != 0)
        {
            gunsIcons[currentWeaponIndex].SetActive(false);
            gunsIcons[0].SetActive(true);
            currentWeaponIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponIndex != 1)
        {
            gunsIcons[currentWeaponIndex].SetActive(false);
            gunsIcons[1].SetActive(true);
            currentWeaponIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeaponIndex != 2)
        {
            gunsIcons[currentWeaponIndex].SetActive(false);
            gunsIcons[2].SetActive(true);
            currentWeaponIndex = 2;
        }
    }
}
