using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

namespace Resources
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager I { get; private set; }

        public float xAxis, yAxis;
        public bool isShootingKeyDown;

        private void Awake() => I = this;

        private void Update()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            isShootingKeyDown = Input.GetButtonDown("Fire1");
        }
    }
}