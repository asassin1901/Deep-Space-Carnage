using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public int projectileDamage;
    public float spread;
    private int originCount;
    public Transform[] originPoint;
    public GameObject projectile;
    public LineRenderer lineRenderer;
    public GameObject telegraph;
    
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
        Vector2[] middlePoint = new Vector2[projCount];
        float[] distance = new float[projCount];
        lineRenderer.enabled = true;
        GameObject thisTelegraph;
        
        int a = layerMask;
        
        //Okay start another raycast find a vector 2 point in between .point and origin.
        //Instantiate an object in therere. Fuck around with Vector2 rotation/direction.
        for (int i = 0; i < projCount; i++)
        {
            hitinfo[i] = Physics2D.Raycast(originPoint[2].position, new Vector2(-0.25f + 0.125f * (i), -0.3f), Mathf.Infinity, a);
            middlePoint[i] = new Vector2((originPoint[2].position.x + hitinfo[i].point.x) / 2, (originPoint[2].position.y + hitinfo[i].point.y) / 2);
            distance[i] = Vector2.Distance(originPoint[2].position, middlePoint[i]);
            thisTelegraph = Instantiate(telegraph, new Vector3(middlePoint[i].x, middlePoint[i].y, 0f), Quaternion.identity);
        }

        lineRenderer.SetPosition(0, originPoint[2].position);
        for (int i = 0; i < projCount; i++)
        {
            hitinfo[i] = Physics2D.Raycast(originPoint[2].position, new Vector2(-0.25f + 0.125f * (i), -0.3f), Mathf.Infinity, a);
            lineRenderer.SetPosition(1, hitinfo[i].point);
            yield return new WaitForSeconds(0.25f);
        }
            lineRenderer.enabled = false;
    }
}