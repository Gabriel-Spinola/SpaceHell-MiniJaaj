using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager I { get; private set; }

        [Header("Keys")]
        public float xAxis, yAxis;
        public bool isShootingKeyDown, isShootingKeyPressed, isReloadingKey;

        private void Awake() => I = this;

        private void Update()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            isShootingKeyPressed = Input.GetButton("Fire1");
            isShootingKeyDown = Input.GetButtonDown("Fire1");

            isReloadingKey = Input.GetKeyDown(KeyCode.R);
        }
    }
}