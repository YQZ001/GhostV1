using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {

    CharacterController cc;
    public float speed;
    Animator animator;

    bool isAlive = true;
    public float turnSpeed;

    public Rigidbody shell;
    public Transform muzzle;
    
    public float launchForce = 0;

    bool attacking = false;
    public float attackTime;

    public float hp;
    public float hpMax = 100;

    public Slider hpSlider;
    public Image hpFillImage;
    public Color hpColorFull = Color.red;
    public Color hpColorNull = Color.white;

    public ParticleSystem explosionEffect;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        hp = hpMax;
        RefreshHealthHUD();
    }

    public void Move(Vector3 v)
    {
        if (!isAlive) return;

        Vector3 movement = v * speed;
        cc.SimpleMove(movement);

        if (animator)
        {
            animator.SetFloat("Speed", cc.velocity.magnitude);
        }
    }

    public void Attack()
    {
        if (!isAlive) return;
        //if (attacking) return;

        var shellInstance = Instantiate(shell, muzzle) as Rigidbody;
        shellInstance.velocity = launchForce * muzzle.forward;

        if(animator)
        {
            animator.SetTrigger("Attack");
        }

        attacking = true;

        Invoke("", attackTime);
    }

    void RefreshAttack()
    {
        attacking = false;
    }

    public void Rotate(Vector3 lookDir)
    {
        var targetPos = transform.position + lookDir;
        var characterPos = transform.position;

        characterPos.y = 0;
        targetPos.y = 0;

        var faceToDir = targetPos - characterPos;

        var faceToQuat = Quaternion.LookRotation(faceToDir);

        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, turnSpeed * Time.deltaTime);
        transform.rotation = slerp;
    }

    public void Death()
    {
        isAlive = false;

        explosionEffect.transform.parent = null;
        explosionEffect.gameObject.SetActive(true);

        ParticleSystem.MainModule mainModule = explosionEffect.main;
        Destroy(explosionEffect.gameObject, mainModule.duration);

        gameObject.SetActive(false);

    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        RefreshHealthHUD();
        if (hp <= 0f && isAlive)
        {
            Death();
        }
    }

    public void RefreshHealthHUD()
    {
        hpSlider.value = hp;
        hpFillImage.color = Color.Lerp(hpColorNull, hpColorFull, hp / hpMax);

    }
}
