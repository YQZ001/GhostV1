using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightHealth : MonoBehaviour {

    UnityEngine.Light myLight;
    public int maxLightHp = 100;
    public int myLightHp; 
//float timer = 0f;
    public float flashTime = 1f;
    public GameObject gamecontroller;
    public Slider healthslider;

    // Use this for initialization
    void Start () {
        myLight = GetComponentInChildren<UnityEngine.Light>();
        if(myLight == null)
        {
            Debug.LogError("Cannot find light component");
        }
        myLightHp = maxLightHp;
	}

    public void  resetLight()
    {
        myLightHp = maxLightHp;
        updateHealthUI();
        myLight.enabled = true;
    }

     void Update()
    {
        if(myLightHp > 0)
        {
          myLight.intensity= 40.0f *( Mathf.PingPong(Time.time *flashTime, 1.0f) + 0.5f); //make it flashes
         
        }
       
           
    }
    public void damageLight(int l_damage)
    {
        if(myLightHp < 0) { return;  } //light has been blown out
        myLightHp -= l_damage;
        if(myLightHp == 0)
        {
            lightOut();
        }
        updateHealthUI();
    }

     void lightOut()
    {

        myLight.enabled = false;
        gamecontroller.GetComponent<gameController>().GameOver(); //end game
    }

    void updateHealthUI()
    {
        float cur = Mathf.Clamp(myLightHp, 0, maxLightHp);
        healthslider.value = cur / maxLightHp;

    }
}
