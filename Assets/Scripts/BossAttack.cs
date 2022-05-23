using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int projectileDamage;
    public float spread;
    private int originCount;
    public Transform[] originPoint;
    public GameObject projectile;
    public LineRenderer lineRenderer;
    
    private void Start() 
    {
        originCount = this.transform.childCount;
        originPoint = new Transform[originCount];
        for (int i = 0; i < originCount; i++)
        {
            originPoint[i] = this.transform.GetChild(i).transform.GetChild(0);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.J))
            StartCoroutine(AttackLaser(6));
    }

    public void AttackLeft(int projCount)
    {
        //originPoint[0]
        //unity animation > (Anim trigger calls this method) Instantiate projectiles > another anim to go back to pos0
        GameObject thisProjectile;
        for (int i = 0; i < projCount; i++)
        {
            thisProjectile = Instantiate(projectile, originPoint[0].position, Quaternion.identity);
            thisProjectile.transform.Rotate(new Vector3(0 ,0 , (thisProjectile.transform.rotation.z + spread * i) + 180));
        }
    }

    public void AttackRight(int projCount)
    {
        //originPoint[1]
        GameObject thisProjectile;
        for (int i = 0; i < projCount; i++)
        {
            thisProjectile = Instantiate(projectile, originPoint[1].position, Quaternion.identity);
            thisProjectile.transform.Rotate(new Vector3(0 ,0 , (thisProjectile.transform.rotation.z + spread * i) + 90));
        }
    }

    public void AttackHead(int projCount)
    {
        //originPoint[2]
        GameObject thisProjectile;
        for (int i = 0; i < projCount; i++)
        {
            thisProjectile = Instantiate(projectile, originPoint[2].position, Quaternion.identity);
            thisProjectile.transform.Rotate(new Vector3(0 ,0 , (thisProjectile.transform.rotation.z + 15 * i)));
        }
    }
    public IEnumerator AttackLaser(int projCount)
    {
        /*1. Line renderer has to be turned on and off (IEnumerator coroutine)
        2. Figure out how to do a continuous raycast (Update)
        3. Remember to put a condition to turn it off.
        4. Okay so. I need a point of origin and to designate a direction for raycast. I also need to figure out how to draw multiple lines.*/
        RaycastHit2D[] hitinfo = new RaycastHit2D[projCount];
        
        lineRenderer.SetPosition(0, originPoint[2].position);
        for (int i = 0; i < projCount; i++)
        {
            //Okay this is cool. But: How the hell am I going to do the warning thingy? Ideas: 1. Prefab
            //2. Another for loop before this one? and line renderer has lower alfa?(Is that even possible?)
            hitinfo[i] = Physics2D.Raycast(originPoint[2].position, new Vector2(-0.75f + 0.25f * (i), -0.2f));
            lineRenderer.SetPosition(1, hitinfo[i].point);
            yield return new WaitForSeconds(0.25f);
        }
    }
}