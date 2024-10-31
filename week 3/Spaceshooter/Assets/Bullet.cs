using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5.0f;
    public float Lifetime = 1.0f;

    private bool BulletLifeTimeCheck = false;
    private float StartTime;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
        StartTime = Time.time;
        BulletLifeTimeCheck = true;
        //Invoke("DestroyMe", Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if (BulletLifeTimeCheck && (StartTime + Lifetime < Time.time))
        {
            BulletLifeTimeCheck = false;
            Destroy(this.gameObject);
        }

        ///BAD DANGEROUS DON'T 
        //while(true)
        //{
        //    Debug.Log("hi");
        //}
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    
}
