using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject lvlToPlace;

    private GameObject spawnedObj;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
    
        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
        }
        
        if (!GetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            spawnedObj = Instantiate(lvlToPlace, hitPose.position, hitPose.rotation);
        }
    }
}
