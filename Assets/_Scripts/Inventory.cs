using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public Inventory inventory;
    public GameObject arrow;
    private CanvasControl stop;

    #region For weapon
    public List<GameObject> bullets;
    public GameObject slingshot;
    public GameObject weapon1;
    public GameObject weapon2;

    public Transform inHandPosition;
    public Transform SlingShotInHandPosition;
    public GameObject inHandObject;

    public Rigidbody weapon1RB;
    public Rigidbody weapon2RB;
    #endregion


    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        stop = FindAnyObjectByType<CanvasControl>();
        Ñhecking();
    }

    public bool switcher = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && switcher)
        {
            inventory.gameObject.SetActive(switcher);
            arrow.SetActive(!switcher);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            switcher = !switcher;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && switcher == false)
        {
            inventory.gameObject.SetActive(switcher);
            arrow.SetActive(!switcher);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            switcher = !switcher;
        }

        if (stop.switcher && switcher)
        {
            InHand();
        }

        InventoryStore();
    }

    public void InHand()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) { Destroy(inHandObject); inHandObject = null; }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && inHandObject == null && weapon1 != null)
        {
            GettingRigitbody();
            weapon1RB.isKinematic = true;
            if (weapon2 != null) { weapon2RB.isKinematic = false; }
            SpawnCoordinates(weapon1);
        } 

        if (Input.GetKeyDown(KeyCode.Alpha2) && inHandObject == null && weapon2 != null)
        {
            GettingRigitbody();
            weapon2RB.isKinematic = true;
            if (weapon1 != null) { weapon1RB.isKinematic = false; }
            SpawnCoordinates(weapon2);
        } 

        if (Input.GetKeyDown(KeyCode.Alpha3) && (inHandObject == null || inHandObject.CompareTag("SlingShot")))
        {
            SpawnCoordinates(slingshot);
        }
    }

    public void GettingRigitbody()
    {
        if (weapon1 != null) { weapon1RB = weapon1.GetComponent<Rigidbody>(); }
        if (weapon2 != null) { weapon2RB = weapon2.GetComponent<Rigidbody>(); }
    }
    public void SpawnCoordinates(GameObject weapon)
    {
        Destroy(inHandObject);
        inHandObject = Instantiate(weapon, inHandPosition.position, Quaternion.identity, inHandPosition);

        WeaponRotation rotation = inHandObject.GetComponent<WeaponRotation>();
        inHandObject.transform.localRotation = Quaternion.Euler(rotation.rotationX, rotation.rotationY, rotation.rotationZ);

        WeaponPosition position = inHandObject.GetComponent<WeaponPosition>();
        inHandObject.transform.localPosition = new Vector3(position.positionX, position.positionY, position.positionZ);
    }
    public void DropItem()
    {
        var dropeditem = Instantiate(inHandObject, inHandPosition.position, Quaternion.identity);
        var rigitbodyOfUtem = dropeditem.GetComponent<Rigidbody>();
        rigitbodyOfUtem.isKinematic = false;

        inHandObject = null;
    }

    public void CleaningInventory(int place)
    {
        if (place == 1) { weapon1 = null; }
        if (place == 2) { weapon2 = null; }
    }


    #region Canvas objects
    public TextMeshProUGUI bulletsCount;
    public TextMeshProUGUI Slingshot;
    public TextMeshProUGUI Weapon1;
    public TextMeshProUGUI Weapon2;

    public void InventoryStore()
    {
        bulletsCount.text = $"Bullets: {bullets.Count}";

        if (slingshot != null) { Slingshot.text = "Slingshot: in stock"; } else { Slingshot.text = "Slingshot: none"; }

        if (weapon1 != null) { Weapon1.text = $"Weapon 1: {weapon1.name}"; } else { Slingshot.text = "Weapon 1: none"; }
        if (weapon2 != null) { Weapon1.text = $"Weapon 2: {weapon2.name}"; } else { Slingshot.text = "Weapon 2: none"; }
    }


    public void ChangeWeapons(GameObject changingObj)
    {

    }
    #endregion

    public List<GameObject> items;
    public void Ñhecking()
    {
        for (int i = 0; i < 10; i++)
        {
            bullets.Add(items[Random.Range(0, 3)]);
        }
    }
}
