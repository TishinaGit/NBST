using UnityEngine;
using Cinemachine;
using Zenject;

public class CreationPortal : MonoBehaviour
{ 
    [Header("Placement Parameters")]
    [SerializeField] private GameObject _placeableObjectPrefab;
    [SerializeField] private GameObject _previewObjectPrefab; 
    [SerializeField] private LayerMask _placementSurfaceLayerMask;

    [Header("Preview Material")]
    [SerializeField] private Material _previewMaterial;
    [SerializeField] private Color _validColor;
    [SerializeField] private Color _invalidColor;

    [Header("Raycast Parameters")]
    [SerializeField] private float _objectDistanceFromPlayer;
    [SerializeField] private float _raycastStartVerticalOffset;
    [SerializeField] private float _raycastDistance;

    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    private bool _inPlacementMode = false;
    private bool _validPreviewState = false;

    private CinemachineFreeLook _playerCamera;

    [Inject]
    public void Construct(CinemachineFreeLook CinemachineFreeLook)
    {
        _playerCamera = CinemachineFreeLook;
    }
     
    private void Update()
    {
        UpdateInput();

        if (_inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
                SetValidPreviewState();
            else
                SetInvalidPreviewState();
        }
    }

    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = _playerCamera.transform.position + (cameraForward * _objectDistanceFromPlayer);
        startPos.y += _raycastStartVerticalOffset;

        RaycastHit hitInfo;
        if (Physics.Raycast(startPos, Vector3.down, out hitInfo, _raycastDistance, _placementSurfaceLayerMask))
        {
            _currentPlacementPosition = hitInfo.point;
        }

        Quaternion rotation = Quaternion.Euler(0f, _playerCamera.transform.eulerAngles.y, 0f);
        _previewObject.transform.position = _currentPlacementPosition;
        _previewObject.transform.rotation = rotation;
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnterPlacementMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExitPlacementMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaceObject();
        }
    }

    private void SetValidPreviewState()
    {
        _previewMaterial.color = _validColor;
        _validPreviewState = true;
    }

    private void SetInvalidPreviewState()
    {
        _previewMaterial.color = _invalidColor;
        _validPreviewState = false;
    }

    private bool CanPlaceObject()
    {
        if (_previewObject == null)
            return false;

        return _previewObject.GetComponentInChildren<PreviewGameobject>().IsValid;
    }

    private void PlaceObject()
    {
        if (!_inPlacementMode || !_validPreviewState)
            return;

        Quaternion rotation = Quaternion.Euler(0f, _playerCamera.transform.eulerAngles.y, 0f);
        Instantiate(_placeableObjectPrefab, _currentPlacementPosition, rotation, transform);

        ExitPlacementMode();
    }

    private void EnterPlacementMode()
    {
        if (_inPlacementMode)
            return;


        Quaternion rotation = Quaternion.Euler(0f, _playerCamera.transform.eulerAngles.y, 0f);
        _previewObject = Instantiate(_previewObjectPrefab, _currentPlacementPosition, rotation, transform);
        _inPlacementMode = true;
    }

    private void ExitPlacementMode()
    {

        Destroy(_previewObject);
        _previewObject = null;
        _inPlacementMode = false;
    }
}
