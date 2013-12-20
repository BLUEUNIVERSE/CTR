using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour 
{

public	AudioClip[] hitsound;
public GameObject effect;
public int HP = 100;

void Start()
{

}

 public void ApplyDamage (float damage ) {

	if(hitsound.Length>0){
	 	AudioSource.PlayClipAtPoint(hitsound[Random.Range(0,hitsound.Length)], transform.position);
	}
 	HP -= (int)damage;
 	if(HP<=0){
 		Dead();
 	}
}
void Dead()
{
	if(effect)
	Instantiate(effect,this.transform.position,this.transform.rotation);
	Destroy(this.gameObject);
}

}
