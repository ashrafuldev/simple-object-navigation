using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;

    public Vector3 cubeSize = new Vector3(1f, 1f, 1f);
    public Vector3 sphereSize = new Vector3(1f, 1f, 1f);
    public Material editModeMaterial;
    public Material defaultMaterial;

    private GameObject selectedObject;
    private bool isEditMode;
    private bool isRotating;

    private bool createCube;
    private bool createSphere;


    private void Awake()
    {
        SaveData.objects.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create cube or sphere on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Floor")
            {
                if (cubePrefab != null && createCube == true && isEditMode == false)
                {
                    GameObject cube = Instantiate(cubePrefab, hit.point, Quaternion.identity);
                     cube.transform.localScale = cubeSize;
                }

                if (spherePrefab != null && createSphere == true && isEditMode == false)
                {
                   GameObject sphere = Instantiate(spherePrefab, hit.point, Quaternion.identity);
                    sphere.transform.localScale = sphereSize;
                }
            }
        }

        // Toggle edit mode and rotate mode
        if (Input.GetKeyDown(KeyCode.R) && selectedObject != null)
        {
            if (isRotating == false)
            {
                isRotating = true;
                isEditMode = true;
                selectedObject.GetComponent<MeshRenderer>().material = editModeMaterial;
            }
            else
            {
                isRotating = false;
                isEditMode = false;
                selectedObject.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }

        // object selection and movement

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag != "Floor")
            {
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                }

                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<MeshRenderer>().material = editModeMaterial;
                isEditMode = true;
            }
            else
            {
                isEditMode = false;
            }
        }

        // Handle object movement and rotation

        if (selectedObject != null && isEditMode == true)
        {
            if (isRotating == false)
            {
                // Translate object
                /*float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");*/

                float horizontalInput = Input.GetAxis("Mouse X");
                float verticalInput = Input.GetAxis("Mouse Y");

                selectedObject.transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * 100f);
            }
            else
            {
                // Rotate object
                float horizontalRotation = Input.GetAxis("Mouse X");
                float verticalRotation = Input.GetAxis("Mouse Y");

                selectedObject.transform.Rotate(Vector3.up, horizontalRotation * Time.deltaTime * 500f, Space.World);
                selectedObject.transform.Rotate(Vector3.right, -verticalRotation * Time.deltaTime * 500f, Space.World);
            }

            
        }

        // Delete selected object
        if (Input.GetKeyDown(KeyCode.Delete) && selectedObject != null)
        {
            Destroy(selectedObject);
            selectedObject = null;
            isEditMode = false;
            isRotating = false;
        }
    }

    public void OnCubeButtonClicked()
    {
        createCube = true;
        createSphere = false;
    }

    public void OnSphereButtonClicked()
    {
        createCube = false;
        createSphere = true;
    }
}
