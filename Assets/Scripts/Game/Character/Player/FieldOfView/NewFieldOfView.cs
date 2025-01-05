using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    public float viewRadius = 5f;  // How far the FOV extends
    public float viewAngle = 60f;  // Angle of the FOV (half of it on each side)
    public int resolution = 50;    // How many rays to cast, the higher the smoother the fan

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Transform player;

    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        player = transform.parent;  // Assuming this script is on the child 'Field of View'
    }

    void LateUpdate()
    {
        UpdateFieldOfView();
    }

    void UpdateFieldOfView()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Keep it in 2D plane

        // Calculate direction from player to mouse
        Vector3 aimDirection = (mousePosition - player.position).normalized;

        // Set the origin of the mesh at the player's position
        Vector3 origin = Vector3.zero;

        // Generate the vertices for the fan shape
        int stepCount = Mathf.RoundToInt(viewAngle * resolution / 360);
        float angleStep = viewAngle / stepCount;

        Vector3[] vertices = new Vector3[stepCount + 2];  // +2 for origin and last point
        int[] triangles = new int[stepCount * 3];         // 3 indices per triangle

        vertices[0] = origin;  // First vertex at the origin

        for (int i = 0; i <= stepCount; i++)
        {
            float currentAngle = -viewAngle / 2 + i * angleStep;  // From -half to +half the viewAngle
            Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * aimDirection;  // Rotate aimDirection by current angle
            vertices[i + 1] = direction * viewRadius;  // Calculate vertex on the edge of the FOV

            if (i < stepCount)  // Create triangles for the mesh
            {
                triangles[i * 3] = 0;                // Center of fan
                triangles[i * 3 + 1] = i + 1;        // Current vertex
                triangles[i * 3 + 2] = i + 2;        // Next vertex
            }
        }

        // Assign vertices and triangles to the mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

