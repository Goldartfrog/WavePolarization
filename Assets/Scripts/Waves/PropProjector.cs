using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Propagation
{

    public class PropProjector : MonoBehaviour
    {
        [Header("Anchors")]
        public Transform ETip;
        public Transform CenterAnchor;

        [Header("E Prefab")]
        public VectorController EArrow;

        [Header("Enable Tracer")]
        public TrailRenderer trailRenderer;

        // Start is called before the first frame update
        void Awake()
        {
            StartCoroutine(TrailCoroutine());
        }

        // Update is called once per frame
        void Update()
        {
            if (CenterAnchor.localPosition != transform.localPosition)
            {
                transform.localPosition = CenterAnchor.localPosition;
            }

            if (EArrow.VectorEndAnchor == null)
                EArrow.VectorEndAnchor = ETip;
            if (EArrow.VectorBeginAnchor == null)
                EArrow.VectorBeginAnchor = CenterAnchor;
        }

        private IEnumerator TrailCoroutine()
        {
            trailRenderer.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            trailRenderer.gameObject.SetActive(true);
        }

    }
}

