using TMPro;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    public GameObject stopPanel;
    public GameObject arrow;

    void Start()
    {
        
    }

    public bool switcher = true;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stopPanel.SetActive(switcher);
            arrow.SetActive(!switcher);

            Cursor.visible = switcher;
            if (switcher)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            switcher = !switcher;
        }
    }




    public void ContiueButton()
    {
        stopPanel.SetActive(switcher);
        arrow.SetActive(!switcher);

        Cursor.visible = switcher;
        if (switcher)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        switcher = !switcher;

        /*stopPanel.SetActive(false);
        arrow.SetActive(true);
        switcher = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }


    public void ExitButton()
    {

    }

    public void SettingsButton()
    {

    }
}
