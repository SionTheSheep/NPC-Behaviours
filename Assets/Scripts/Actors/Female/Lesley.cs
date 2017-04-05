using UnityEngine;
using System.Collections;

public class Lesley : AgentManager
{
	public GameObject[] lesleyTargets;
	//static List<Tranform> lesleyTargets = new List<Transform>();

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Chess Club";
	public string gender = "Female";

	//Out of 100
	public int health = 100;
	public int stamina = 100;
	public int fatigue = 0;

	//Out of 10
	public int knowledge = 9;
	//0 being angry, 10 being happy
	public int mood = 7;
	//0 being relaxed, 10 being scared
	public int fear = 5;

	//AgentManager agentManager;

	void Awake()
	{
		//lesleyTargets.Add(pointOfInterest);

		timer = GameObject.Find("Timer").GetComponent<Timer>();
		lesleyTargets[0] = GameObject.FindGameObjectWithTag("FemaleDorm");
		lesleyTargets[1] = GameObject.FindGameObjectWithTag("School");
		lesleyTargets[2] = GameObject.FindGameObjectWithTag("ActivitiesHall");
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
			GoToChessClub();
		}
		else
		{
			GoToDorm();
		}
	
		//if fatigue == 50
		//go back to dorm

	}

	public Lesley(int knwoledge, int mood, int fear)
	{

	}

	void GoToClass()
	{
		activeTarget = 1;
		FindNewTarget();
	}

	void GoToDorm()
	{
		activeTarget = 0;
		FindNewTarget();
	}

	void GoToChessClub()
	{
		activeTarget = 2;
		FindNewTarget();
	}

	void FindNewTarget()
	{
		//if (agentManager.time = 20)
		/*
		if (time = 0)
			target = target 3
		else if (time = 5)
			target = target 4
		*/

		agent.destination = lesleyTargets[activeTarget].transform.position;
	}

}