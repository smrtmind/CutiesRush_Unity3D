﻿using UnityEngine;

namespace Scripts.Utils
{
    public class AnimationComponent : MonoBehaviour
    {
        [SerializeField] private bool _isAnimated = false;

        [Space]
        [SerializeField] private bool _isRotating = false;
        [SerializeField] private Vector3 _rotationAngle;
        [SerializeField] private float _rotationSpeed;

        [Space]
        [SerializeField] private bool _isFloating = false;
        [SerializeField] private float _floatSpeed;
        [SerializeField] private float _floatRate;

        [Space]
        [SerializeField] private bool _isScaling = false;
        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;
        [SerializeField] private float _scaleSpeed;
        [SerializeField] private float _scaleRate;

        private bool _goingUp = true;
        private float _floatTimer;
        private bool _scalingUp = true;
        private float _scaleTimer;

        private void Update()
        {
            if (_isAnimated)
            {
                if (_isRotating)
                {
                    transform.Rotate(_rotationAngle * _rotationSpeed * Time.deltaTime);
                }

                if (_isFloating)
                {
                    _floatTimer += Time.deltaTime;
                    Vector3 moveDir = new Vector3(0.0f, 0.0f, _floatSpeed);
                    transform.Translate(moveDir);

                    if (_goingUp && _floatTimer >= _floatRate)
                    {
                        _goingUp = false;
                        _floatTimer = 0;
                        _floatSpeed = -_floatSpeed;
                    }
                    else if (!_goingUp && _floatTimer >= _floatRate)
                    {
                        _goingUp = true;
                        _floatTimer = 0;
                        _floatSpeed = +_floatSpeed;
                    }
                }

                if (_isScaling)
                {
                    _scaleTimer += Time.deltaTime;

                    if (_scalingUp)
                        transform.localScale = Vector3.Lerp(transform.localScale, _endScale, _scaleSpeed * Time.deltaTime);
                    else if (!_scalingUp)
                        transform.localScale = Vector3.Lerp(transform.localScale, _startScale, _scaleSpeed * Time.deltaTime);

                    if (_scaleTimer >= _scaleRate)
                    {
                        if (_scalingUp)
                            _scalingUp = false;
                        else if (!_scalingUp)
                            _scalingUp = true;

                        _scaleTimer = 0;
                    }
                }
            }
        }
    }
}
