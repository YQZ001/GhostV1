using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    PlayerCharacter Pcharacter;
	
	public void Start () {
        Pcharacter = GetComponent<PlayerCharacter>();

	}

    void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Pcharacter.Attack();

        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Pcharacter.Move(new Vector3(h, 0, v));

        var lookDir = Vector3.forward * v + Vector3.right * h;

        if(lookDir.magnitude != 0)
        {
            Pcharacter.Rotate(lookDir);
        }
    }
}
