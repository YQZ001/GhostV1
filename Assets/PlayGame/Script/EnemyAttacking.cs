using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour {

    public float attack = 25;
    public int lightAttack = 1;
    public float attackTime = 1;
    private float timer;
    private EnemyHealth health;
    private LightHealth lightHealth;

    void Start()
    {
        timer = attackTime;
        health = this.GetComponent<EnemyHealth>();
      //  lightHealth = this.GetComponent<Light>();
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag == Tags.player && health.myCurHp > 0)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                timer -= attackTime;
                col.GetComponent<PlayerHealth>().TakeDamage(attack);
            }
        }
        else if (col.tag == Tags.lamplight && health.myCurHp> 0)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                timer -= attackTime;
                col.GetComponent<LightHealth>().damageLight(lightAttack);
            }
        }

        /*
        if(col.tag == Tags.lamplight && lightHealth.lightHp > 0)
        {
            timer += Time.deltaTime;
            if(timer >= attackTime)
            {
                timer -= attackTime;
                col.GetComponent<Light>().damageLight(lightAttack);
            }
        }
        */
    }
}
