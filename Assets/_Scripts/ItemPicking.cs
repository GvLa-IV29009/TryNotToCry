using UnityEngine;

public class ItemPiking : MonoBehaviour
{
    public GameObject pickedObject;
    public GameObject prefab;
    private Inventory inventory;
    public Prefabs ModelPrefab;
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        ModelPrefab = FindAnyObjectByType<Prefabs>();
    }
    void Update()
    {
        Picking();
    }

    public void Picking()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5))
            {
                pickedObject = hit.transform.gameObject;

                string teg = pickedObject.transform.tag;
                switch (teg)
                {
                    case "Bullet":
                        if (inventory.bullets.Count < 10)
                        {
                            GetPrefab(pickedObject.gameObject);
                            inventory.bullets.Add(prefab);
                            Destroy(pickedObject);
                        } else { Debug.Log("Full inventory"); }
                        break;

                    case "Weapon":
                        Debug.Log("EEE");
                        GetPrefab(pickedObject.gameObject);
                        Debug.Log("EEE");
                        ObjectPrefab objectPrefabW = prefab.GetComponent<ObjectPrefab>();
                        if (inventory.weapon1 == null)
                        {
                            objectPrefabW.GetPlaceInInventory(1);
                            inventory.weapon1 = prefab;
                            Destroy(pickedObject);
                        }
                        else if (inventory.weapon2 == null)
                        {
                            objectPrefabW.GetPlaceInInventory(2);
                            inventory.weapon2 = prefab;
                            Destroy(pickedObject);
                        }
                        else { inventory.ChangeWeapons(pickedObject); }
                        break;

                    case "Throwable":
                        Debug.Log("Bruh");
                        GetPrefab(pickedObject.gameObject);
                        ObjectPrefab objectPrefabT = prefab.GetComponent<ObjectPrefab>();
                        if (inventory.weapon1 == null) 
                        {
                            objectPrefabT.GetPlaceInInventory(1);
                            inventory.weapon1 = prefab;
                            Destroy(pickedObject);
                        }
                        else if (inventory.weapon2 == null) 
                        {
                            objectPrefabT.GetPlaceInInventory(2);
                            inventory.weapon2 = prefab;
                            Destroy(pickedObject);
                        }
                        else 
                        {
                            if (inventory.inHandObject == null)
                            {
                                inventory.inHandObject = prefab;
                                Rigidbody inHandRB = inventory.inHandObject.GetComponent<Rigidbody>();
                                inHandRB.isKinematic = true;
                                Debug.Log("1");
                                inventory.SpawnCoordinates(inventory.inHandObject);
                                Debug.Log("2");
                                Destroy(pickedObject);
                            }
                            
                        }
                        Debug.Log("hurB");
                        break;
                }
            }
        }
    }

    public void GetPrefab(GameObject thisObject)
    {
        ObjectPrefab objectPrefab = thisObject.GetComponent<ObjectPrefab>();
        ModelPrefab.GetPrefab(objectPrefab.prefab);
        prefab = ModelPrefab.prefab;
        Debug.Log("Yes" + prefab.name);
    }
}
