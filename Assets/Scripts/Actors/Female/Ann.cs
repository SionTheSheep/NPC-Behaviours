using UnityEngine;
using System.Collections;

public class Ann : MonoBehaviour
{
	public GameObject[] annTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Chess Club";
	public string gender = "Female";

	//Out of 10
	public int knowledge = 7;
	//0 being angry, 10 being happy
	public int mood = 7;
	//0 being relaxed, 10 being scared
	public int fear = 3;


	void Awake()
	{
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		annTargets[0] = GameObject.FindGameObjectWithTag("FemaleDorm");
		annTargets[1] = GameObject.FindGameObjectWithTag("School");
		annTargets[2] = GameObject.FindGameObjectWithTag("ActivitiesHall");
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


	void GoToChessClub()
	{
		activeTarget = 2;
		FindNewTarget();
	}

	void FindNewTarget()
	{
		agent.destination = annTargets[activeTarget].transform.position;
	}

}
