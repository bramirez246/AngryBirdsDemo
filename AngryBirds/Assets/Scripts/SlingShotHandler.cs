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

    [Header("Bird")]
    [SerializeField] private GameObject angryBirdPrefab;
    [SerializeField] private float angryBirdPositionOffset = 0.3f;

    //global sling shot line position vector2
    private Vector2 SlingShotLinePosition;
    private Vector2 direction;
    private Vector2 directionNormalized;

    //bool to determine if user clicked within slingshot area
    private bool clickedWithinArea;

    private GameObject spawnedAngryBird;


    private void Awake()
    {
        //disable line renderers at start of game
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;

        //spawn angry bird
        SpawnAngryBird();
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
           PositionAndRotateAngryBird();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickedWithinArea = false;
        }
    }

    #region Slingshot Methods

    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SlingShotLinePosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);

        SetLines(SlingShotLinePosition);

        direction = (Vector2)centerPosition.position - SlingShotLinePosition;
        directionNormalized = direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!leftLineRenderer.enabled && !rightLineRenderer.enabled)
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;
        }

        //set left line renderer positions
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);
        
        //set right line renderer positions
        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }

    #endregion

    #region Angry Bird Methods
    private void SpawnAngryBird()
    {
        SetLines(idlePosition.position);

        Vector2 dir = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPos = (Vector2)idlePosition.position + dir * angryBirdPositionOffset;

        spawnedAngryBird = Instantiate(angryBirdPrefab, spawnPos, Quaternion.identity);
        spawnedAngryBird.transform.right = dir;
    }

    private void PositionAndRotateAngryBird()
    {
        spawnedAngryBird.transform.position = SlingShotLinePosition + directionNormalized * angryBirdPositionOffset;
        spawnedAngryBird.transform.right = directionNormalized;
    }
    #endregion
}
