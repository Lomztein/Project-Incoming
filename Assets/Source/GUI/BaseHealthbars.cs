using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthbars : MonoBehaviour {

    public static BaseHealthbars baseHealthbars;
    public List<Bar> allBars = new List<Bar> ();
    public GameObject barPrefab;

    private void Awake() {
        baseHealthbars = this;
    }

    public static void AddHealthbar (Bar newBar) {
        baseHealthbars.allBars.Add (newBar);
        GameObject newHealthbarObject = Instantiate (baseHealthbars.barPrefab, baseHealthbars.transform);
        newBar.healthbar = newHealthbarObject.GetComponent<Slider> ();
        newBar.Initialize ();
    }

    void Update () {
        foreach (Bar bar in allBars) {
            bar.Update ();
        }
	}

    [System.Serializable]
    public class Bar {

        public Bar (GameObject _obj, Color _color) {
            obj = _obj.GetComponentInChildren<IHasHealthbar>(); healthbarColor = _color;
        }

        public IHasHealthbar obj;
        public Slider healthbar;
        public Color healthbarColor;

        public void Initialize () {
            healthbar.fillRect.GetComponent<Image> ().color = healthbarColor;
        }

        public void Update () {
            healthbar.value = obj.GetHealthPercentage ();
        }

    }
}
