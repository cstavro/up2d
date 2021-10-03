using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController2D : MonoBehaviour {
    [SerializeField] private GameObject followTarget;
    [SerializeField] private float upperBound;
    [SerializeField] private float lowerBound;

    private Camera camera;
    public Vector3 targetCameraLocation;

    
    // Start is called before the first frame update
    void Start() {
        this.camera = gameObject.GetComponent<Camera>();
        this.targetCameraLocation = camera.transform.position;
    }

    // Update is called once per frame
    void Update() {
        var targetScreenPosition = camera.WorldToScreenPoint(followTarget.transform.position);
        var y = targetScreenPosition.y / Screen.height;

        if (y > upperBound || y < lowerBound) {
            targetCameraLocation = camera.transform.position;
            targetCameraLocation.y = followTarget.transform.position.y;
        }

        if (followTarget.activeInHierarchy) {
            updateCamera(targetCameraLocation);
        }
    }

    void updateCamera(Vector3 targetPosition) {
        var velocity = Vector3.zero;
        var smoothTime = 0.3f;
        
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, 
            targetPosition, ref velocity, smoothTime);
    }
}
