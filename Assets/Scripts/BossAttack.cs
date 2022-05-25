using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask layerMaskIP; // The same but this one ignores player. We'll need that.
    public int projectileDamage;
    public float spread;
    private int originCount;
    public Transform[] originPoint;
    public GameObject projectile;
    public LineRenderer lineRenderer;
    public GameObject telegraph;
    private Rigidbody2D rb;
    
    private void Start() 
    {
        originCount = this.transform.childCount;
        originPoint = new Transform[originCount];
        for (int i = 0; i < originCount; i++)
        {
            originPoint[i] = this.transform.GetChild(i).transform.GetChild(0);
        }
        lineRenderer.enabled = false;
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
        GameObject[] thisTelegraph = new GameObject[projCount];
        
        int a = layerMask;
        int b = layerMaskIP;
        lineRenderer.SetPosition(0, originPoint[2].position);
        
        //Okay start another raycast find a vector 2 point in between .point and origin.
        //Instantiate an object in therere. Fuck around with Vector2 rotation/direction.
        for (int i = 0; i < projCount; i++)
        {
            //We cast a raycast so we have info we need.
            hitinfo[i] = Physics2D.Raycast(originPoint[2].position, new Vector2(-0.25f + 0.125f * (i), -0.3f), Mathf.Infinity, b);
            //We find out where is a middle point between origin and raycast hit
            middlePoint[i] = new Vector2((originPoint[2].position.x + hitinfo[i].point.x) / 2, (originPoint[2].position.y + hitinfo[i].point.y) / 2);
            //We need that later for scaling things
            distance[i] = Vector2.Distance(originPoint[2].position, middlePoint[i]);
            //We spawn an object in the middle between origin and raycast hit
            thisTelegraph[i] = Instantiate(telegraph, new Vector3(middlePoint[i].x, middlePoint[i].y, 0f), Quaternion.identity);

            //We rotate said object so it covers the area we want it to.
            Vector3 dir = originPoint[2].transform.position - thisTelegraph[i].transform.position;
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
            thisTelegraph[i].transform.rotation = Quaternion.Lerp(thisTelegraph[i].transform.rotation, rot, 10f);

            //We scale the sprite up so it covers the whole distance between origin point and wherever raycast hits
            thisTelegraph[i].transform.localScale = new Vector3(0.5f, distance[i] + 2.1f, 1f);
            SpriteRenderer spritey = thisTelegraph[i].GetComponent<SpriteRenderer>();
            spritey.color = new Color(spritey.color.r, spritey.color.g, spritey.color.b, spritey.color.a / 2f);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < projCount; i++)
        {
            Destroy(thisTelegraph[i].gameObject);
        }

        lineRenderer.enabled = true;

        for (int i = 0; i < projCount; i++)
        {
            hitinfo[i] = Physics2D.Raycast(originPoint[2].position, new Vector2(-0.25f + 0.125f * (i), -0.3f), Mathf.Infinity, a);
            lineRenderer.SetPosition(1, hitinfo[i].point);
            yield return new WaitForSeconds(0.25f);
        }
            lineRenderer.enabled = false;
    }
}