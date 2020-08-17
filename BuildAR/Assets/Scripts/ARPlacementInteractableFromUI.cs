using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARPlacementInteractableFromUI : ARPlacementInteractable
{
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    static GameObject s_TrackablesObject;
    ARObjectPlacedEvent m_OnObjectPlaced = new ARObjectPlacedEvent();
    /// <summary>Gets or sets the event that is called when the this interactable places a new GameObject in the world.</summary>
    public ARObjectPlacedEvent onObjectPlaced { get { return m_OnObjectPlaced; } set { m_OnObjectPlaced = value; } }
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.StartPosition.IsOverUI())
        {
            return false;
        }

        if (gesture.TargetObject == null || gesture.TargetObject.layer == 9)
        {
            return true;
        }

        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.WasCancelled)
            return;

        // If gesture is targeting an existing object we are done.
        // Allow for test planes
        if (gesture.TargetObject != null && gesture.TargetObject.layer != 9)
            return;

        // Raycast against the location the player touched to search for planes.
        if (GestureTransformationUtility.Raycast(gesture.StartPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = s_Hits[0];

            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (Vector3.Dot(Camera.main.transform.position - hit.pose.position,
                hit.pose.rotation * Vector3.up) < 0)
                return;

            // Instantiate placement prefab at the hit pose.
            var placementObject = Instantiate(placementPrefab, hit.pose.position, hit.pose.rotation);
            placementPrefab = null;

            // Create anchor to track reference point and set it as the parent of placementObject.
            // TODO: this should update with a reference point for better tracking.
            var anchorObject = new GameObject("PlacementAnchor");
            anchorObject.transform.position = hit.pose.position;
            anchorObject.transform.rotation = hit.pose.rotation;
            placementObject.transform.parent = anchorObject.transform;

            // Find trackables object in scene and use that as parent
            if (s_TrackablesObject == null)
                s_TrackablesObject = GameObject.Find("Trackables");
            if (s_TrackablesObject != null)
                anchorObject.transform.parent = s_TrackablesObject.transform;

            if (m_OnObjectPlaced != null)
                m_OnObjectPlaced.Invoke(this, placementObject);
        }
    }
}
