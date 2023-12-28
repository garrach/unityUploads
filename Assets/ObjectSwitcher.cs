using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectSwitcher : MonoBehaviour
{
    public GameObject camFPS;
    public GameObject camTPS;
    private SkinnedMeshRenderer PlayerMesh;

    public float zoomSpeed = 5f;
    private void FixedUpdate()
    {
        PlayerMesh = FindObjectOfType<SkinnedMeshRenderer>();
    }
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollWheel) > 0.01f)
        {
            float newFieldOfView = Camera.main.fieldOfView - scrollWheel * zoomSpeed;
            newFieldOfView = Mathf.Clamp(newFieldOfView, 1f, 179f);

            Camera.main.fieldOfView = newFieldOfView;
        }
    }
    public void changeView()
    {
       
            camFPS.SetActive(!camFPS.activeSelf);
            camTPS.SetActive(!camTPS.activeSelf);

            camFPS.transform.position = PlayerMesh.transform.position+Vector3.up;
            PlayerMesh.enabled = !PlayerMesh.enabled;
        

    }
    public void retourne()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
