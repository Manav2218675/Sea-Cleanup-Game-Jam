using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    //set the two objects
    private GameObject[] objects;
    LineRenderer line;
    private bool isActive;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    private void Update()
    {
        if (isActive)
        {
            Vector3[] temp = new Vector3[2] { objects[0].transform.position, objects[1].transform.position };
            line.SetPositions(temp);
        }
    }

    public void Activate(GameObject obj1, GameObject obj2)
    {
        Debug.Log("activate");
        obj1.GetComponent<DragDrop>().connections++;
        obj2.GetComponent<DragDrop>().connections++;
        objects = new GameObject[2] { obj1, obj2 };
        isActive = true;
        line.enabled = true;
    }

    public GameObject[] GetPositions()
    {
        return this.objects;
    }
}
