using UnityEngine;

public class LineRendererCollision : MonoBehaviour
{
    public static LineRenderer lineRenderer { get; set; }

    void Update()
    {
        // Example: Check for collision when the player presses a button (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckCollisionWithLineRenderer();
        }
    }

    void CheckCollisionWithLineRenderer()
    {
        // Example: Raycast down from the player's position
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit hitInfo;

        // Cast the ray and check for intersection with the LineRenderer
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Check if the hit object has a LineRenderer component
            LineRenderer hitLineRenderer = hitInfo.collider.GetComponent<LineRenderer>();

            if (hitLineRenderer != null && hitLineRenderer == lineRenderer)
            {
                // The player is on the LineRenderer
                Debug.Log("Player stepped on the LineRenderer!");
            }
        }
    }
}
