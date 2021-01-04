using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PY_Path : MonoBehaviour
{
	public event Action OnPointReached = null;

	#region Serialize Field
    [SerializeField] Color pathColor = Color.white;
    [SerializeField] Transform[] pathPoints = null;
    [SerializeField, Range(.5f, 10f)] float minDistance = .1f;
    [SerializeField] bool isRandomTime = false;
	#endregion Serialize Field

	#region F/P

	IAgent currentAgent = null;
    int currentIndex = 0;
    float timerDelay = 0, maxDelay = 5;
    public Vector3 CurrentNavPosition
    {
        get
        {
            if (!IsValid || currentIndex > pathPoints.Length - 1 || !pathPoints[currentIndex]) return Vector3.zero;
            return pathPoints[currentIndex].position;
        }
    }
    public bool IsValid => pathPoints != null && pathPoints.Length > 0;

	#endregion F/P

	#region Unity Methods
	public void OnDisable()
	{
        OnPointReached();


    }

    private void Awake() => OnPointReached += SetPoint;

	#endregion Unity Methods

	#region Personnal Methods
	void SetPoint()
	{
        if (currentAgent.IsRewind)
            SetPreviousPoint();
        else SetNextPoint();
	}

	private void Update()
	{
        RandomDelayTimer();

    }
	public void SetAgent(IAgent _agent) => currentAgent = _agent;
    public bool IsAtPos(IAgent _agent) => Vector3.Distance(CurrentNavPosition, _agent.Position) < minDistance;
    public bool IsAtPos(IAgent _agent, float _minDistance) => Vector3.Distance(CurrentNavPosition, _agent.Position) < _minDistance;
    public void SetNextPointDelay()
	{
        if (isRandomTime) return;
        maxDelay = Random.Range(0, 5);
        Debug.Log(maxDelay);
        isRandomTime = true;
	}

 
    public void SetNextPoint()
    {
        //TODO timer waitforseconds
        currentIndex++;
        currentIndex = currentIndex > pathPoints.Length - 1 ? pathPoints.Length - 1 : currentIndex;


    }

    void RandomDelayTimer()
	{
        if (!isRandomTime) return;
        Debug.Log("Start Timer");

        timerDelay += Time.deltaTime;
        if(timerDelay > maxDelay)
		{
            Debug.Log($"End Timer {timerDelay} {maxDelay}");
            OnPointReached?.Invoke();
            timerDelay = 0;
            SetNextPoint();
            currentAgent.SetNextTarget(CurrentNavPosition);
            isRandomTime = false;
           

        }
	}
    public void SetPreviousPoint()
    {
    
        currentIndex--;
        currentIndex = currentIndex < 0 ? pathPoints.Length +1 : currentIndex;
    }

	//

	#region Gizmos Debug

	private void OnDrawGizmos()
    {
        if (!IsValid) return;
        DrawPath(pathColor);
        DrawCurrentNavPoint(Color.green);
        DrawCurrentAgent(pathColor);
    }

    void DrawPath(Color _color)
    {
        Gizmos.color = _color;
        int _count = pathPoints.Length;
        for (int i = 0; i < _count; i++)
        {
            Vector3 _fromPosition = pathPoints[i].position;
            if (i < _count - 1)
            {
                Vector3 _nextPosition = pathPoints[i + 1].position;
                Gizmos.DrawLine(_fromPosition, _nextPosition);
            }
            Gizmos.DrawSphere(_fromPosition + Vector3.up, .1f);
            Gizmos.DrawLine(_fromPosition, _fromPosition + Vector3.up);
        }
     
    }
    void DrawCurrentNavPoint(Color _color)
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(CurrentNavPosition + Vector3.up * 1.1f, Vector3.one * .25f);
        Gizmos.DrawLine(CurrentNavPosition + Vector3.up, CurrentNavPosition + Vector3.up);
    }

    void DrawCurrentAgent(Color _color)
	{
        if (currentAgent != null)
		{
            Gizmos.color = _color;//- new Color(0,0,0, .8f) for the opacity
            Gizmos.DrawWireSphere(currentAgent.Position, 1);
            Gizmos.DrawLine(currentAgent.Position, CurrentNavPosition);
		}

        Gizmos.color = Color.white;
    }

	#endregion Gizmos Debug

	#endregion Personnal Methods
}
