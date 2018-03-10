using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    public GameObject leader;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float dis = Vector3.Distance(leader.transform.position, transform.position);
        Vector3 newPos = leader.transform.position;
        newPos.y = 0.5f;
        float T = Time.deltaTime * dis * 9;
        transform.position = Vector3.Slerp(transform.position, newPos, T);
        //? transform.rotation = Quaternion.Slerp(transform.rotation, leader.transform.rotation, T);
    }
}
