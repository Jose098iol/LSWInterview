using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float Smooth;
    [SerializeField] private float Offset;

    private float xPos;
    private float yPos;

    [SerializeField] private float LimitDown;
    [SerializeField] private float LimitUp;
    [SerializeField] private float LimitRight;
    [SerializeField] private float LimitLeft;

    private void LateUpdate()
    {
        UpdateCameraEdges();
        CameraMov();
    }
    private void UpdateCameraEdges()
    {
        updateXpos();
        updateYpos();
    }
    private void updateXpos()
    {
        xPos = Player.position.x;
        if (xPos < LimitLeft)
        {
            xPos = LimitLeft;
        }
        else if (xPos > LimitRight)
        {
            xPos = LimitRight;
        }
    }
    private void updateYpos()
    {
        yPos = Player.transform.position.y + Offset;
        if (yPos < LimitDown)
        {
            yPos = LimitDown;
        }
        else if (yPos > LimitUp)
        {
            yPos = LimitUp;
        }
    }
    private void CameraMov()
    {
        Vector3 desiredPos = new Vector3(xPos, yPos, transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, Smooth);
        transform.position = smoothPos;
    }

}
