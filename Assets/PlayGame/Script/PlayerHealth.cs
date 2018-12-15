using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float maxHp = 100;
    public float hp = 100;
    public float smoothing = 5;
    private Animator anim;
    private PlayerMove playerMove;
    private SkinnedMeshRenderer bodyRenderer;
    private PlayerShoot playerShoot;
    public GameObject gamecontroller;
    public Slider healthslider;
    public Transform healthsliderTransform;
   
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        this.playerMove = this.GetComponent<PlayerMove>();
        this.bodyRenderer = transform.Find("Player").GetComponent<Renderer>() as SkinnedMeshRenderer;
        //this.bodyRenderer = transform.Find("Player").renderer as SkinnedMeshRenderer
        playerShoot = this.GetComponentInChildren<PlayerShoot>();
        hp = maxHp;
    }

    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            TakeDamage(30);
        }
        */
        bodyRenderer.material.color = Color.Lerp(bodyRenderer.material.color, Color.white, smoothing*Time.deltaTime);
        updateHealthBarPosition();
    }

    void updateHealthUI()
    {
        float cur = Mathf.Clamp(hp, 0, maxHp);
        healthslider.value = cur / maxHp;

    }
    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        this.hp -= damage;
        bodyRenderer.material.color = Color.red;

        if(this.hp <= 0)
        {
            anim.SetTrigger("Die");
            Dead();
        }
        updateHealthUI();
    } 

    void Dead()
    {
        this.playerMove.enabled = false;
        this.playerShoot.enabled = false;

        gamecontroller.GetComponent<gameController>().GameOver();
    }

    public void resetPlayer()
    {
        playerMove.enabled = true;
        playerShoot.enabled = true;
        hp = maxHp;
        updateHealthUI();
    }

    void updateHealthBarPosition()
    {

        //Vector3 curPos = this.transform.position;
        //healthsliderTransform.position = new Vector3(curPos.x, curPos.y, curPos.z);

        healthsliderTransform.rotation = new Quaternion(0f, transform.rotation.y, 0f, 0f);
        //healthsliderTransform.LookAt(Camera.main.transform);
    }
}
