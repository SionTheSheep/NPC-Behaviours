using UnityEngine;
using System.Collections;

public class Andy : MonoBehaviour
{
	public GameObject[] andyTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	private bool attendSchool = false;
	private bool attendSports = false;
	private bool attendEC = false;

	public string faction = "Sports Club";
	public string gender = "Male";

	//10 being smart
	public float knowledge = 4f;
	//0 being angry, 10 being happy
	public float mood = 7f;
	//0 being relaxed, 10 being scared
	public float fear = 3f;


	void Awake()
	{
		//Set where each target is.
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		andyTargets[0] = GameObject.FindGameObjectWithTag("MaleDorm");
		andyTargets[1] = GameObject.FindGameObjectWithTag("School");
		andyTargets[2] = GameObject.FindGameObjectWithTag("SportsPitch");
		andyTargets[3] = GameObject.FindGameObjectWithTag("SportsHall");
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
			if (fear >= 8f)
			{
				GoToSportsPitch();
			}
			else
			{
				GoToClass();
			}
		}
		else if ((currentTime >= 0.5) && (currentTime <= 0.8))
		{
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
				fear += 3f;
				attendSchool = false;
			}
			if (attendSports == true)
			{
				fear -= 1f;

				attendSchool = false;
			}
			if (attendEC == true)
			{
				fear -= 1f;
				mood += 1f;
				attendEC = false;
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

	void GoToSportsPitch()
	{
		activeTarget = 2;
		FindNewTarget();
	}

	void GoToSportsHall()
	{
		activeTarget = 3;
		FindNewTarget();
	}


	void FindNewTarget()
	{
		agent.destination = andyTargets[activeTarget].transform.position;
	}

	void OnTriggerEnter(Collider col)
	{
		switch (col.tag)
		{
			case "School":
				attendSchool = true;
				break;
			case "SportsPitch":
				attendEC = true;
				break;
			case "SportsHall":
				attendSports = true;
				break;
		}
	}
}
