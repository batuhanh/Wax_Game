using UnityEngine;

public class WaxStick : Moveable
{

    [SerializeField] private float force = -25f;
    [SerializeField] private float forceOffset = 0.1f;
    [SerializeField] private LayerMask layerMask;
    public override void Update()
    {
        base.Update();
        if (isMoving)
        {
            RayToArm();
        }
    }
    public void RayToArm()
    {
        RaycastHit hit;

        Vector3 startPosOfRay = transform.position + new Vector3(0, -0.1f, 0);
        Vector3 directionOfRay = (transform.position + new Vector3(0, -0.05f, 10f)) - (transform.position + new Vector3(0, -0.05f, 0f));
        if (Physics.Raycast(startPosOfRay, directionOfRay, out hit, 3f, layerMask))
        {
            Debug.Log("Hitted");

            WaxMeshCreator waxMeshCreator = hit.collider.GetComponent<WaxMeshCreator>();
            if (waxMeshCreator)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                waxMeshCreator.AddForceToNormal(point, force);
                if (!myAnim.GetBool("isWaxing"))
                {
                    myAnim.SetBool("isWaxing", true);

                }
            }

        }
        else
        {
            if (myAnim.GetBool("isWaxing"))
            {
                myAnim.SetBool("isWaxing", false);

            }
        }
    }
    private void MakeItDisable()
    {
        myAnim.SetTrigger("Disable");
    }
    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.firstStageComplated += MakeItDisable;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.firstStageComplated -= MakeItDisable;
    }

}