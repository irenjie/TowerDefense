using UnityEngine;

public class CameraManager : MonoBehaviour {
    public float moveSpeed = 1;

    private Camera _cam;
    private bool isDragging = false;

    void Start() {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //ÓÒ¼ü°´ÏÂ×´Ì¬
        if (Input.GetMouseButtonDown(1)) {
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            isDragging = false;
        }

        if (isDragging) {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 dir = mouseX * -_cam.transform.right + mouseY * -_cam.transform.forward;
            dir.y = 0;
            _cam.transform.position += dir * moveSpeed;
        }
    }
}
