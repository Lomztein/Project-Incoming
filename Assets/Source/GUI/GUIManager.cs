using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    public static GUIManager manager;
    public List<GUIWindowBase> allWindows = new List<GUIWindowBase> ();
    public Canvas defaultCanvas;

    private void Awake() {
        manager = this;
    }

    public static void AddWindow (GUIWindowBase window) {
        manager.allWindows.Add (window);
    }

    public static void RemoveWindow (GUIWindowBase window) {
        manager.allWindows.Remove (window);
    }
	
	public static void CloseAllWindows () {
        for (int i = 0; i < manager.allWindows.Count; i++) {
            manager.allWindows [ i ].Close ();
        }
    }
}
