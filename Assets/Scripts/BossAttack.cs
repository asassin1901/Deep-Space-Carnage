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

    public void AttackLeft(int projCount)
    {
        //originPoint[0]
        //unity animation > Instantiate projectiles > another anim to go back to pos0
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
        //Same as left / pos1 > pos2 > enable trigger > swipe across room > dmg on trigger > disable trigger > pos0
        GameObject thisProjectile;
        for (int i = 0; i < projCount; i++)
        {
            thisProjectile = Instantiate(projectile, originPoint[1].position, Quaternion.identity);
            thisProjectile.transform.Rotate(new Vector3(0 ,0 , (thisProjectile.transform.rotation.z + spread * i)));
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

    public void AttackLaser(int projCount)
    {
        /*1. Line renderer has to be turned on and off
        2. Figure out how to do a continuous raycast (Ideas: 1. Update 2. FixedUpdate 3. Some weird method or coroutine I don't know about 4. Sacrifice an infant or something idk.)
        3. Figure out how to stop a continuous raycast just in case.
        4. Profit.*/
        RaycastHit2D[] hitinfo = new RaycastHit2D[projCount];
        
        for (int i = 0; i < projCount; i++)
        {
            
        }
    }
}