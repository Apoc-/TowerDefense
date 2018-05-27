﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Hexen
{
    public class CameraHandler : MonoBehaviour
    {

        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var dead = 0.1;

            horizontalInput = (Math.Abs(horizontalInput) > dead) ? horizontalInput : 0;
            verticalInput = (Math.Abs(verticalInput) > dead) ? verticalInput : 0;

            PanCamera(horizontalInput, verticalInput);        
        }

        private void PanCamera(float horizontalInput, float verticalInput)
        {
            gameObject.transform.Translate(Vector3.right * horizontalInput);
            gameObject.transform.Translate(Vector3.forward * verticalInput);
        }
    }
}