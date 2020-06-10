using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _tilt;

    private void Update()
    {
        if (Pause.Active)
            return;

        float mouseRotation = Input.GetAxis("Mouse Y");
        _tilt = Mathf.Clamp(_tilt - mouseRotation, -15f, 15f);
        transform.localRotation = Quaternion.Euler(_tilt, 0, 0);
    }
}
