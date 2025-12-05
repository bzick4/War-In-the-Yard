using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private float _Sensitivity = 2f;
    [SerializeField] private float _MaxYAngle = 80f;
    private float _rotationX = 0f;
    

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.parent.Rotate(Vector3.up * mouseX * _Sensitivity);
        
        _rotationX -= mouseY * _Sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -_MaxYAngle, _MaxYAngle);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
       
        
    }
}
