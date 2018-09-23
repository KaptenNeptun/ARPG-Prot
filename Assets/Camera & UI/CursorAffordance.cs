using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(1, 1);
    CameraRaycaster camRaycaster;
	// Use this for initialization
	void Start () {
        camRaycaster = GetComponent<CameraRaycaster>();
        camRaycaster.onLayerChangeObs += CursorLayerCheck;
	}

	//TODO consider deregistering CursorLayerCheck on leaving scenes
	void CursorLayerCheck(Layer newLayer) //Checks what layer the cursor hits and changes the cursor graphics depending on the layer
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
        print("Layerhit is " + camRaycaster.currentLayerHit);
	}
}
