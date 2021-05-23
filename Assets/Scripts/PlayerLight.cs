using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLight : MonoBehaviour
{

    [Tooltip("Light resource player starts with")]
    public float lightHealth; //How much light the player has 

    [RangeAttribute(0f, 5f)]
    [Tooltip("Decay rate player loses light health")]
    public float lightDecayRate;

    [RangeAttribute(0.05f, .10f)]
    [Tooltip("Rate at which player recovers light when standing in light")]
    public float lightRegenRate;

    public bool stopDecayBool = false;

 

    

    private void Awake()
    {
        lightHealth = 100f; // This ensures that the starting health wont first start at 0 causing a game over;
        
    }


    private void Update()
    {
        LightDecay();
        LightSourceEmpty();
    }




    void LightSourceEmpty() // Game over method 
    {
        if (lightHealth > 0)
        {

            Debug.Log("Light source is not empty");
        }
        else if (lightHealth <= 0)
        {
            Debug.Log("light source empty. Player dead");
            SceneManager.LoadScene("GameOver");
        }
    }






    void LightDecay() //Method for light sources depleting, needs to stop when lighthealthregen is active 
    {
        if (stopDecayBool == false)
        {
            lightHealth -= lightDecayRate * Time.deltaTime;
        }
        else if (stopDecayBool == true)
        {
            Debug.Log("light stopped decaying"); // Do nothing 
        }


    }

}
