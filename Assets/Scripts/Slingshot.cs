using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public static Slingshot Instance { get; private set; }

    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;

    private Transform leftPoint;
    private Transform rightPoint;
    private Transform centerPoint;

    private bool isDrawing = false;
    private Transform birdTransform;

    private void Awake()
    {
        Instance = this;

        leftLineRenderer = transform.Find("LeftLineRenderer").GetComponent<LineRenderer>();
        rightLineRenderer = transform.Find("RightLineRenderer").GetComponent<LineRenderer>();
        leftPoint = transform.Find("LeftPoint");
        rightPoint = transform.Find("RightPoint");
        centerPoint = transform.Find("CenterPoint");
    }

    void Start()
    {
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDrawing)
        {
            Draw();
        }
    }

    public void StartDraw(Transform birdTransform)
    {
        isDrawing = true;
        this.birdTransform = birdTransform;
        ShowLine();
    }

    public void EndDraw()
    {
        isDrawing = false;
        HideLine();
    }

    public void Draw()
    {
        Vector3 birdPosition = birdTransform.position;

        birdPosition = (birdPosition - centerPoint.position).normalized * (birdTransform.localScale.x / 2) + birdPosition;

        leftLineRenderer.SetPosition(0, leftPoint.position);
        leftLineRenderer.SetPosition(1, birdPosition);
        rightLineRenderer.SetPosition(0, rightPoint.position);
        rightLineRenderer.SetPosition(1, birdPosition);   
    }

    public Vector3 getCenterPosition()
    {
        return centerPoint.transform.position;
    }

    private void HideLine()
    {
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;
    }

    private void ShowLine()
    {
        leftLineRenderer.enabled = true;
        rightLineRenderer.enabled = true;
    }
}
