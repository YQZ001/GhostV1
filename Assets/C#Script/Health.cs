using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHealth;
    int curHealth;
    bool isDead = false;
    ParticleSystem hurtEffect;
	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
        hurtEffect = GetComponent<ParticleSystem>();
	}
	

    public void TakeDamage(Vector3 hitPoint)
    {

        if (isDead) { return; }

        // reducehealth
        curHealth -= 1;

        //// Set the position of the hurt effect to where the hit was sustained.
        if(hurtEffect != null)
        {
            hurtEffect.transform.position = hitPoint;
            hurtEffect.Play();
        }
      
        // death
        if (curHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
    }

}
