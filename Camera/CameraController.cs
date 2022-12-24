using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance;
    private float _movementSpeed = 25;
    private float _movementTime = 25;
    private Vector3 _newPosition;
    private Vector3 _startPosition;
    private bool _isAbleToMove = true;

    private void Start()
    {
        _startPosition = transform.position;
        _newPosition = transform.position;
    }

    private void Update()
    {
        if (_isAbleToMove)
            CameraPositionTransform();
        else MoveCameraToStartPosition();
    }

    private void MoveCameraToStartPosition()
    {
        if (Vector3.Distance(transform.position, _startPosition) >= 1)
            transform.position = Vector3.Lerp(transform.position, _startPosition, _movementTime * Time.deltaTime);
        else
        {
            _newPosition = transform.position;
            _isAbleToMove = true;
        }

    }

    private void CameraPositionTransform()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, _rayDistance, _layerMask))
        {
            _isAbleToMove = false;
        }

        if (_isAbleToMove && Input.GetAxis("Vertical") > 0)
            _newPosition += transform.forward * _movementSpeed * Time.deltaTime;
        if (_isAbleToMove && Input.GetAxis("Vertical") < 0)
            _newPosition += -transform.forward * _movementSpeed * Time.deltaTime;

        if (_isAbleToMove && Input.GetAxis("Horizontal") > 0)
            _newPosition += transform.right * _movementSpeed * Time.deltaTime;
        if (_isAbleToMove && Input.GetAxis("Horizontal") < 0)
            _newPosition += -transform.right * _movementSpeed * Time.deltaTime;

        if (_isAbleToMove && Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y >= 25)
            _newPosition.y += -4 * _movementSpeed * Time.deltaTime;
        if (_isAbleToMove && Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y <= 65)
            _newPosition.y += 4 * _movementSpeed * (Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, _newPosition, _movementTime * Time.deltaTime);
    }
}