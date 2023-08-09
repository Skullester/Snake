using UnityEngine;
using UnityEngine.Serialization;

public class CameraFix : MonoBehaviour
{
    private Transform head;

    [SerializeField, FormerlySerializedAs("desiredPosition")]
    private Transform desiredTransform;

    [SerializeField]
    private Transform empty;

    [SerializeField]
    private Transform empty2;
    private Vector3 startPosition;
    Vector3 vectorZ = new(0f, 0f, -2f);
    private Vector3 startTransfrom;
    private bool isThird;

    private void Awake()
    {
        startTransfrom = desiredTransform.localPosition;
        head = transform.parent;
    }

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    void OnEnable()
    {
        isThird = false;
        empty.transform.parent = transform;
        empty2.transform.parent = transform;
        switch (CameraChanger.CounterCameras)
        {
            case 0:
                desiredTransform.localPosition = startTransfrom;
                empty.transform.localPosition = Vector3.right * 3.5f + vectorZ;
                break;
            case 1:
                desiredTransform.localPosition = startTransfrom;
                empty.transform.localPosition = Vector3.right * -3.5f + vectorZ;
                break;
            case 2:
                isThird = true;
                empty2.localPosition = Vector3.right * 3.5f + vectorZ;
                break;
            case 3:
                desiredTransform.localPosition = transform.localPosition;
                break;
            case 4:
                desiredTransform.localPosition -= Vector3.back * 2f;
                break;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = startPosition;
        if (Physics.Linecast(empty.position, head.position, out var hit))
            if (hit.collider.CompareTag("Walls"))
                transform.position = desiredTransform.position;
        if (Physics.Linecast(empty2.position, head.position, out var hit2) && isThird)
            if (hit2.collider.CompareTag("Walls"))
                transform.position = desiredTransform.position;
    }
}
