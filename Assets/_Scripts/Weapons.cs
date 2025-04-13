using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapons : MonoBehaviour
{
    private Animator animator;
    public Transform bulletSpawn;
    public int bulletSpeed = 5;

    public Vector2 firstMousePosition;
    public Vector2 lastMousePosition;

    private Inventory inventory;
    private CanvasControl stop;
    private CharacterMove movement;

    void Start()
    {
        animator = GetComponent<Animator>();

        inventory = FindAnyObjectByType<Inventory>();
        stop = FindAnyObjectByType<CanvasControl>();
        movement = FindAnyObjectByType<CharacterMove>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (inventory.inHandObject != null && inventory.switcher && stop.switcher )
        {
            Hitting();
            Throwing();
            if (inventory.inHandObject.CompareTag("SlingShot")) { Shooting(); }
        }
    }
    #region Attack
    public void Hitting()
    {
        if (Input.GetMouseButtonDown(1) && inventory.inHandObject.CompareTag("Weapon")) { animator.SetTrigger(name: "Hit"); }
    }
    public void Throwing()
    {
        if (Input.GetMouseButtonDown(1) && inventory.inHandObject.CompareTag("Throwable") || Input.GetMouseButtonDown(0) && inventory.inHandObject.CompareTag("Weapon")) 
        {
            animator.SetTrigger("Hit");
            StartCoroutine(ToThrowTime());
        }
    }
    private IEnumerator ToThrowTime()
    {
        yield return new WaitForSeconds(0.75f);

        Destroy(inventory.inHandObject);
        GameObject thrownObject = Instantiate(inventory.inHandObject, inventory.inHandPosition.position, Quaternion.identity);

        var thrownObjRigidBody = thrownObject.GetComponent<Rigidbody>();
        thrownObjRigidBody.isKinematic = false;
        Vector3 vectorForse = thrownObject.transform.TransformDirection(transform.forward);

        thrownObjRigidBody.AddForce(vectorForse * 10, ForceMode.Impulse);

        ObjectPrefab objectPrefab = inventory.inHandObject.GetComponent<ObjectPrefab>();

        inventory.CleaningInventory(objectPrefab.PlaceInInventory);
        inventory.inHandObject = null;
    }
    public void Shooting()
    {
        Debug.Log("It works");
        if (Input.GetMouseButtonDown(0) && inventory.bullets.Count != 0)
        {
            movement.canRotate = false;
            firstMousePosition = Input.mousePosition;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0) && inventory.bullets.Count == 0)
        {
            Debug.Log("No bullets!");
        }

        if (Input.GetMouseButtonUp(0) && inventory.bullets.Count != 0)
        {
            lastMousePosition = Input.mousePosition;


            Vector3 mouseDirection = firstMousePosition - lastMousePosition;

            Vector3 localShootDirection = new Vector3(0, 0, mouseDirection.y).normalized;

            Vector3 shootDirection = transform.TransformDirection(localShootDirection);

            var bulletInstance = Instantiate(inventory.bullets[0], bulletSpawn.position, Quaternion.identity);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.AddForce(shootDirection * bulletSpeed, ForceMode.Impulse);

            inventory.bullets.Remove(inventory.bullets[0]);

            firstMousePosition = Vector2.zero;
            lastMousePosition = Vector2.zero;


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            movement.canRotate = true;
        }
        
    }
    #endregion
}
