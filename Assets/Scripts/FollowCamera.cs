using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform target;
    public float smoothSpeed = 5f;

    [Header("오프셋")]
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    [Header("카메라 범위 제한")]
    public bool useBounds = false;
    public float minX, maxX, minY, maxY;

    void FixedUpdate()
    {
        if (target == null) return;

        // 목표 위치 계산
        Vector3 targetPosition = target.position + offset;

        // 범위 제한 적용
        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        // 부드러운 추적
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}