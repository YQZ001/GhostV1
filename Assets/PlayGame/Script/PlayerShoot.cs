using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public float shootRate = 3;
    public float attack = 25;
    private float timer = 0;
    private ParticleSystem m_particleSystem;
    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        m_particleSystem = this.GetComponentInChildren<ParticleSystem>();
        lineRenderer = this.GetComponent<Renderer>() as LineRenderer;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		if(timer > 1 / shootRate)
        {
            timer -= 1/shootRate;
            Shoot();
        }
	}

    void Shoot()
    {
        GetComponent<UnityEngine.Light>().enabled = true;
        m_particleSystem.Play();

        this.lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo))
        {
            lineRenderer.SetPosition(1, hitInfo.point);

            //Decide current shoot whether push enemies.
            if(hitInfo.collider.tag == Tags.enemy)
            {
                hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(attack,hitInfo.point,this.gameObject);
            }
        } else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * 50);
        }

        this.GetComponent<AudioSource>().Play();

        Invoke("ClearEffect", 0.05f);
    }

    void ClearEffect()
    {
        GetComponent<UnityEngine.Light>().enabled = false;
        lineRenderer.enabled = false;
    }
}
