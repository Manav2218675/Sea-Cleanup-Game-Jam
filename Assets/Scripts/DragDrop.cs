using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector3 mousePostionOffset;
    private Vector2 startPos;
    private Vector2 endPos;

    public string type;
    public string colour;
    public bool active;
    [HideInInspector]
    public int connections = 0;
    public bool isReclying;
    private bool validPosition;
    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        if (!active)
        {
            mousePostionOffset = gameObject.transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        if (!active)
        {
            transform.position = GetMouseWorldPosition() + mousePostionOffset;
        }
    }

    private void OnMouseUp()
    {
        if(validPosition == true)
        {
            active = true;
            gameObject.transform.position = endPos;
            GameState.unlockedNodes++;

        }
        else
        {
            gameObject.transform.position = startPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Slot>().type == type)
        {
            validPosition = true;
            endPos = collision.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        validPosition = false;
    }
}
