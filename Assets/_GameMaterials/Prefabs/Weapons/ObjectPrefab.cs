using UnityEngine;

public class ObjectPrefab : MonoBehaviour
{
    public string prefab;
    public int PlaceInInventory;
    public string Enemy_Player;

    public void GetPlaceInInventory(int weaponNumber)
    {
        PlaceInInventory = weaponNumber;
    }
}
