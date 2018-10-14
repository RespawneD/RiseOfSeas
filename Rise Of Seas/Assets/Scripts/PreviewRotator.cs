﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRotator : MonoBehaviour {

	float speed = 180f;

    void Update () {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);	
	}
}
