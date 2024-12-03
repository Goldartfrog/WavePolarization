using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUI
{
    public class SliderVR : MonoBehaviour
    {
        [SerializeField] private RectTransform MinVal_tr, MaxVal_tr, CurVal_tr;

        [SerializeField] private float minVal, maxVal, totalRange, curVal;

        [SerializeField] private Slider slider;

        public Vector3 Track;

        void Awake()
        {
            minVal = slider.minValue;
            maxVal = slider.maxValue;
            totalRange = maxVal - minVal;
            Track = MaxVal_tr.position - MinVal_tr.position;
            UpdateValue();
        }

        // Update is called once per frame
        void Update()
        {
            if (curVal != slider.value) slider.value = curVal;
        }

        public void UpdateValue()
        {
            curVal = (CurVal_tr.position - MinVal_tr.position).magnitude / (MaxVal_tr.position - MinVal_tr.position).magnitude * totalRange;
        }
    }
}
