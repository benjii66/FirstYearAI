using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PY_Movement : MonoBehaviour
{
	[SerializeField, Range(0, 10)] float speed = 1;
	public Vector3 CurrentPosition => transform.position;
	public Vector3 MoveTarget{get;set;} = Vector3.zero;

	void Update() => MoveTo(MoveTarget);

	void MoveTo(Vector3 _target)
	{
		//info du point de départ ? mettre le target au point de départ
		transform.position = Vector3.MoveTowards(CurrentPosition, _target, Time.deltaTime * speed);
	}

}
