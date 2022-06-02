using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    public int currentAmmoSMG;
    public int currentAmmoShotgun;
    public int currentAmmoGrenade;
    public GameObject[] guns;
    private void Start() {
        for (int i = 0; i < guns.Length; i++)
        {
            switch (i)
            {
                case 0:
                currentAmmoSMG = guns[i].GetComponent<Shooting>().maxMag;
                break;
                case 1:
                currentAmmoShotgun = guns[i].GetComponent<Shooting>().maxMag;
                break;
                case 2:
                currentAmmoGrenade = guns[i].GetComponent<Shooting>().maxMag;
                break;
                default:
                break;
            }
        }
    }
}
