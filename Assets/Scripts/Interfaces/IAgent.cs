using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent 
{
	
	Vector3 Position { get; }
	PY_Path CurrentPath { get; }
	bool IsValidAgent { get; }
	bool IsRewind { get;}
	void InitPathAgent();
	void UpdateNav();
	void UpdateNavDelayed();
	void SetNextTarget(Vector3 _pos);

	void SetPreviousTarget(Vector3 _pos);
	
}
