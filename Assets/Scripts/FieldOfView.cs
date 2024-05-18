using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;        
        fov = 45f;
    }
    private void LateUpdate() {        
        
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++) {
            float angleRad = angle * (Mathf.PI / 180f);
            Vector3 vertex;
            Vector3 utilsGetVectorfromAngle = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


            RaycastHit2D raycast = Physics2D.Raycast(origin, utilsGetVectorfromAngle, viewDistance, layerMask);            
            if(raycast.collider == null) {
                //no hit
                vertex = origin + utilsGetVectorfromAngle * viewDistance;
            } else {
                vertex = raycast.point;
            }
            vertices[vertexIndex] = vertex;



            if(i > 0) {
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
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }
    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 aimDirection) {
        aimDirection = aimDirection.normalized;
        float n = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (n < 0) {
            n += 360;
        }
        startingAngle = n + fov / 2f + 180f;
    }
}
