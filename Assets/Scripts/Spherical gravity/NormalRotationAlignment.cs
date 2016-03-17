using UnityEngine;

public class NormalRotationAlignment: MonoBehaviour
{
	public float planetLerp = 1f;
	public float linkDistance = 10f;
	public float rotateCatchupSpeed = 1f;
	public float rotateSpeed = 0.1f;

	private Transform target;
	private float previousTarget;
	private Quaternion startQuat;
	private Vector3 startUp;

	private bool touchedSomething = false;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 dwn = transform.TransformDirection(Vector3.down);
		RaycastHit hit1;
		RaycastHit hit2;
		touchedSomething = false;
		if (Physics.Raycast(transform.position, dwn, out hit1, linkDistance))
		{
			linkToPlanet(hit1);
		}
		if (Physics.Raycast(transform.position, transform.up, out hit2, linkDistance))
		{
			if (hit2.distance < hit1.distance)
			{
				linkToPlanet(hit2);
			}
		}
		if (!touchedSomething)
		{
			transform.Rotate(rotateCatchupSpeed * Time.deltaTime, 0f, 0f);
		}
		touchedSomething = false;
	}
	void linkToPlanet(RaycastHit planet)
	{
		if (planet.transform.tag == Tags.PLANET)
		{
			touchedSomething = true;
			Transform lastPlanet = this.target;
			this.target = planet.transform;
			if (this.target != lastPlanet)
			{
				gameObject.GetComponent<CustomGravity>().ChangePlanet(this.target); // changes target obj in player, change the calling method?
				previousTarget = Time.time;
				startQuat = transform.rotation;
				startUp = transform.up;
			}
			float frac = (Time.time - previousTarget);
			transform.rotation = Quaternion.FromToRotation(transform.up, planet.normal) * transform.rotation;
		}
	}
}