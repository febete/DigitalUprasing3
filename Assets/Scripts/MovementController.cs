using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float lookSensitivity = 3f;
    
    [SerializeField]
    GameObject fpsCamera;

    [SerializeField]
    float jumpForce = 10f;
    

    private bool isGrounded;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float  CameraUpandDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;


    private Rigidbody rb;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update()
    {
        //Calculate movement veloc, ty as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input. GetAxis( "Vertical") ;

        Vector3 _movementHorizontal = transform.right * _xMovement;  // (1,0,0)
        Vector3 _movementVertical = transform.forward * _zMovement;  // (0,0,1)

        //Final movement velocity
        Vector3 _movementVelocity  = ( _movementHorizontal + _movementVertical).normalized *speed;

        //Apply movement
        Move(_movementVelocity);    




        //calculate rotation as a 3D vector for turning around. 
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0,_yRotation,0) * lookSensitivity;

        //Apply rotation
        Rotate(_rotationVector);




        //Calculate  look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

        //Apply rotation
        RotateCamera(_cameraUpDownRotation);
    }

 




    //runs per physics iteration
    void FixedUpdate()
    {
        PerformMovement();

        PerformRotation();
        
        CheckGrounded(); //yerde olup olmadığını kontrol eder

        Jump(); //zıplama kontrolü

        if (fpsCamera != null)
          {
            CurrentCameraUpAndDownRotation -= CameraUpandDownRotation;
            CurrentCameraUpAndDownRotation  = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85,85);
            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,0,0);

          }  



    }






    private void Move(Vector3 movementVelocity)
    {
         velocity = movementVelocity;   
    }




      private void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }




       private void RotateCamera(float cameraUpDownRotation)
    {
        CameraUpandDownRotation = cameraUpDownRotation;
    }



    //Perform movement based on velocity variable
    void PerformMovement()
    {
         if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

    }




    //Perform Rotation
    void PerformRotation()
    {
         rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }


    //yerde olup olmadığını kontrol etmek için
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }



    //Zıplama fonksiyonu
    private void Jump()
    {
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
