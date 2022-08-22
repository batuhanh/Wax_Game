using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxRemover : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    private bool canDetect = false;
    private bool lastMove = false;
    private Vector3 mousePosition;
    private float horizontal = 0;
    [SerializeField] private float rotationValue;
    [SerializeField] Material[] bendingMats;
    [SerializeField] GameObject physcialWaxobject;
    [SerializeField] GameObject bendedWaxobject;
    [SerializeField] GameObject waxHairParent;
    private float lerpedBendValue;
    private float lerpedXValue;
    private void Start()
    {
        
        foreach (Material mat in bendingMats)
        {
            mat.SetFloat("_RotationRate", 0);
        }
    }
    private void Update()
    {
        if (canDetect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 2.5f;
            }
            else
            {
                horizontal = 0;
                CheckIsRemoved();
            }
            rotationValue = (horizontal * 100);
           
            foreach (Material mat in bendingMats)
            {
                mat.SetFloat("_RotationRate", Mathf.Clamp(rotationValue,-250,0));
            }
        }
        if (lastMove)
        {
            lerpedBendValue = Mathf.Lerp(lerpedBendValue, -270f, Time.deltaTime * 10f);
            lerpedXValue = Mathf.Lerp(lerpedXValue, -5f, Time.deltaTime * 1f);
            waxHairParent.transform.position = new Vector3(lerpedXValue, waxHairParent.transform.position.y, waxHairParent.transform.position.z);
            foreach (Material mat in bendingMats)
            {
                mat.SetFloat("_RotationRate", lerpedBendValue);
            }
        }
    }
    private void CheckIsRemoved()
    {
        if (rotationValue <= -120)
        {
            //game completed
            canDetect = false;
            lastMove = true;
            eventManager.CallLevelCompletedEvent();
            
        }
    }
    private void StartDetecting()
    {
        lerpedXValue = waxHairParent.transform.position.x;
        canDetect = true;
        physcialWaxobject.SetActive(false);
        bendedWaxobject.SetActive(true);
        foreach (Animator anim in waxHairParent.GetComponentsInChildren<Animator>())
        {
            Destroy(anim);
        }
    }
    void OnEnable()
    {
        EventManager.firstStageComplated += StartDetecting;

    }
    void OnDisable()
    {
        EventManager.firstStageComplated -= StartDetecting;
    }
}
