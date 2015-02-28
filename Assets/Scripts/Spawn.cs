using UnityEngine;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

	public GameObject[] groups;

	Transform _transform;

	void Awake () {
		_transform = transform;
	}
	// Use this for initialization
	void Start () {
		SpawnGroup ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnGroup () {
		var i = Random.Range (0, groups.Length);

		Instantiate (groups [i], _transform.position, Quaternion.identity);
	}
}
