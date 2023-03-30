using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 20f;
    public float minY = 10f;
    public float maxY = 80f;
    public float maxDistanceFromInitialPosition = 30f; // Adjust as needed

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
        
        float distanceFromInitialPosition = Vector3.Distance(transform.position, initialPosition);
        if (distanceFromInitialPosition > maxDistanceFromInitialPosition)
        {
            transform.position = initialPosition + (transform.position - initialPosition).normalized * maxDistanceFromInitialPosition;
        }
        
#endif

#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(-touchDeltaPosition.x * panSpeed * Time.deltaTime, 0, -touchDeltaPosition.y * panSpeed * Time.deltaTime, Space.World);
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Vector3 pos = transform.position;
                pos.y += deltaMagnitudeDiff * scrollSpeed * Time.deltaTime;
                pos.y = Mathf.Clamp(pos.y, minY, maxY);
                transform.position = pos;
            }
#endif
    }
}