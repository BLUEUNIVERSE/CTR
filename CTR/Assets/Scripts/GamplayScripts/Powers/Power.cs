using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour 
{
    public string m_sName;
    public PowerType m_Type;
    public GameObject m_SpecialEffect; 
    public AudioClip m_SoundEffect;
    public Transform m_Target;

    virtual public void PlaySoundEffect()
    {
        if(m_SoundEffect != null)
        {
            GetComponent<AudioSource>().PlayOneShot(m_SoundEffect);
        }
    }

    virtual public void SpawnSpecialEffect()
    {

    }

}

public enum PowerType 
{
    Projectile,
    GlobalHit,
    Advantage
}