using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Iconography : MonoBehaviour {

    public static Iconography iconography;
    public static Camera renderCamera { get { return iconography.camera;  } }
    public static int renderSize = 128;

    new public Camera camera;


    private void Awake() {
        iconography = this;
        gameObject.SetActive (false);
    }

    public static Texture2D GenerateIcon (GameObject obj) {

        iconography.gameObject.SetActive (true);

        renderCamera.enabled = true;
        renderCamera.aspect = 1f;

        // First create object and strip away all non-transform non-renderer components.
        GameObject model = Instantiate (obj, iconography.transform.position, Quaternion.identity, iconography.transform);
        List<Component> nonVitals = model.GetComponentsInChildren<Component> ().Where (x => !(x is Transform) && !(x is Renderer)).ToList ();
        foreach (Component comp in nonVitals) {
            Destroy (comp); // Might not be neccesary, test sometime.
        }

        RenderTexture renderTexture = new RenderTexture (renderSize, renderSize, 24);
        renderTexture.Create ();

        Bounds bounds = GetObjectBounds (model);

        float camSize = Mathf.Max (Mathf.Abs (bounds.extents.y), Mathf.Abs (bounds.extents.z));
        renderCamera.orthographicSize = camSize;

        renderCamera.targetTexture = renderTexture;
        renderCamera.transform.position = bounds.center + renderCamera.transform.forward * Mathf.Max (bounds.extents.x, bounds.extents.y, bounds.extents.z) * -2f;
        renderCamera.Render ();

        RenderTexture.active = renderTexture;

        Texture2D texture = new Texture2D (renderSize, renderSize, TextureFormat.ARGB32, false);
        texture.ReadPixels (new Rect (0f, 0f, renderSize, renderSize), 0, 0);
        texture.Apply ();

        renderCamera.targetTexture = null;
        RenderTexture.active = null;

        Destroy (renderTexture);

        renderCamera.enabled = false;
        iconography.gameObject.SetActive (false);

        model.SetActive (false);
        Destroy (model);
        return texture;
    }

    public static Bounds GetObjectBounds(GameObject obj) {
        MeshFilter [ ] filters = obj.GetComponentsInChildren<MeshFilter> ();

        CombineInstance [ ] instances = new CombineInstance [ filters.Length ];
        for (int i = 0; i < filters.Length; i++) {
            instances [ i ].mesh = filters [ i ].sharedMesh;
            instances [ i ].transform = filters [ i ].transform.localToWorldMatrix;
        }

        Mesh newMesh = new Mesh ();
        newMesh.CombineMeshes (instances);
        newMesh.RecalculateBounds ();

        return newMesh.bounds;
    }
}
