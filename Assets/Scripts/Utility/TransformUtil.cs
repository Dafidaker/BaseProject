using UnityEngine;

namespace Utility
{
    public class TransformUtil : MonoBehaviour
    {
        /// <summary>
        /// returns a transform with the information of the transform passed but its independent from the original transform.
        /// </summary>
        public static Transform CreateStandaloneTransform(Transform copyFrom)
        {
            if (copyFrom == null) return null;
       
            var copiedObject = new GameObject("CopiedTransform");
            var copiedTransform = copiedObject.transform;

            copiedTransform.position = copyFrom.position;
            copiedTransform.localPosition = copyFrom.localPosition;
            copiedTransform.rotation = copyFrom.rotation;
            copiedTransform.localScale = copyFrom.localScale;

            return copiedTransform;
        }
        
        /// <summary>
        /// returns a transform with the information of the transform passed but its independent from the original transform.
        /// </summary>
        public static void CopyTransformData(Transform copyFrom,Transform copyTo)
        {
            if (copyTo == null || copyFrom == null)
            {
                CopyTransformDataDebug(copyFrom, copyTo);    
                return;
            }
            
            copyTo.position = copyFrom.position;
            copyTo.rotation = copyFrom.rotation;
            copyTo.localScale = copyFrom.localScale;
        }

        private static void CopyTransformDataDebug(Transform copyFrom, Transform copyTo)
        {
            if (copyFrom == null)
            {
                Debug.LogWarning("CopyTransformData method received a null copyFrom transform");
            }

            if (copyTo == null)
            {
                Debug.LogWarning("CopyTransformData method received a null copyTo transform");
            }
        }
        
        
        /// <summary>
        /// returns a RectTransform with the information of the RectTransform passed but its independent from the original RectTransform.
        /// </summary>
        public static RectTransform CreateStandaloneRectTransform(RectTransform copyFrom)
        {
            if (copyFrom == null) return null;

            var copiedObject = new GameObject("CopiedRectTransform", typeof(RectTransform));
            var copiedRectTransform = copiedObject.GetComponent<RectTransform>();

            copiedRectTransform.SetParent(copyFrom.parent, false); 
            
            copiedRectTransform.position = copyFrom.position;
            copiedRectTransform.rotation = copyFrom.rotation;
            copiedRectTransform.localScale = copyFrom.localScale;

            copiedRectTransform.sizeDelta = copyFrom.sizeDelta;
            copiedRectTransform.anchoredPosition = copyFrom.anchoredPosition;
            copiedRectTransform.anchorMin = copyFrom.anchorMin;
            copiedRectTransform.anchorMax = copyFrom.anchorMax;
            copiedRectTransform.pivot = copyFrom.pivot;

            return copiedRectTransform;
        }
    }
}
