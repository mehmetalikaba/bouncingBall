using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class drawingController : MonoBehaviour
{
    public GameObject dotPrefab;
    public float dotSpacing = 0.1f;
    public GameObject drawingContainerPrefab;

    private List<Vector2> points = new List<Vector2>();
    private Vector2 lastPoint;
    private Transform currentDrawingContainer;

    void Update()
    {


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartDrawing(Camera.main.ScreenToWorldPoint(touch.position));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                ContinueDrawing(Camera.main.ScreenToWorldPoint(touch.position));
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                StartDrawing(mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueDrawing(mousePosition);
            }
        }

    }

    void StartDrawing(Vector2 position)
    {
        points.Clear();
        CreateNewDrawingContainer();
        PlaceDot(position);
        lastPoint = position;
        points.Add(position);
    }

    void ContinueDrawing(Vector2 position)
    {
        if (Vector2.Distance(position, lastPoint) > dotSpacing)
        {
            PlaceDot(position);
            lastPoint = position;
            points.Add(position);
        }
    }

    void CreateNewDrawingContainer()
    {
        GameObject newContainer = Instantiate(drawingContainerPrefab);
        currentDrawingContainer = newContainer.transform;
    }

    void PlaceDot(Vector2 position)
    {
        GameObject dot = Instantiate(dotPrefab, position, Quaternion.identity);
        dot.transform.parent = currentDrawingContainer;
    }
}