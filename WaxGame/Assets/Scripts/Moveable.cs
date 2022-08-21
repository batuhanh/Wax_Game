using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] protected Animator myAnim;
    private Vector3 mousePosition;

    private float horizontal;
    private float vertical;
    protected float moveSpeed = 65f;
    private float lerpedZVal;
    private float startZVal;
    private float targetZVal;

    private bool canMove = false;
    protected bool isMoving = false;
   
    private void Start()
    {
        targetZVal = transform.position.z;
        lerpedZVal = targetZVal;
        startZVal = targetZVal;
    }
    public virtual void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
                targetZVal = -0.044f;
                myAnim.SetBool("isMoving", true);
                isMoving = true;
            }
            else if (Input.GetMouseButton(0))
            {

                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 2.5f;
                vertical = (Input.mousePosition.y - mousePosition.y) / Screen.width * 1.5f;
                mousePosition = Input.mousePosition;
            }
            else
            {
                horizontal = 0;
                vertical = 0;
                targetZVal = startZVal;
                myAnim.SetBool("isMoving", false);
                myAnim.SetBool("isWaxing", false);
                isMoving = false;
            }
            lerpedZVal = Mathf.Lerp(lerpedZVal, targetZVal, Time.deltaTime * 10f);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + (moveSpeed * horizontal * Time.deltaTime), -0.18f, 0.20f),
                Mathf.Clamp(transform.position.y + (moveSpeed * vertical * Time.deltaTime), -2.3f, -1.6f),
                lerpedZVal
                );
        }

    }
    private void MakeMoveable()
    {
        canMove = true;
    }
    private void MakeNonMovable()
    {
        canMove = false;
    }
    void OnEnable()
    {
        EventManager.myLevelStarted += MakeMoveable;
        EventManager.myLevelFailed += MakeNonMovable;
        EventManager.myLevelCompleted += MakeNonMovable;
    }
    void OnDisable()
    {
        EventManager.myLevelStarted -= MakeMoveable;
        EventManager.myLevelFailed -= MakeNonMovable;
        EventManager.myLevelCompleted -= MakeNonMovable;
    }
}
