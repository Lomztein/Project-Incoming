﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public Slider healthSlider;

    public void SetHealth (float value) {
        healthSlider.value = value;
    }

}
