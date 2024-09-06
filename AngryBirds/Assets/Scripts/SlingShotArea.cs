using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask slingShotArea;

    public bool IsWithinSlingShotArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //if the collider detects mouse click return true
        if (Physics2D.OverlapPoint(worldPosition, slingShotArea))
        {
            return true;
        }

        //else return false
        return false;
    }
}
