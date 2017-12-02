using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public static Wall wall;

    public GameObject wallPrefab;
    public List<WallPiece> pieces;

    public float segmentLength;
    public int segmentAmount;

    private void Awake() {
        wall = this;
    }

    private void Start() {
        BuildWall ();
    }

    public void BuildWall() {
        Vector3 direction = transform.right;
        for (int i = -segmentAmount; i <= segmentAmount; i++) {
            GameObject newSegment = Instantiate (wallPrefab, transform.position + direction * segmentLength * i, transform.rotation);
            newSegment.transform.SetParent (transform);

            WallPiece piece = newSegment.GetComponent<WallPiece> ();
            pieces.Add (piece);
        }
    }

    public static float GetTotalHealth() {
        float total = 0f;
        wall.pieces.ForEach (x => total += x.health);
        return total;
    }

}
