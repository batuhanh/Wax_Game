using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaxMeshCreator : MonoBehaviour
{
    [SerializeField] private float shapeSpeed = 4f;
    private Mesh deformingMesh;
    private Vector3[] originalVertices;
    private Vector3[] originalVerticesPositions;
    private float maxDistanceForVertex = 200;
    private const float maxDistanceConstant = 200;
    [SerializeField] private List<Vector3> upperSideVertices = new List<Vector3>();

    private void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().material.renderQueue = 2998;

        originalVertices = deformingMesh.vertices;
        originalVerticesPositions = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            originalVerticesPositions[i] = originalVertices[i];
            originalVertices[i] = new Vector3(originalVertices[i].x, originalVertices[i].y, originalVertices[i].z + 3.2f);
        }
        deformingMesh.vertices = originalVertices;

        upperSideVertices = new List<Vector3>(new Vector3[originalVertices.Length]);
        for (int i = 0; i < upperSideVertices.Count; i++)
        {
            upperSideVertices[i] = new Vector3(0, 2, 5);

        }
        //MakeAllVerticesToNormalPos();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {
            if (maxDistanceForVertex < 350)
            {
                maxDistanceForVertex += Time.deltaTime * 30;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            maxDistanceForVertex = maxDistanceConstant;
            deformingMesh.RecalculateNormals();
            //DetectUpperSideOfMesh();
        }
    }
    public void DetectUpperSideOfMesh()
    {
        Mesh upperSideMesh = new Mesh();
        upperSideMesh.vertices = upperSideVertices.ToArray();
        upperSideMesh.triangles = deformingMesh.triangles;
        upperSideMesh.RecalculateBounds();
        upperSideMesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = upperSideMesh;
    }
    public void AddForceToNormal(Vector3 point, float force)
    {
        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 pointToVertex = originalVertices[i] - point;
            if (pointToVertex.sqrMagnitude < maxDistanceForVertex && originalVertices[i] != originalVerticesPositions[i])
            {
                float speedAccordingDistance = (50 / pointToVertex.sqrMagnitude);
                if (pointToVertex.sqrMagnitude < 150)
                {
                    Debug.Log("150den küçük");
                    originalVertices[i] = Vector3.Lerp(originalVertices[i], originalVerticesPositions[i],
                    (shapeSpeed * 3 * Time.deltaTime));
                }
                else
                {
                    Debug.Log("150den büyük");
                    originalVertices[i] = Vector3.Lerp(originalVertices[i], originalVerticesPositions[i],
                    speedAccordingDistance * (shapeSpeed * Time.deltaTime));
                }


                if (!upperSideVertices.Contains(originalVertices[i]))
                {
                    //upperSideVertices.Add(originalVertices[i]);
                    if (originalVertices[i] == originalVerticesPositions[i])
                    {
                        upperSideVertices[i] = originalVertices[i];
                    }

                    deformingMesh.RecalculateNormals();
                }
            }
        }
        deformingMesh.vertices = originalVertices;
    }

    private void MakeAllVerticesToNormalPos()
    {
        deformingMesh.vertices = originalVerticesPositions;
    }
}