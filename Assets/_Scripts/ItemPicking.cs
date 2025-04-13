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
                        ObjectPrefab objectPrefab = prefab.GetComponent<ObjectPrefab>();
                        if (inventory.weapon1 == null)
                        {
                            objectPrefab.GetPlaceInInventory(1);
                            inventory.weapon1 = prefab;
                            Destroy(pickedObject);
                        }
                        else if (inventory.weapon2 == null)
                        {
                            objectPrefab.GetPlaceInInventory(2);
                            inventory.weapon2 = prefab;
                            Destroy(pickedObject);
                        }
                        else { inventory.ChangeWeapons(pickedObject); }
                        break;

                    case "Throwable":
                        Debug.Log("Bruh");
                        if (inventory.weapon1 == null) 
                        {
                            inventory.weapon1 = prefab; 
                        }
                        else if (inventory.weapon2 == null) { inventory.weapon2 = prefab; }
                        else 
                        {
                            if (inventory.inHandObject != null)
                            {
                                Destroy(inventory.inHandObject);
                                GameObject thrownObject = Instantiate(inventory.inHandObject, inventory.inHandPosition.position, Quaternion.identity);
                            }
                            inventory.inHandObject = pickedObject;
                            inventory.SpawnCoordinates(pickedObject); 
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
