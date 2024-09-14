using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _matBlock;

    private Camera _mainCamera;
    private float _currentValue = 1;
    private float _targetValue = 1;
    private float _lerp;


    private void Start()
    {
        _matBlock = new MaterialPropertyBlock();
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        AlignCamera();

        if(_currentValue != _targetValue)
        {
            _lerp += Time.deltaTime / 2f;
            _meshRenderer.GetPropertyBlock(_matBlock);
            _matBlock.SetFloat("_Fill", _currentValue);
            _meshRenderer.SetPropertyBlock(_matBlock);

            _currentValue = Mathf.Lerp(_currentValue, _targetValue, _lerp);
        }


    }


    public void SetValueLocal(float value)
    {
        _lerp = 0;
        _targetValue = value;

        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat("_Fill", _currentValue);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }

    public void SetValueFromRpc(float value)
    {
        _lerp = 0;
        _targetValue = value;
    }

    private void AlignCamera()
    {
        if(_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        if (_mainCamera != null)
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }
}
