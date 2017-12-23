using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveEnemyEntry : MonoBehaviour {

    public GameObject enemyObject;
    private Enemy enemy;

    public RawImage image;
    public Text text;
    public int count;

    public void UpdateGUI () {
        enemy = enemyObject.GetComponent<Enemy> ();
        image.texture = Iconography.GenerateIcon (enemyObject);
        IDescribed described = enemy as IDescribed;
        text.text = described.Name + "\nX " + count;
    }

}
