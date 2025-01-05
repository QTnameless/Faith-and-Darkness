using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.InputSystem;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    //[SerializeField] private string sortingLayerName = "Default";


    private Mesh mesh;
    private float fov;
    private Vector3 origin;
    private float startingAngle;

 

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        /*var meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // Apply values from the Inspector
            meshRenderer.sortingLayerName = sortingLayerName;
         
        }*/

        fov = 90f;

        startingAngle = 135f;
        origin = Vector3.zero;

        
    }

    private void Update()
    {
        int rayCount = 50;

        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure it's 2D (remove Z-axis changes)
        Vector3 aimDirecion = mousePosition.normalized;
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov/2f;
        */



        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; // +2 for the center and last vertex
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

    
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

     
}