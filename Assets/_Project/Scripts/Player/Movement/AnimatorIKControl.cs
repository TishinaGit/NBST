using UnityEngine;
using Zenject;

namespace Controller
{
    public class AnimatorIKControl : MonoBehaviour
    {
          
        [SerializeField] private Animator animator;
 
        [SerializeField] private float headWeight;
        [SerializeField] private float bodyWeight;
        private float _horizontalClamp = 100f;

        private GameObject _objPivot;

        private Camera _camera;
        private Transform _objTarget;

        [Inject]
        public void Construct(Camera CameraPlayer, GameObject AimTargetForCamera)
        {
            _camera = CameraPlayer;
            _objTarget = AimTargetForCamera.transform;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();

            _objPivot = new GameObject("Pivot");
            _objPivot.transform.parent = transform.parent;
        }


        private void Update()
        {
            Quaternion rot = Quaternion.LookRotation(transform.forward);
            Vector3 euler = rot.eulerAngles;
            float minY = euler.y - _horizontalClamp;
            float maxY = euler.y + _horizontalClamp;

            float camRotY = _camera.transform.eulerAngles.y;

            if (camRotY > maxY || camRotY < minY)
            {
                bodyWeight = Mathf.Lerp(bodyWeight, 0, Time.deltaTime * 6.5f);
                headWeight = Mathf.Lerp(headWeight, 0, Time.deltaTime * 6.5f);
            }
            else
            {
                bodyWeight = Mathf.Lerp(bodyWeight, 1, Time.deltaTime * 6.5f);
                headWeight = Mathf.Lerp(headWeight, 1, Time.deltaTime * 6.5f);
            }
        }

        private void OnAnimatorIK()
        {
            if (animator != null)
            {
                if (_objTarget != null)
                {
                    animator.SetLookAtPosition(_objTarget.position);
                    animator.SetLookAtWeight(1, bodyWeight, headWeight);
                }
                else
                {
                    animator.SetLookAtWeight(0);
                }
            }
        }
    }

}
