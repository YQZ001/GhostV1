using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed = 1;
    Rigidbody m_rigidbody;

    private ETCJoystick joystick;

    private Animator anim;
    private int groundLayerIndex = -1;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        groundLayerIndex = LayerMask.GetMask("Ground");
	}

    public void RunAnimatorOpen()
    {
        try
        {
            anim.SetInteger("Moving", 1);
        }
        catch
        {
            Debug.Log("Cannot do move animation");
        }
      
    }

    public void RunAnimatorStop()
    {
        //  anim.SetBool("Move", false);
        anim.SetInteger("Moving", -1);
    }

    // Update is called once per frame
    void Update () {
    //    Controll movement
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");

    //    transform.Translate(new Vector3(h, 0, v) * speed*Time.deltaTime);
    //    m_rigidbody.MovePosition(transform.position + new Vector3(h, 0, v) * speed * Time.deltaTime);

    //    Controll turning
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;
    //    if(Physics.Raycast(ray, out hitInfo, 100, groundLayerIndex))
    //    {
    //        Vector3 target = hitInfo.point;
    //        target.y = transform.position.y;
    //        transform.LookAt(target);
    //    }

    //    Controll animation
    //    if (h != 0 || v != 0) {
    //        anim.SetInteger("Moving", 1);
    //    } else {
    //        anim.SetInteger("Moving", -1);
    //    }
    }

}
