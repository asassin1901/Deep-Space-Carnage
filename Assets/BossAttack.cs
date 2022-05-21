using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int projectileDamage;
    public float spread;
    private int originCount;
    public Transform[] originPoint;
    public GameObject projectile;
    
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
        //originPoint[3]
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
    }

    public void AttackHead(int projCount)
    {
        //originPoint[2]
    }
}