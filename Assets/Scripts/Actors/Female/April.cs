using UnityEngine;
using System.Collections;

public class April : MonoBehaviour
{
	public GameObject[] aprilTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	private bool attendSchool = false;
	private bool attendSports = false;
	private bool attendEC = false;

	public string faction = "Sports Club";
	public string gender = "Female";

	//10 being smart
	public float knowledge = 4f;
	//0 being angry, 10 being happy
	public float mood = 3f;
	//0 being relaxed, 10 being scared
	public float fear = 5f;


	void Awake()
	{
		//Set where each target is.
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		aprilTargets[0] = GameObject.FindGameObjectWithTag("FemaleDorm");
		aprilTargets[1] = GameObject.FindGameObjectWithTag("School");
		aprilTargets[2] = GameObject.FindGameObjectWithTag("SportsPitch");
		aprilTargets[3] = GameObject.FindGameObjectWithTag("SportsHall");
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
			if (mood <= 2f)
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
				mood -= 3f;
				attendSchool = false;
			}
			if (attendSports == true)
			{
				mood += 1f;

				attendSchool = false;
			}
			if (attendEC == true)
			{
				fear += 1f;
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
		agent.destination = aprilTargets[activeTarget].transform.position;
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
