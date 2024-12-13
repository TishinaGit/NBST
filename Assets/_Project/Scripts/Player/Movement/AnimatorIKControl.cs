using UnityEngine;

namespace Controller
{
    public class AnimatorIKControl : MonoBehaviour
    {
        [SerializeField] private Camera Camera;
        [SerializeField] private Animator animator;

        [SerializeField] private Transform objTarget;
        [SerializeField] private float headWeight;
        [SerializeField] private float bodyWeight;
        private float _horizontalClamp = 100f;

        private GameObject _objPivot;


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

            float camRotY = Camera.transform.eulerAngles.y;

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
                if (objTarget != null)
                {
                    animator.SetLookAtPosition(objTarget.position);
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
