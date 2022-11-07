using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraController;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public SpriteRenderer focusSpriteRenderer;
    public float offsetX = 0f;

    public float offsetY = 0f;

    public float dragSpeed = 2f;

    float maxX, maxY, minX, minY;

    float moveX, moveY;

    private Camera camera;

    private bool cameraIsDragged;
    private Vector3 dragOrigin = Vector2.zero;

    private float originAspect;
    public enum ControlType
    {
        ConstantWidth,      // Camera will keep constant width
        ConstantHeight,     // Camera will keep constant height
        OriginCameraSize	// Do not scale camera
    }
    public ControlType controlType;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        originAspect = camera.aspect;
        maxX = focusSpriteRenderer.bounds.max.x;
        minX = focusSpriteRenderer.bounds.min.x;
        maxY = focusSpriteRenderer.bounds.max.y;
        minY = focusSpriteRenderer.bounds.min.y;
        UpdateCameraSize();

    }

    // Update is called once per frame
    void Update()
    {
        //if Camera Aspect change
        if (originAspect != camera.aspect)
        {
            UpdateCameraSize();
            originAspect = camera.aspect;
        }
        //Move camera left/right
        if (moveX != 0f)
        {
            //bool to check if able to move left/right
            bool permit = false;
            //move right
            if (moveX > 0f)
            {
                //if camera limit does not meet
                if (camera.transform.position.x + (camera.orthographicSize * camera.aspect) < maxX - offsetX)
                {
                    permit = true;
                }
            }
            //move left
            else
            {
                //if camera limit does not meet
                if (camera.transform.position.x - (camera.orthographicSize * camera.aspect) > minX + offsetX)
                {
                    permit = true;
                }
            }
            if (permit == true)
            {
                transform.Translate(Vector2.right * moveX * dragSpeed, Space.World);
            }
            moveX = 0f;
        }
        // Need to move camera up/down
        if (moveY != 0f)
        {
            bool permit = false;
            // Move up
            if (moveY > 0f)
            {
                // If camera limit does not meet
                if (camera.transform.position.y + camera.orthographicSize < maxY - offsetY)
                {
                    permit = true;
                }
            }
            // Move down
            else
            {
                // If camera limit does not meet
                if (camera.transform.position.y - camera.orthographicSize > minY + offsetY)
                {
                    permit = true;
                }
            }
            if (permit == true)
            {
                // Move camera
                transform.Translate(Vector2.up * moveY * dragSpeed, Space.World);
            }
            moveY = 0f;
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            cameraIsDragged = true;
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            // Stop drag camera on mouse release
            cameraIsDragged = false;
        }
        if (cameraIsDragged == true)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            // Camera dragging (inverted)
            MoveX(-pos.x);
            MoveY(-pos.y);
        }
    }
    public void MoveX(float distance)
    {
        moveX = distance;
    }

    public void MoveY(float distance)
    {
        moveY = distance;
    }
    private void UpdateCameraSize()
    {
        switch (controlType)
        {
            case ControlType.ConstantWidth:
                camera.orthographicSize = (maxX - minX - 2 * offsetX) / (2f * camera.aspect);
                break;
            case ControlType.ConstantHeight:
                camera.orthographicSize = (maxY - minY - 2 * offsetY) / 2f;
                break;
        }
    }
}

