using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform player;
    [SerializeField] private float threshold;
    [SerializeField] private LayerMask groundMask;

    private void Update()
    {
        MoveCameraFollowObject();
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            return (success: true, position: hitInfo.point);
        else
            return (success: false, position: Vector3.zero);
    }

    private void MoveCameraFollowObject()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            var direction = position - player.transform.position;

            Vector3 newPosition = direction / 2f;

            if (newPosition.magnitude > threshold)
            {
                newPosition = newPosition.normalized * threshold;
            }

            transform.position = player.transform.position + newPosition;
        }
    }
}
