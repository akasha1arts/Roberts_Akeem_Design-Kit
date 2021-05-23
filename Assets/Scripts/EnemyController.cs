using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Code received from Octobeard youtube video https://www.youtube.com/watch?v=KqkW3FsuPoM 
// Tool Tips added by Akeem Roberts
public class EnemyController : MonoBehaviour
{
	[Header("Using these settings you can manipulate the enemies AI and movement")]
	[Tooltip("Drag and drop Player object here")]
	public Transform player;
	
	[Tooltip("Choose distance enemy can follow player")]
	[RangeAttribute(1, 5)]
	public float playerDistance;
	
	[Tooltip("How close the Player can get till enemy is aware of them")]
	public float awareAI = 10f;
	
	[Tooltip("Adjust the speed of the AI")]
	[RangeAttribute(1, 5)]
	public float AIMoveSpeed; // Adjust the speed of the AI
	public float damping = 6.0f;
	
	[Tooltip("Add objects/waypoints to this list for enemy to walk to")]
	public Transform[] navPoint;
	
	[Tooltip("After adding a nav mesh to the enemy drag and drop the enemy here")]
	public UnityEngine.AI.NavMeshAgent agent;
	public int destPoint = 0;
	public Transform goal;
	public static float enemyHealth;

	

	[Header("Using these settings you can adjust the time the player is stunned by enemy")]
	[Tooltip("Choose amount of time player is stunned when colliding with enemy")] // Stun mechanic coded by me
	[RangeAttribute(0, 5)]
	public float timeStunned; //Time in seconds for stun to last

	

	private PlayerMovement playerMovement;

	

	public GameObject Player;

	[Tooltip("Add gameobject CanvasGO here")]
	public GameObject Canvas;


	private void Awake()
    {
		playerMovement = Player.GetComponent<PlayerMovement>();

		Canvas.SetActive(false);
    }


    void Start()
	{
		enemyHealth = 100;
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;

		agent.autoBraking = false;

	}

	void Update()


	{
		Debug.Log(enemyHealth);

		if (enemyHealth <= 0)
			Destroy(gameObject);


		playerDistance = Vector3.Distance(player.position, transform.position);

		if (playerDistance < awareAI)
		{
			LookAtPlayer();
			Debug.Log("Seen");
		}

		if (playerDistance < awareAI)
		{
			if (playerDistance < 2f)
			{
				Chase();
			}
			else
				GotoNextPoint();
		}


		if (agent.remainingDistance < 0.5f)
			GotoNextPoint();

		//StunTimer();
	}

	void LookAtPlayer()
	{
		transform.LookAt(player);
	}


	void GotoNextPoint()
	{
		if (navPoint.Length == 0)
			return;
		agent.destination = navPoint[destPoint].position;
		destPoint = (destPoint + 1) % navPoint.Length;
	}


	void Chase()
	{
		transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
	}


	void Stun() // Code for stunning player 
	{
		
		
			playerMovement.enabled = false; // Sets movement script as false 
											// timeStunned = Time.timeScale;
		Canvas.SetActive(true);
		Debug.Log("Player is stunned");

	}

	IEnumerator StunTimer()
    {
		yield return new WaitForSeconds(timeStunned);
		Debug.Log("Waiting Stun duration");
		 StunTimeOver();
		
			
		
	}


	void OnTriggerEnter(Collider other)   //Upon colliding with player stun is triggered 
	{
		if (other.gameObject == Player) // If the gameobject is Player gameObject Stun() is called
		{  
			Stun(); // Stuns gameobject tagged as Player
			Destroy(GetComponent<BoxCollider>()); //So stun won't be triggered again
			StartCoroutine(StunTimer());
		}

	}

	void StunTimeOver() // Waits for Stun time to end then enables player movement again 
	{
		playerMovement.enabled = true;
		Canvas.SetActive(false);
		Debug.Log("Player Stun time over");
	} 


}

