using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLineManager : MonoBehaviour
{
    [SerializeField]
    private int nodesToUnlock;
    private LineRenderer line;
    private Vector2 pos1;
    private Vector2 pos2;
    bool pos1Selected;

    private void Awake()
    {

        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameState.unlockedNodes >= nodesToUnlock)
        {
            line.enabled = true;
            pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos1Selected = true;
        }

        if (Input.GetMouseButton(0) && pos1Selected)
        {
            pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3[] temp = new Vector3[2] { (Vector3)pos1, (Vector3)pos2 };
            line.SetPositions(temp);
        }

        if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;
            pos1Selected = false;
        }


    }
    public Vector2[] GetPositions()
    {
        Vector2[] positions = new Vector2[2] { pos1, pos2 };
        return positions;
    }
}
