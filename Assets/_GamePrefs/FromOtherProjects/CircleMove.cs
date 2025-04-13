using UnityEngine;

public class CircleMove : MonoBehaviour
{
    private Rigidbody _rb;
    public float force = 0.5f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { _rb.useGravity = !_rb.useGravity; }
        Vector3 vectorForse = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { vectorForse += Vector3.forward; }
        if (Input.GetKey(KeyCode.S)) { vectorForse += Vector3.back; }
        if (Input.GetKey(KeyCode.D)) { vectorForse += Vector3.right; }
        if (Input.GetKey(KeyCode.A)) { vectorForse += Vector3.left; }
        if (Input.GetKey(KeyCode.Space)) { vectorForse += Vector3.up; }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Q)) { vectorForse += Vector3.down; }

        _rb.AddForce(vectorForse.normalized * force, ForceMode.Acceleration); 
    }
}
