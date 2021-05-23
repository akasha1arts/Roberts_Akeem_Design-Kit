using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PowerUps : MonoBehaviour
{
    [Header("Use these tools to change values of speed power up")]

    [Tooltip("drag and drop player gameobject here")]
    public PlayerMovement playerMovement;

    [Tooltip("Use this to increase player speed by a certain amount")]
    [RangeAttribute(0f, 2f)]
    public float increaseSpeed;

    [Tooltip("Drag and drop player gameobject here")]
    public GameObject player;

    [Tooltip("Use this to manipulate speed increase duration")]
    [RangeAttribute(0f, 5f)]
    public float increasedSpeedDuration;

    public Renderer speedUp;
    private void Awake()
    {
        
    }









    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpeedDuration()
    {
        Debug.Log("speed duration coroutine running");
        yield return new WaitForSeconds(increasedSpeedDuration);
        SpeedReturn();
    }

  

    void SpeedIncreased()
    {
        playerMovement.speed += increaseSpeed; // Speed increased 
        Debug.Log("Speed Increased");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            SpeedIncreased();
            speedUp.enabled = false;
            StartCoroutine(SpeedDuration());

        }
    }
    void SpeedReturn()
    {
        playerMovement.speed -= increaseSpeed;
        Debug.Log("Speed Normal");
    }
}
