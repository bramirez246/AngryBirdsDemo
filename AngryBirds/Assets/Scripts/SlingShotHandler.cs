using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NewBehaviourScript : MonoBehaviour
{
    [Header("Line Renderers")]
    //initialize all line renderers
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;

    [Header("Transform References")]
    //initialize all transform variables
    [SerializeField] private Transform leftStartPosition;
    [SerializeField] private Transform rightStartPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [Header("Slingshot Variables")]
    //initialize max distance float for slingshot
    [SerializeField] private float maxDistance = 4.0f;

    [Header("Scripts")]
    //variable of type SlingShotArea script
    [SerializeField] private SlingShotArea slingShotArea;

    //global sling shot line position vector2
    private Vector2 SlingShotLinePosition;

    //bool to determine if user clicked within slingshot area
    private bool clickedWithinArea;


    private void Awake()
    {
        //disable line renderers at start of game
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingShotArea())
        {
            clickedWithinArea = true;
        }

        if (Mouse.current.leftButton.isPressed && clickedWithinArea)
        {
           DrawSlingShot();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickedWithinArea = false;
        }
    }

    private void DrawSlingShot()
    {
        if (!leftLineRenderer.enabled && !rightLineRenderer.enabled)
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;
        }

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SlingShotLinePosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);

        SetLines(SlingShotLinePosition);
    }

    private void SetLines(Vector2 position)
    {
        //set left line renderer positions
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);
        
        //set right line renderer positions
        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
