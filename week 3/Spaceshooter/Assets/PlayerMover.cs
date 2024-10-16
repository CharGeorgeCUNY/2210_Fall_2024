using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    private Rigidbody2D MyRigidbody2D;
    public GameObject BulletPrefab;
    public AudioSource ShipSoundSource;

    public AudioClip ExplosionSound;

    public  List<Bullet> Bullets = new List<Bullet>();
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        ShipSoundSource = GetComponent<AudioSource>();
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
            ShipSoundSource.Play();
            
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
            AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
            //ShipSoundSource.PlayOneShot(ExplosionSound);
        }
        
        
    }
}
