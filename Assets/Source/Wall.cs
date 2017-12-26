using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHasHealthbar {

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
        Invoke ("AddHealthbar", 0.1f);
    }

    public void BuildWall() {
        foreach (WallPiece piece in pieces)
            if (piece)
                Destroy (piece.gameObject);
        pieces = new List<WallPiece> ();

        Vector3 direction = transform.right;
        for (int i = -segmentAmount; i <= segmentAmount; i++) {
            GameObject newSegment = Instantiate (wallPrefab, transform.position + direction * segmentLength * i, transform.rotation);
            newSegment.transform.SetParent (transform);

            WallPiece piece = newSegment.GetComponent<WallPiece> ();
            pieces.Add (piece);
        }
    }

    void AddHealthbar () {
        BaseHealthbars.AddHealthbar (new BaseHealthbars.Bar (gameObject, Color.yellow));
    }

    public static float GetTotalHealth() {
        float total = 0f;
        wall.pieces.ForEach (x => total += x.health);
        return total;
    }

    public float GetTotalMaxHealth () {
        return wall.pieces.Count * wall.pieces [ 0 ].maxHealth;
    }

    public float GetHealthPercentage() {
        return GetTotalHealth () / (GetTotalMaxHealth ());
    }
}
