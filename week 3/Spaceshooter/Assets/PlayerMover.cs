using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    private Rigidbody2D MyRigidbody2D;
    public GameObject BulletPrefab;

    public  List<Bullet> Bullets = new List<Bullet>();
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal");
        float fullRotation = rotation * RotationSpeed;
        MyRigidbody2D.MoveRotation(MyRigidbody2D.rotation + (fullRotation * Time.deltaTime));

        if(Input.GetAxis("Vertical") > 0f)
        {
            MyRigidbody2D.AddForce(transform.up * (Speed * Input.GetAxis("Vertical")));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject myBullet =  GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
            Bullets.Add(myBullet.GetComponent<Bullet>());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //   Destroy(Bullets[0]);
            //   Destroy(Bullets[1]);
            foreach (Bullet singleBullet in Bullets)
            {
                if(singleBullet != null)
                { 
                    Destroy(singleBullet.gameObject);
                }
            }
            Bullets.Clear();
        }
        
    }
}
