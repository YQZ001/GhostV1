using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovementControll : MonoBehaviour {

    public Transform lamplight; 
    NavMeshAgent myNavAgent; 
    public bool isAttacked = false;
    public Transform player;
	// Use this for initialization
	void Start () {
        myNavAgent = GetComponent<NavMeshAgent>();

        lamplight = GameObject.FindGameObjectWithTag(Tags.lamplight).transform;
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;

        if (myNavAgent != null && lamplight != null)
        {
            myNavAgent.SetDestination(lamplight.position);
        }
        else
        {
            this.enabled = false;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        //myNavAgent.isStopped
        if(isAttacked){
            myNavAgent.SetDestination(player.position);
        }
      
    }
    //change agent moving destination
    public void changeDestination(Vector3 pos){
        myNavAgent.SetDestination(pos);
    }
    //make enemy stop moving 
    public void stopMoving(){
        myNavAgent.enabled = false;
    }
}
