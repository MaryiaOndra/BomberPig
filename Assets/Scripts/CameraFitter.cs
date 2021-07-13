using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberPig
{
    [ExecuteAlways]
    public class CameraFitter : MonoBehaviour
    {
        [SerializeField]
        private float _cameraWidth;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _camera.orthographicSize = (float)Screen.height / Screen.width * _cameraWidth;
        }

        private void Update()
        {
            _camera.orthographicSize = (float)Screen.height / Screen.width * _cameraWidth;
        }
    }
}
