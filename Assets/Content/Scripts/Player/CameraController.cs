using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Pointer;
    public float LookSpeed = 3;
    public float FovSpeed = 5;

    private float DeltaMagn;
    //SceneController scene;
    float _x, _y;
    Vector3 lastPos;
    Vector3 delta;
    private Camera cam;
    public Camera PointerCamera;
    private PlayerController player;

    private void Awake()
    {
        //scene = FindObjectOfType<SceneController>();
        player = FindObjectOfType<PlayerController>();
        cam = GetComponent<Camera>();

        PointerCamera.fieldOfView = cam.fieldOfView;
    }

    void Update()
    {
        CheckMouseDelta();

        if (Input.GetMouseButton(0))
        {
            _x += -Input.GetAxis("Mouse X") * LookSpeed;
            _y += Input.GetAxis("Mouse Y") * LookSpeed;
        }
        var targetRotation = Quaternion.Euler(_y, _x, 0);
        _y = Mathf.Clamp(_y, -89, 89);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 5);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Pointer.position = hit.point;
            Pointer.rotation = Quaternion.Lerp(Pointer.rotation, Quaternion.FromToRotation(-Vector3.forward, hit.normal), Time.deltaTime * 30);

            if (Input.GetMouseButtonUp(0) && delta.magnitude < 0.7f)
                player.DestPoint = hit.point;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            cam.fieldOfView -= FovSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30, 100);

            PointerCamera.fieldOfView = cam.fieldOfView;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            cam.fieldOfView += FovSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30, 100);

            PointerCamera.fieldOfView = cam.fieldOfView;
        }
    }

    void CheckMouseDelta()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            delta = Input.mousePosition - lastPos;
        }

        DeltaMagn = delta.magnitude;
    }
}
