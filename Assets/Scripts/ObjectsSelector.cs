using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSelector : MonoBehaviour
{
    private Vector2 pos1;
    private Vector2 pos2;

    GameObject obj1;
    GameObject obj2;

    private List<LineManager> lines = new List<LineManager>();

    public GameObject line;

    [SerializeField]
    private GameObject[] objPositions;

    [SerializeField]
    private PlayerLineManager playerLine;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("1");
            bool obj1Found = false;
            bool obj2Found = false;
            pos1 = playerLine.GetPositions()[0];
            pos2 = playerLine.GetPositions()[1];
            foreach (GameObject pos in objPositions)
            {
                if (Vector3.Distance((Vector3)pos1, pos.transform.position) < 1 && pos.GetComponent<DragDrop>().active == true 
                    && pos.GetComponent<DragDrop>().connections < 2)
                {
                    Debug.Log("2");
                    obj1 = pos;
                    obj1Found = true;
                }

                if (Vector3.Distance((Vector3)pos2, pos.transform.position) < 1 && obj1 != pos 
                    && pos.GetComponent<DragDrop>().active == true 
                    && obj1.GetComponent<DragDrop>().colour == pos.GetComponent<DragDrop>().colour 
                    && (pos.GetComponent<DragDrop>().connections < 2 || pos.GetComponent<DragDrop>().isReclying))
                {
                    Debug.Log("3");
                    obj2 = pos;
                    obj2Found = true;
                }
            }

            if (obj1Found && obj2Found)
            {
                Debug.Log("4");

                foreach (LineManager l in lines)
                {
                    if ((l.GetPositions()[0] == obj1 && l.GetPositions()[1] == obj2) ||
                        (l.GetPositions()[0] == obj2 && l.GetPositions()[1] == obj1))
                    {
                        return;
                    }
                }

                foreach (LineManager l in lines)
                {
                    if (l.GetPositions()[0] == obj1 || l.GetPositions()[1] == obj2 ||
                        l.GetPositions()[0] == obj2 || l.GetPositions()[1] == obj1)
                    {
                        continue;
                    }

                    if (IsIntersecting(l, obj1, obj2))
                    {
                        return;
                    }
                }

                GameObject newLine = Instantiate(line);
                newLine.GetComponent<LineManager>().Activate(obj1, obj2);

                lines.Add(newLine.GetComponent<LineManager>());
            }

        }
    }
    bool IsIntersecting(LineManager line1, GameObject obj1, GameObject obj2)
    {
        bool intersects = false;

        //3d -> 2d
        Vector2 p1 = line1.GetPositions()[0].transform.position;
        Vector2 p2 = line1.GetPositions()[1].transform.position;

        Vector2 p3 = obj1.transform.position;
        Vector2 p4 = obj2.transform.position;

        float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);

        //Make sure the denominator is > 0, if so the lines are parallel
        if (denominator != 0)
        {
            float u_a = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float u_b = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            //Is intersecting if u_a and u_b are between 0 and 1
            if (u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1)
            {
                intersects = true;
            }

        }

        return intersects;
    }
}
