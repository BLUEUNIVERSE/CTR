using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour 
{

public GameObject effect;
public GameObject Owner;
public int iDamage = 20;
public bool Explosive;
public float ExplosionRadius = 20;
public float ExplosionForce = 1000;
void Start () {
	if(Owner){
		if(Owner.collider != null){
			Physics.IgnoreCollision(this.collider,Owner.collider);
		}
	}
}
void Update()
{
	
}
void Active(){
	if(effect){
   		GameObject obj = Instantiate(effect,this.transform.position,this.transform.rotation) as GameObject;
   		GameObject.Destroy(obj,3);
   	}
   	
   	if(Explosive)
   	ExplosionDamage();
   	Destroy(this.gameObject);
}

void ExplosionDamage(){
	 Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, ExplosionRadius);
     for (var i = 0; i < hitColliders.Length; i++) {
           var hit = hitColliders[i];
	       if (!hit)
	            continue;

	       if(hit.gameObject.tag !="Particle" && hit.gameObject.tag!="Bullet")
		   {
               if (hit.gameObject.GetComponent<DamageManager>())
               {
                   hit.gameObject.GetComponent<DamageManager>().ApplyDamage(iDamage);
		   	 }
		   }
		   if(hit.rigidbody)
		        hit.rigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f,ForceMode.Impulse);
	 }
	 
}


void NormalDamage(Collision collision){
	if(collision.gameObject.GetComponent<DamageManager>()){
		collision.gameObject.GetComponent<DamageManager>().ApplyDamage(iDamage);
	}	
}

void OnCollisionEnter(Collision collision) {
	if(collision.gameObject.tag!="Particle" && collision.gameObject.tag!="Bullet")
    {
        if (!Explosive)
            NormalDamage(collision);
        

		Active();
	}
   	
}
}
