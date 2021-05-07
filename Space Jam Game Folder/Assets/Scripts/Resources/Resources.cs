using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class StaticRes
    {
        public static float LookDir(Vector3 target)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.WorldToScreenPoint(target);

            mousePos.x -= targetPos.x;
            mousePos.y -= targetPos.y;

            return Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        }

        public static float TargetLookDir2D(Vector2 self, Vector2 target)
        {
            Vector2 dir = self - target;

            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }
}
