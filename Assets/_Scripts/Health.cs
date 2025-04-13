using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI health;
    public int hp = 100;

    public void Update()
    {
        HealthText();
    }
    public void HealthText()
    {
        if (hp <= 0) { health.text = "Crying"; }
        if (hp > 0) { health.text = $"{hp}"; }
    }

    public void OnCollisionEnter(Collision other)
    {
        ObjectPrefab objectPrefab = other.gameObject.GetComponent<ObjectPrefab>();
        if (other.gameObject.CompareTag("Weapon") && objectPrefab.Enemy_Player == "Player") { hp = hp - 6; }
    }
}
