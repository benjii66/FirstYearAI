using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAY_Player : FAY_Singleton<FAY_Player>
{
    #region F/P
    [SerializeField] float movementSpeed = 1;
    [SerializeField] int health = 100;
    [SerializeField] Animator playerAnim = null;
 


    public float MovementSpeed => movementSpeed;
    public int Health => health;

    public bool IsDead;
    public bool CanMove = true;
    #endregion F/P

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    #region Methods
    void Move()
    {
        if (!CanMove) return;
        GetInput();
    }


    void GetInput()
    {
        if (Input.GetKey("z"))
            MoveUp();
        if (Input.GetKey("s"))
            MoveDown();
        if (Input.GetKey("q"))
            MoveLeft();
        if (Input.GetKey("d"))
            MoveRight();
    }

    #region Movement
    void MoveUp()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        PlayWalk();
    }


    void PlayWalk() => playerAnim.Play("HappyWalk");

    void MoveDown()
    {
        transform.position -= transform.forward * movementSpeed * Time.deltaTime;
        playerAnim.Play("HappyWalkBackward");
    }

    void MoveLeft()
    {
        transform.position -= transform.right * movementSpeed * Time.deltaTime;
        transform.eulerAngles -= new Vector3(0, 90, 0) * movementSpeed * Time.deltaTime;
        PlayWalk();
    }

    void MoveRight()
    {
        transform.position += transform.right * movementSpeed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 90, 0) * movementSpeed * Time.deltaTime;
        PlayWalk();
    }

    #endregion

    #endregion Methods

}