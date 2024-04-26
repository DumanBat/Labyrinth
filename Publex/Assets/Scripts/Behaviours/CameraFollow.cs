using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float smoothSpeed = 0.125f;

        private Transform _target;
        private Transform _offsetTransform;
        private Vector3 _offset;

        public void Init(Transform target, Transform offsetTransform)
        {
            _target = target;
            _offsetTransform = offsetTransform;

            _offset = _offsetTransform.position - _target.position;
            transform.position = _offsetTransform.position;
            transform.rotation = _offsetTransform.rotation;
        }

        void FixedUpdate()
        {
            if (_target == null)
                return;

            Vector3 desiredPosition = _target.position + _offset;
            desiredPosition.y = transform.position.y;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}