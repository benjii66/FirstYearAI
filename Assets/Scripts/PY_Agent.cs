using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PY_Agent : MonoBehaviour, IAgent
{
	#region Serialize Field
	[SerializeField] PY_Path currentPath = null;
	[SerializeField] PY_Movement movement = null;
	[SerializeField] bool reverseMove = false;

	#endregion Serialize Field

	#region F/P
	public Vector3 Position => transform.position;

	public PY_Path CurrentPath => currentPath;
	public bool IsValidAgent => currentPath;
	public bool IsValid => movement;

	public bool IsRewind
	{
		get => reverseMove;
	}

	#endregion F/P

	#region Unity Methods

	void Start() => Init();
	void Update() => UpdateNavDelayed();

	#endregion Unity Methods 

	#region Personnal Methods
	void Init() => InitPathAgent();
	public void InitPathAgent()
	{
		if (!IsValidAgent || !IsValid) return;
		currentPath.SetAgent(this);
	}

	public void UpdateNav()
	{
		if (!IsValidAgent || !IsValid) return;
		bool _next = currentPath.IsAtPos(this);
		if (_next)
		{
			currentPath.SetNextPoint();
			//currentPath.SetNextPointDelay();
			movement.MoveTarget = CurrentPath.CurrentNavPosition;
		}

		//TODO Test arrivé
		//Nav 
	}

	public void UpdateNavDelayed()
	{
		if (!IsValidAgent || !IsValid) return;
		bool _next = currentPath.IsAtPos(this);
		if (_next)
		{
			//currentPath.SetNextPoint();
			currentPath.SetNextPointDelay();
			movement.MoveTarget = CurrentPath.CurrentNavPosition;
		}

	 
	}

	public void SetNextTarget(Vector3 _pos)
	{
		movement.MoveTarget = _pos;
	}

	public void SetPreviousTarget(Vector3 _pos)
	{
		movement.MoveTarget = _pos;
	}

	void OnDrawGizmos()
	{
		if (!IsValidAgent && !IsValid) return;
		Color _validPath = IsValidAgent ? Color.green : Color.red;
		Color _valid = IsValid ? Color.green : Color.red;
		Gizmos.color = _valid;
		Gizmos.DrawCube(Position + Vector3.up, Vector3.one * .25f);
		Gizmos.color = _validPath;
		Gizmos.DrawCube(Position + Vector3.up * 1.5f, Vector3.one * .25f);
	}

	#endregion Personnal Methods
}
