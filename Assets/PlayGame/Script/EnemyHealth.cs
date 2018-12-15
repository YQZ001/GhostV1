using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public GameObject gamecontroller;
    public GameObject healthBarPrefab;
    private GameObject myHealthBar;
    public Canvas SliderObject;
    public Slider healthslider;
   // public Transform healthBarPos;
    public float maxHp = 100;
    public float myCurHp = 100;
    public float offset = 1f;
    private Animator anim;
    private NavMeshAgent agent;
    private EnemyMovementControll move;
    private CapsuleCollider capsuleCollider;
    private ParticleSystem m_particleSystem;
    public AudioClip deathClip;
    private EnemyAttacking enemyAttack;



    void Awake()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        move = this.GetComponent<EnemyMovementControll>();
        capsuleCollider = this.GetComponent<CapsuleCollider>();
        m_particleSystem = this.GetComponentInChildren<ParticleSystem>();
        enemyAttack = this.GetComponentInChildren<EnemyAttacking>();
        gamecontroller = GameObject.FindGameObjectWithTag("GameController");
        //healthBarPrefab = 
        myHealthBar = healthBarPrefab; //Instantiate<GameObject>(healthBarPrefab);
        SliderObject = myHealthBar.GetComponentInChildren<Canvas>();
        healthslider = myHealthBar.GetComponentInChildren<Slider>();
        myCurHp = maxHp;
         updateHealthSliderUI();
    }

    void Update()
    {
        if(this.myCurHp <= 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.15f);
            if(transform.position.y <= -2)
            {
                Destroy(this.gameObject);
            }

        }
        else
        {
            updateHealthBarPosition();
        }
    }
    void updateHealthSliderUI()
    {
         
        float curHp = Mathf.Clamp(myCurHp, 0, maxHp);
        healthslider.value = curHp / maxHp;

    }
    void updateHealthBarPosition()
    {
        Vector3 curPos = this.transform.position;
        myHealthBar.transform.position = new Vector3(curPos.x, curPos.y + offset, curPos.z);
        myHealthBar.transform.LookAt(Camera.main.transform);
    }

    public void TakeDamage(float damage, Vector3 hitPoint, GameObject target)
    {
        if (this.myCurHp <= 0) return;

        GetComponent<AudioSource>().Play();

        m_particleSystem.transform.position = hitPoint;
        m_particleSystem.Play();

        this.myCurHp -= damage;
        if(this.myCurHp <= 0)
        {
            Dead();
        }
         updateHealthSliderUI();
        //else move to the attacked target
        if (agent.enabled){
            //GetComponent<EnemyMovementControll>().target = target;
            GetComponent<EnemyMovementControll>().isAttacked = true;


        }
      

        
    }

    // To deal with the situation after dying
    void Dead()
    {
        anim.SetTrigger("Dead");
        agent.enabled = false;
        move.enabled = false;
        SliderObject.enabled = false;
        capsuleCollider.enabled = false;
        AudioSource.PlayClipAtPoint(deathClip, transform.position, 0.5f);
        enemyAttack.enabled = false;
        gamecontroller.GetComponent<gameController>().addScore(); //improve score
    }
}
