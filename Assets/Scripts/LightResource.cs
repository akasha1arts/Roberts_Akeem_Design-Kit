using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightResource : MonoBehaviour
{
  

    public GameObject player;


    private PlayerLight playerLight;


    private void Awake()
    {
        playerLight = player.GetComponent<PlayerLight>();
    }








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // print(lightHealth);
       
    }

    //Help from health regen tutorial from Alexander Zotov https://www.youtube.com/watch?v=KE9BHGgVP4A

    IEnumerator LightRegen() //Method for light source regenerating  // Possible Lerp function 
    {
                                                //Made it 200 so it activates regen, may make maxlightHealth later for designer
        for (float currentLightHealth = playerLight.lightHealth; currentLightHealth <= 101; currentLightHealth += playerLight.lightRegenRate)
        {
            Debug.Log("Coroutine running");
            // print(currentLightHealth);
            playerLight.lightHealth = currentLightHealth;
           // lightHealth = currentLightHealth += lightRegenRate; Tried this way and didn't add to lightHealth value in inspector
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log("Coroutine running done");
        }

        
       



       // lightHealth += lightRegenRate * Time.deltaTime;
        
    }


    private void OnTriggerEnter(Collider other) // When player collides with sphere collider light regens
    {
        if (other.gameObject == player && playerLight.lightHealth < 101 ) //Made it 200 so it activates regen 
        {
            Debug.Log("Player light Regenerating");
            playerLight.stopDecayBool = true;
            StartCoroutine(LightRegen());
            
        }
    }

    private void OnTriggerExit(Collider other) // When player leave collider regen stops
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player exited light regen field");
            playerLight.stopDecayBool = false;
            StopCoroutine(LightRegen());
            Debug.Log("Coro Stop");
        }
    }

    




}
