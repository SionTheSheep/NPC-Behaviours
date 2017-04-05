using UnityEngine;
using System.Collections;

public class Donna : MonoBehaviour
{
	public GameObject[] donnaTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Band Practice";
	public string gender = "Female";

	//Out of 10
	public int knowledge = 5;
	//0 being angry, 10 being happy
	public int mood = 6;
	//0 being relaxed, 10 being scared
	public int fear = 0;


	void Awake()
	{

		timer = GameObject.Find("Timer").GetComponent<Timer>();
		donnaTargets[0] = GameObject.FindGameObjectWithTag("FemaleDorm");
		donnaTargets[1] = GameObject.FindGameObjectWithTag("School");
		donnaTargets[2] = GameObject.FindGameObjectWithTag("MusicHall");
		donnaTargets[3] = GameObject.FindGameObjectWithTag("SportsPitch");
		//donnaTargets[3] = GameObject.Find("Agent").GetComponent<Agent>();
	}

	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();

		FindNewTarget();
	}

	// Update is called once per frame
	void Update()
	{
		float currentTime = timer.currentTimeOfDay;
		if ((currentTime >= 0.25) && (currentTime <= 0.5))
		{
			GoToClass();
		}
		else if ((currentTime >= 0.5) && (currentTime <= 0.75))
		{
			GoToMusicHall();
		}
		else
		{
			GoToDorm();
		}

	}

	void GoToDorm()
	{
		activeTarget = 0;
		FindNewTarget();
	}

	void GoToClass()
	{
		activeTarget = 1;
		FindNewTarget();
	}

	void GoToMusicHall()
	{
		activeTarget = 2;
		FindNewTarget();
	}

	void FindNewTarget()
	{
		agent.destination = donnaTargets[activeTarget].transform.position;
	}
}