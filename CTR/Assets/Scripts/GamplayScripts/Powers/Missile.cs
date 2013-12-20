using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Missile : Power
{

    public GameObject target;
    public string targettag;
    public float damping = 3;
    public float Speed = 80;
    public float SpeedMax = 80;
    public float SpeedMult = 1;
    public Vector3 Noise = new Vector3(20, 20, 20);
    private bool locked;
    public int distanceLock = 70;
    public int DulationLock = 40;
    private int timetorock;
    public bool Seeker;
    public float LifeTime = 5.0f;
    private float timeCount = 0;
    public float targetlockdirection = 0.5f;

    void Start()
    {
        timeCount = Time.time;
        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        if (Time.time >= (timeCount + LifeTime) - 0.5f)
        {
            if (GetComponent<Damage>())
            {
                GetComponent<Damage>();
            }
        }
        if (Seeker)
        {
            if (timetorock > DulationLock)
            {
                if (!locked && !target)
                {
                    float distance = int.MaxValue;
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(targettag);
                    if (GameObject.FindGameObjectsWithTag(targettag).Length > 0)
                    {
                        for (var i = 0; i < objs.Length; i++)
                        {
                            if (objs[i])
                            {
                                var dis = Vector3.Distance(objs[i].transform.position, transform.position);
                                if (distanceLock > dis)
                                {
                                    if (distance > dis)
                                    {
                                        distance = dis;
                                        target = objs[i];
                                    }
                                    locked = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                timetorock += 1;
            }

            if (target)
            {
                damping += 0.9f;
                var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                Vector3 dir = (target.transform.position - this.transform.position).normalized;
                float direction = Vector3.Dot(dir, this.transform.forward);
                if (direction < targetlockdirection)
                {
                    target = null;
                }
            }
            else
            {
                locked = false;
                
            }
        }
        if (Speed < SpeedMax)
        {
            Speed += SpeedMult;
        }


        Vector3 velocity = transform.forward * Speed * Time.deltaTime;
        rigidbody.velocity = velocity;
        //Vector3 noise = new Vector3(Random.Range(-Noise.x,Noise.x),Random.Range(-Noise.z,Noise.z),Random.Range(-Noise.y,Noise.y));
        // rigidbody.velocity  += noise;

    }

    public override void CollisionResponse()
    {
        //base.CollisionResponse();
        
    }

}
