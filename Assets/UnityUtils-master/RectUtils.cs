using UnityEngine;

namespace Utils
{
    public class RectUtils
    {
        public static Bounds RectTransformToScreenSpace(RectTransform transform)
        {
            
            /*
             * Usage for copying rect transform to world transform:
            var screenSpace = RectTransformToScreenSpace(someRectTransform);
            var bottomLeft = screenSpace.min;
            var topRight = screenSpace.max;
            Generate(new Rect(bottomLeft.x, bottomLeft.y,
                topRight.x - bottomLeft.x, topRight.y - bottomLeft.y));
             * 
             */

            // This if it's overlay canvas
            var worldCorners = new Vector3[4];
            transform.GetWorldCorners(worldCorners);
        
            var bounds = new Bounds(worldCorners[0], Vector3.zero);
            for(var i = 1; i < 4; ++i)
            {
                bounds.Encapsulate(worldCorners[i]);
            }

            return bounds;
        }
    }
}