using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyOverCam : MonoBehaviour
{
	public static FlyOverCam follow;
	//private float numOfAgents;

	//Try and get lists working, it's going to make this so much easier
	//public GameObject[] cameraTargets;
	//public List<Transform> agents;
	//private Agent targetAgent;

	public Transform agent1;
	public Transform agent2;
	public Transform[] cameraTargets;
	public Camera FlyCam;
	public float dampTime;

	public Vector3 mid;
	public Vector3 distance;
	public float camDistance;
	public float CamOffset;

	Vector3 velocity = Vector3.zero;
	public float midX;
	public float midY;
	public float midZ;

	void Awake()
	{
		cameraTargets[0] = GameObject.FindGameObjectWithTag("April").transform;
		cameraTargets[1] = GameObject.FindGameObjectWithTag("Lesley").transform;
		//Agent[] agents = (Agent[]) GameObject.FindObjectOfType(typeof(Agent));

	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		//Changing distance between agents
		//distance = (each agent position) / numberOfagents
		distance = agent1.position - agent2.position;

		camDistance = distance.z * 1.5f;
		if (camDistance <= 10.0f)
		{
			camDistance = 10.0f;
		}
		/*else if (distance.z < 15.0f)
		{
			camDistance = distance.z * 1.0f;
		}
		*/

		//Makes negative distances always positive.
		if (distance.x < 0)
		{
			distance.x = distance.x * 1;
		}
		if (distance.z < 0)
		{
			distance.z = distance.z * 1;
		}

		//Changes camera's offset depending on distance between agents.
		if (distance.x > 15.0f)
		{
			CamOffset = distance.x * 0.3f;
			if (CamOffset >= 8.5f)
			{
				CamOffset = 8.5f;
			}
		}
		else if (distance.x < 14.0f)
		{
			CamOffset = distance.x * 0.3f;
		}
		else if (distance.z < 14.0f)
		{
			CamOffset = distance.x * 0.3f;
		}

		//Only taking two characters in account at the moment.
		//Work out the middle position of all agents.
		midX = (agent2.position.x + agent1.position.x) / 2;
		midY = (agent2.position.y + agent1.position.y) / 2;
		midZ = (agent2.position.z + agent1.position.z) / 2;
		mid = new Vector3(midX, midY, midZ);

		//Makes the delta position the mid point - the camera's position on screen.
		Vector3 delta = mid - FlyCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + CamOffset));
		Vector3 destination = transform.position + delta;
		//Makes the camera smooooooother.
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	}
}