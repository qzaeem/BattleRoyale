using UnityEngine;

public class AlignWithCam : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = GetComponent<Canvas>().worldCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cameraTransform.forward;
    }
}
