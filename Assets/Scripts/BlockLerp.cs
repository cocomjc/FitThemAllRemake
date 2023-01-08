using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLerp : MonoBehaviour
{
    [SerializeField] private BlockParam blockParam;
    [SerializeField] private float scaleLerpTime = .2f;
    [SerializeField] private float posLerpTime = .2f;
    private bool isScaleDecreasing = true;
    private float currentScaleLerpTime;
    private bool posMoving = false;
    private Vector2 destination;
    private float currentPosLerpTime;

    public void ToggleScale(bool isDecreasing)
    {
        isScaleDecreasing = isDecreasing;
        currentScaleLerpTime = 0;
    }
    public void SetPos(Vector2 _destination)
    {
        destination = _destination;
        currentPosLerpTime = 0;
        posMoving = true;
    }

    private void Update()
    {
        // Scale Lerp
        currentScaleLerpTime += Time.deltaTime;
        if (currentScaleLerpTime > scaleLerpTime)
        {
            currentScaleLerpTime = scaleLerpTime;
        }
        float tScale = currentScaleLerpTime / scaleLerpTime;
        transform.localScale = Vector3.Lerp(isScaleDecreasing ? blockParam.blockNormalScale : blockParam.blockSmallScale, isScaleDecreasing ? blockParam.blockSmallScale : blockParam.blockNormalScale, tScale);

        // Position Lerp
        if (!posMoving) return;
        currentPosLerpTime += Time.deltaTime;
        if (currentPosLerpTime > posLerpTime)
        {
            currentPosLerpTime = posLerpTime;
        }
        float tPos = currentPosLerpTime / posLerpTime;
        transform.position = Vector3.Lerp(transform.position, destination, tPos);
        posMoving = !(currentPosLerpTime == posLerpTime);
    }
}
