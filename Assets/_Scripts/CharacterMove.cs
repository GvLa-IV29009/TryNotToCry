using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class CharacterMove : MonoBehaviour
{
    public GameObject characterCamera;
    public Transform cameraCoordinatesUp;
    public Transform cameraCoordinatesDown;

    private Rigidbody _rb;

    public float jumpForce = 100f;
    public float walkForce = 5f;

    public bool canRotate = true;

    private Inventory inventory;
    private CanvasControl stop;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        inventory = FindAnyObjectByType<Inventory>();
        stop = FindAnyObjectByType<CanvasControl>();

        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {   
        if (inventory.switcher && stop.switcher)
        {
            Walk_Run();
            RotateLeftRight();
            RotateUpDown();
            Jumping();
            Crouch();
        }
    }

    #region ôóíêö³¿
    public void Walk_Run()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { walkForce = 3f; }

        Vector3 vectorForse = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { vectorForse += transform.forward; }
        if (Input.GetKey(KeyCode.S)) { vectorForse += -transform.forward; }
        if (Input.GetKey(KeyCode.D)) { vectorForse += transform.right; }
        if (Input.GetKey(KeyCode.A)) { vectorForse += -transform.right; }

        _rb.AddForce(vectorForse.normalized * walkForce, ForceMode.Acceleration);
    }
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            animator.SetBool("Crouch", true); 
            animator.SetTrigger("Crouch 0");
            characterCamera.transform.position = cameraCoordinatesDown.position;
        }
        if (Input.GetKeyUp(KeyCode.Z)) 
        {
            animator.SetBool("Crouch", false);
            characterCamera.transform.position = cameraCoordinatesUp.position;
        }
    }
    public void RotateLeftRight()
    {
        var arrowPositionX = Input.GetAxis("Mouse X");
        transform.Rotate(0, arrowPositionX * 10, 0);
    }
    public void RotateUpDown()
    {
        if (canRotate)
        {
            var arrowPositionY = Input.GetAxis("Mouse Y");
            characterCamera.transform.Rotate(arrowPositionY * -1, 0, 0);
        }
    }
    
    public void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { _rb.AddForce(new Vector3(_rb.linearVelocity.x, jumpForce), ForceMode.Impulse); }
    }
    #endregion
}
