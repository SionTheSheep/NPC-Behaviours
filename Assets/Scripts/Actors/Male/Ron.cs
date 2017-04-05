using UnityEngine;
using System.Collections;

public class Ron : MonoBehaviour
{
	public GameObject[] ronTargets;

	NavMeshAgent agent;
	int activeTarget = 0;
	Timer timer;

	public string faction = "Band Practice";
	public string gender = "Male";

	//Out of 10
	public int knowledge = 7;
	//0 being angry, 10 being happy
	public int mood = 2;
	//0 being relaxed, 10 being scared
	public int fear = 0;


	void Awake()
	{

		timer = GameObject.Find("Timer").GetComponent<Timer>();
		ronTargets[0] = GameObject.FindGameObjectWithTag("MaleDorm");
		ronTargets[1] = GameObject.FindGameObjectWithTag("School");
		ronTargets[2] = GameObject.FindGameObjectWithTag("MusicHall");
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
		agent.destination = ronTargets[activeTarget].transform.position;
	}
}
