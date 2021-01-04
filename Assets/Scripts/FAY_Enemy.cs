using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAY_Enemy : MonoBehaviour
{
    #region F/P
    [SerializeField, Header("Speed movement of the enemy")] float movementSpeed = 1;
    [SerializeField, Header("Speed rotation of the enemy")] float rotationSpeed = 1;
    [SerializeField, Header("Distance from the player to the Enemy"), Range(0,10)] float minDistance = 1;
    [SerializeField, Header("Visibility of the Enemy"), Range(0,10)] float visionRange = 1;
    [SerializeField, Header("Value of the damage")] int damage = 100;
    [SerializeField, Header("Check if the Enemy hit the player")] bool isHit = false;
    [SerializeField, Header("Trigger zone")] Collider enemyZone = null;
    [SerializeField, Header("The player")] FAY_Player player = null;
    [SerializeField] Animator enemyAnim = null;


    public Transform PlayerTransform => player.transform;
    public Vector3 targetPos => player.transform.position;
    public Quaternion targetRota => player.transform.rotation;
    public float MovementSpeed => movementSpeed;
    public float RotationSpeed => rotationSpeed;
    public int Damage => damage;

    public bool IsMovingGuard;
    public bool IsMovingOnPlayer;
    public bool IsReachingPlayer;
    public bool IsHit => isHit;

    public Collider EnemyZone => enemyZone;

    #endregion F/P


    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }


    //private void OnTriggerEnter(Collider _other)
    //{
    //    if (_other.tag == "Bryce")
    //    {
    //        Debug.Log("TU L'AS");
    //        GameObject _player = GameObject.FindGameObjectWithTag("Bryce");

    //        transform.position = _player.transform.position * Time.deltaTime * movementSpeed;

    //        minDistance = 0;

    //        IsReachingPlayer = true;
    //        MoveToPlayer();
    //    }
    //}


    void TestReach()
    {
       //target pos dans la visibility on chasse
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HitPlayer();
    }

    private void OnDrawGizmos()
    {        
        DrawEnemyToPlayer();
        DrawMinDistance();
        DrawVisionRange();
    }


    void DrawVisionRange()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    void DrawMinDistance()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }

    void DrawEnemyToPlayer()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, targetPos);
    }

    #endregion Unity Methods

    #region Methods

    void HitPlayer()
    {
        enemyAnim.Play("Attack");
        player.CanMove = false;
        if (player.Health <= 0)
            IsReachingPlayer = false;
        MoveToNextPoint();
        //play hit animation and decrease player health
    }

    void MoveToPlayer()
    {
        float _distance = Vector3.Distance(targetPos, transform.position);
        if (IsReachingPlayer == true)
        {
            //look at target
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationSpeed * Time.deltaTime);

            //move towards target
            if (_distance > minDistance)
            {
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
                enemyAnim.Play("run");
            }

            if (_distance <= minDistance)
            {
                HitPlayer();               
            }
               
        }


    }

    void MoveToNextPoint()
    {

    }
    #endregion Methods

}