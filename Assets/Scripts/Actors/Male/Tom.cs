using UnityEngine;
using System.Collections;

public class Tom : MonoBehaviour
{
	public GameObject[] tomTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	private bool attendSchool = false;
	private bool attendSports = false;
	private bool attendEC = false;
	private bool attendChess = false;

	public string faction = "Sports Club";
	public string gender = "Male";

	//10 being smart
	public float knowledge = 7f;
	//0 being angry, 10 being happy
	public float mood = 4f;
	//0 being relaxed, 10 being scared
	public float fear = 6f;


	void Awake()
	{
		//Set where each target is.
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		tomTargets[0] = GameObject.FindGameObjectWithTag("MaleDorm");
		tomTargets[1] = GameObject.FindGameObjectWithTag("School");
		tomTargets[2] = GameObject.FindGameObjectWithTag("SportsHall");
		tomTargets[3] = GameObject.FindGameObjectWithTag("ActivitiesHall");
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
		//Limits each float.
		knowledge = Mathf.Clamp(knowledge, 0f, 10f);
		mood = Mathf.Clamp(mood, 0f, 10f);
		fear = Mathf.Clamp(fear, 0f, 10f);

		//Choosing objectives based on time
		float currentTime = timer.currentTimeOfDay;

		if ((currentTime >= 0.2) && (currentTime <= 0.5))
		{
			GoToClass();
		}
		else if ((currentTime >= 0.5) && (currentTime <= 0.8))
		{
			if (fear >= 8f)
			{
				GoToActivitiesHall();
			}
			else
			{
				GoToSportsHall();
			}
		}
		else
		{
			GoToDorm();
			if (attendSchool == true)
			{
				knowledge += 1f;
				attendSchool = false;
			}
			if (attendEC == true)
			{
				fear += 3f;
				attendEC = false;
			}
			if (attendChess == true)
			{
				knowledge += 1f;
				fear -= 5f;
				attendChess = false;
			}

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


	void GoToSportsHall()
	{
		activeTarget = 2;
		FindNewTarget();
	}
	void GoToActivitiesHall()
	{
		activeTarget = 3;
		FindNewTarget();
	}


	void FindNewTarget()
	{
		agent.destination = tomTargets[activeTarget].transform.position;
	}

	void OnTriggerEnter(Collider col)
	{
		switch (col.tag)
		{
			case "School":
				attendSchool = true;
				break;
			case "SportsHall":
				attendSports = true;
				break;
			case "ActivitiesHall":
				attendChess = true;
				break;

		}
	}
}
