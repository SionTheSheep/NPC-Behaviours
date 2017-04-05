using UnityEngine;
using System.Collections;

public class Ben : MonoBehaviour
{
	public GameObject[] benTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Chess Club";
	public string gender = "Male";

	//Out of 10
	public int knowledge = 7;
	//0 being angry, 10 being happy
	public int mood = 5;
	//0 being relaxed, 10 being scared
	public int fear = 4;

	void Awake()
	{
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		benTargets[0] = GameObject.FindGameObjectWithTag("MaleDorm");
		benTargets[1] = GameObject.FindGameObjectWithTag("School");
		benTargets[2] = GameObject.FindGameObjectWithTag("ActivitiesHall");
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
		if ((currentTime >= 0.3) && (currentTime <= 0.5))
		{
			GoToClass();
		}
		else if ((currentTime >= 0.5) && (currentTime <= 0.75))
		{
			GoToActivitiesHall();
		}
		else
		{
			GoToDorm();
		}
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

	void GoToActivitiesHall()
	{
		activeTarget = 2;
		FindNewTarget();
	}

	void FindNewTarget()
	{
		agent.destination = benTargets[activeTarget].transform.position;
	}

}
