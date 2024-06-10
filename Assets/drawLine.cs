using System.Collections.Generic;
using UnityEngine;

public class drawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public float lineWidth = 0.05f;

    private LineRenderer currentLineRenderer;
    private List<Vector2> fingerPositions;
    private bool isDrawing = false;

    theBall theBall;

    void Start()
    {
        theBall = GameObject.FindAnyObjectByType<theBall>();
    }

    void Update()
    {
        if (theBall.gameStarted)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    StartDrawing(touchPosition);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    UpdateDrawing(touchPosition);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    FinishDrawing();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                StartDrawing(mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                UpdateDrawing(mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                FinishDrawing();
            }
        }
    }

    void StartDrawing(Vector2 position)
    {
        GameObject line = Instantiate(linePrefab);
        currentLineRenderer = line.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        fingerPositions = new List<Vector2>();
        fingerPositions.Add(position);
        fingerPositions.Add(position);
        currentLineRenderer.SetPosition(0, fingerPositions[0]);
        currentLineRenderer.SetPosition(1, fingerPositions[1]);
        isDrawing = true;
    }

    void UpdateDrawing(Vector2 position)
    {
        if (isDrawing)
        {
            fingerPositions.Add(position);
            currentLineRenderer.positionCount = fingerPositions.Count;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, position);
        }
    }

    void FinishDrawing()
    {
        if (isDrawing)
        {
            AddCollider();
            isDrawing = false;
        }
    }

    void AddCollider()
    {
        EdgeCollider2D edgeCollider = currentLineRenderer.gameObject.AddComponent<EdgeCollider2D>();
        Vector2[] colliderPoints = new Vector2[fingerPositions.Count];
        for (int i = 0; i < fingerPositions.Count; i++)
        {
            colliderPoints[i] = fingerPositions[i];
        }
        edgeCollider.points = colliderPoints;
    }
}