using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableMovingObjectController : MonoBehaviour, IActivateble
{
    [SerializeField] bool isClosed;
    private bool initClosedState;
    private Vector2 startPosition;
    private Vector2 endPosition;

    [SerializeField] public float speed;
    [SerializeField] public bool isVertical = true;
    [SerializeField] public float moveWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = this.transform.position;
        if (isVertical)
        {
            endPosition = new Vector2(startPosition.x, startPosition.y + spriteRenderer.bounds.size.y * moveWeight);
        } else
        {
            endPosition = new Vector2(startPosition.x + spriteRenderer.bounds.size.x * moveWeight, startPosition.y);
        }        

        initClosedState = isClosed;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = this.transform.position;
        Vector2 targetPosition = isClosed ? startPosition : endPosition;
        bool shouldBeMoving = (isClosed && currentPosition != startPosition) || (!isClosed && currentPosition != endPosition);
        

        if (shouldBeMoving)
        {
            this.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        isClosed = !initClosedState;
    }

    public void Deactivate()
    {
        isClosed = initClosedState;
    }
}
