using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class GUIColor : MonoBehaviour {

    public RectTransform [ ] guiElements;

    public Color primaryColor;
    public Color secondaryColor;

    public Color primaryTextColor;
    public Color secondaryTextColor;

    public Font textFont;

    private void Update() {
        UpdateColors ();
    }

    public void UpdateColors () {
        foreach (RectTransform obj in guiElements) {
            Image[] allImages = obj.GetComponentsInChildren<Image> ();
            Text[] allText = obj.GetComponentsInChildren<Text> ();


            foreach (Image img in allImages) {
                switch (img.gameObject.tag) {
                    case "UIPrimary":
                        img.color = primaryColor;
                        break;

                    case "UISecondary":
                        img.color = secondaryColor;
                        break;
                }
            }

            foreach (Text text in allText) {
                switch (text.gameObject.tag) {
                    case "UIPrimary":
                        text.color = primaryTextColor;
                        break;

                    case "UISecondary":
                        text.color = secondaryTextColor;
                        break;
                }

                text.font = textFont;
            }
        }
    }
}
