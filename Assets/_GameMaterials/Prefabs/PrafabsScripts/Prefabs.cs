using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public GameObject Book;
    public GameObject BigBook;
    public GameObject Stick_1;
    public GameObject Stick_2;
    public GameObject Stick_3;
    public GameObject Paper;
    public GameObject Pen;
    public GameObject Pensil;
    public GameObject Pointer;
    public GameObject Ruler;
    public GameObject Crayon;

    public GameObject Eraser_1;
    public GameObject Eraser_2;
    public GameObject Eraser_3;

    public GameObject Slingshot;

    public GameObject prefab;

    public void GetPrefab(string tag)
    {
        switch(tag)
        {
            case "Book": prefab = Book; break;
            case "BigBook": prefab = BigBook; break;
            case "Stick_1": prefab = Stick_1; break;
            case "Stick_2": prefab = Stick_2; break;
            case "Stick_3": prefab = Stick_3; break;
            case "Paper": prefab = Paper; break;
            case "Pen": prefab = Pen; break;
            case "Pensil": prefab = Pensil; break;
            case "Pointer": prefab = Pointer; break;
            case "Ruler": prefab = Ruler; break;
            case "Crayon": prefab = Crayon; break;

            case "Eraser_1": prefab = Eraser_1; break;
            case "Eraser_2": prefab = Eraser_2; break;
            case "Eraser_3": prefab = Eraser_3; break;

            case "Slingshot": prefab = Slingshot; break;
        }
    }
}
