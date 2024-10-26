using UnityEngine;

public class CameraManager : MonoBehaviour {
    public float mouseXSpeed = 0;
    public float mouseYSpeed = 0;

    public float _xMin;
    public float _xMax;
    public float _zMin;
    public float _zMax;


    private Transform[] _sizeRegions;
    private Camera _cam;


    private float camPostion_x = 0;
    private float camPostion_y = 0;


    void Start() {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //ÓÒ¼ü°´ÏÂ×´Ì¬
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0)) {
            camPostion_x -= Input.GetAxis("Mouse X");
            camPostion_y -= Input.GetAxis("Mouse Y");
            _cam.transform.localPosition = new Vector3(camPostion_x, _cam.transform.localPosition.y, camPostion_y);
            //print("Update               "+ _cam.transform.localPosition);
        } else {
            if (Input.mousePosition.x >= Screen.width * 0.99) {
                _cam.transform.Translate(Vector3.right * mouseXSpeed * Time.deltaTime);
            }

            if (Input.mousePosition.x <= Screen.width * 0.01) {
                _cam.transform.Translate(Vector3.right * -mouseXSpeed * Time.deltaTime);
            }

            if (Input.mousePosition.y >= Screen.height * 0.99) {
                _cam.transform.Translate(Vector3.forward * mouseYSpeed * Time.deltaTime, Space.World);
            }

            if (Input.mousePosition.y <= Screen.height * 0.01) {
                _cam.transform.Translate(Vector3.forward * -mouseYSpeed * Time.deltaTime, Space.World);
            }
        }

        var position = _cam.transform.position;
        position = new Vector3(Mathf.Clamp(position.x, _xMin, _xMax), _cam.transform.position.y,
            Mathf.Clamp(position.z, _zMin, _zMax));
        _cam.transform.position = position;
        camPostion_x = _cam.transform.localPosition.x;
        camPostion_y = _cam.transform.localPosition.z;
    }
}
