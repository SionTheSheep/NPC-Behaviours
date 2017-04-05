using UnityEngine;
using System.Collections;

public class Larry : MonoBehaviour
{
	public GameObject[] larryTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Band Practice";
	public string gender = "Male";

	//Out of 10
	public int knowledge = 5;
	//0 being angry, 10 being happy
	public int mood = 6;
	//0 being relaxed, 10 being scared
	public int fear = 0;


	void Awake()
	{

		timer = GameObject.Find("Timer").GetComponent<Timer>();
		larryTargets[0] = GameObject.FindGameObjectWithTag("MaleDorm");
		larryTargets[1] = GameObject.FindGameObjectWithTag("School");
		larryTargets[2] = GameObject.FindGameObjectWithTag("MusicHall");
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
		agent.destination = larryTargets[activeTarget].transform.position;
	}
}
