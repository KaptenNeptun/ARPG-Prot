﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(1, 1);
    CameraRaycaster camRay;
	// Use this for initialization
	void Start () {
        camRay = this.gameObject.GetComponent<CameraRaycaster>();
        camRay.onLayerChangeObs += CursorLayerCheck; //registering a delegate
	}
	//TODO consider deregistering CursorLayerCheck on leaving scenes
	// Update is called once per frame
	void CursorLayerCheck(Layer newLayer)
    {
        switch(newLayer)
        {
            case (Layer.Walkable):
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case (Layer.Enemy):
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;
            case (Layer.RaycastEndStop):
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                return;
        }

        print("Layerhit is " + camRay.currentLayerHit);
	}
}
