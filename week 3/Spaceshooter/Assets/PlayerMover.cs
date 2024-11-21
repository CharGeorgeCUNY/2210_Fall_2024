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
    public GameObject ThrusterPoint;
    public AudioClip ExplosionSound;
    public ParticleSystem exhaust;
    public GameObject fire;

    public  List<Bullet> Bullets = new List<Bullet>();
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        ShipSoundSource = GetComponent<AudioSource>();
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowShip(.5f));
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal");
        float fullRotation = rotation * RotationSpeed;
        MyRigidbody2D.MoveRotation(MyRigidbody2D.rotation + (fullRotation * Time.deltaTime));
        fire.GetComponent<SpriteRenderer>().enabled = true;
        fire.GetComponent<SpriteRenderer>().enabled = false;

        if (Input.GetAxis("Vertical") > 0f)
        {
            MyRigidbody2D.AddForce(transform.up * (Speed * Input.GetAxis("Vertical")));
            exhaust.Emit(1);
            RaycastHit2D hit = Physics2D.Raycast(ThrusterPoint.transform.position, -transform.up, 3.0f);
            Debug.DrawRay(transform.position, -transform.up*3.0f);
            if (hit && hit.collider.gameObject.GetComponent<Asteroid>())
            {
                hit.rigidbody.AddForce(-transform.up * 3.0f);
               
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject myBullet =  GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
            Bullets.Add(myBullet.GetComponent<Bullet>());
            ShipSoundSource.Play();
            
        }
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.0f);
            Debug.DrawRay(transform.position, transform.up * 3.0f);
            if (hit && hit.collider.gameObject.GetComponent<Asteroid>())
            {
                Destroy(hit.collider.gameObject);
                GameManager.GetGameManager().IncrementScore();

            }
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
            //StartCoroutine(GrowShip(3f));
            Bullets.Clear();
            AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
            //ShipSoundSource.PlayOneShot(ExplosionSound);
        }
    }

    IEnumerator GrowShip(float TimeToGrow)
    {
        float CurrentTime = Time.timeSinceLevelLoad;
        float TargetTime = CurrentTime + TimeToGrow;
        float LoopTime = CurrentTime;
        while(LoopTime < TargetTime)
        {
            //transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, .2f);
            //float TotalTime = TargetTime - CurrentTime ;
            //float t = (LoopTime - CurrentTime) / TotalTime;
            float t =  Mathf.InverseLerp(CurrentTime, TargetTime, LoopTime);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, InOutBounce(t));
            LoopTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1.0f);



    }

    public float tween(float x)
    {
        return (x == 0f) ? 0f : x == 1f ? 1f : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
    }

    public static float Linear(float t) => t;

    public static float InQuad(float t) => t * t;
    public static float OutQuad(float t) => 1 - InQuad(1 - t);
    public static float InOutQuad(float t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }

    public static float InCubic(float t) => t * t * t;
    public static float OutCubic(float t) => 1 - InCubic(1 - t);
    public static float InOutCubic(float t)
    {
        if (t < 0.5) return InCubic(t * 2) / 2;
        return 1 - InCubic((1 - t) * 2) / 2;
    }

    public static float InQuart(float t) => t * t * t * t;
    public static float OutQuart(float t) => 1 - InQuart(1 - t);
    public static float InOutQuart(float t)
    {
        if (t < 0.5) return InQuart(t * 2) / 2;
        return 1 - InQuart((1 - t) * 2) / 2;
    }

    public static float InQuint(float t) => t * t * t * t * t;
    public static float OutQuint(float t) => 1 - InQuint(1 - t);
    public static float InOutQuint(float t)
    {
        if (t < 0.5) return InQuint(t * 2) / 2;
        return 1 - InQuint((1 - t) * 2) / 2;
    }

    public static float InSine(float t) => 1 - (float)Mathf.Cos(t * Mathf.PI / 2);
    public static float OutSine(float t) => (float)Mathf.Sin(t * Mathf.PI / 2);
    public static float InOutSine(float t) => (float)(Mathf.Cos(t * Mathf.PI) - 1) / -2;

    public static float InExpo(float t) => (float)Mathf.Pow(2, 10 * (t - 1));
    public static float OutExpo(float t) => 1 - InExpo(1 - t);
    public static float InOutExpo(float t)
    {
        if (t < 0.5) return InExpo(t * 2) / 2;
        return 1 - InExpo((1 - t) * 2) / 2;
    }

    public static float InCirc(float t) => -((float)Mathf.Sqrt(1 - t * t) - 1);
    public static float OutCirc(float t) => 1 - InCirc(1 - t);
    public static float InOutCirc(float t)
    {
        if (t < 0.5) return InCirc(t * 2) / 2;
        return 1 - InCirc((1 - t) * 2) / 2;
    }

    public static float InElastic(float t) => 1 - OutElastic(1 - t);
    public static float OutElastic(float t)
    {
        float p = 0.3f;
        return (float)Mathf.Pow(2, -10 * t) * (float)Mathf.Sin((t - p / 4) * (2 * Mathf.PI) / p) + 1;
    }
    public static float InOutElastic(float t)
    {
        if (t < 0.5) return InElastic(t * 2) / 2;
        return 1 - InElastic((1 - t) * 2) / 2;
    }

    public static float InBack(float t)
    {
        float s = 1.70158f;
        return t * t * ((s + 1) * t - s);
    }
    public static float OutBack(float t) => 1 - InBack(1 - t);
    public static float InOutBack(float t)
    {
        if (t < 0.5) return InBack(t * 2) / 2;
        return 1 - InBack((1 - t) * 2) / 2;
    }

    public static float InBounce(float t) => 1 - OutBounce(1 - t);
    public static float OutBounce(float t)
    {
        float div = 2.75f;
        float mult = 7.5625f;

        if (t < 1 / div)
        {
            return mult * t * t;
        }
        else if (t < 2 / div)
        {
            t -= 1.5f / div;
            return mult * t * t + 0.75f;
        }
        else if (t < 2.5 / div)
        {
            t -= 2.25f / div;
            return mult * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / div;
            return mult * t * t + 0.984375f;
        }
    }
    public static float InOutBounce(float t)
    {
        if (t < 0.5) return InBounce(t * 2) / 2;
        return 1 - InBounce((1 - t) * 2) / 2;
    }

}
