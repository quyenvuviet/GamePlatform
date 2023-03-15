using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector2 focusAreaSize;
    public float vericallOffSet;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticallSmoothTime;

    FocusArea focsArea;
    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheaDirX;
    float smoothLookVelocityX;
    float smoothCVelocityY;
    bool lookAheadStopped;
    private void Start()
    {
        focsArea = new FocusArea(target.GetComponent<Collider2D>().bounds, focusAreaSize);
    }
    private void LateUpdate()
    {
        focsArea.update(target.GetComponent<Collider2D>().bounds);
        Vector2 focusPos = focsArea.centre + Vector2.up * vericallOffSet;
        if (focsArea.velocity.x != 0)
        {
            lookAheaDirX = Mathf.Sign(focsArea.velocity.x);
            if (Mathf.Sign(target.transform.position.x) == Mathf.Sign(focsArea.velocity.x) && target.transform.position.x != 0)
            {
                lookAheadStopped = false;
                targetLookAheadX = lookAheaDirX * lookAheadDstX;

            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheaDirX * lookAheadDstX - currentLookAheadX) / 4f;

                }
            }

        }
        targetLookAheadX = lookAheaDirX + lookAheadDstX;
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
        focusPos.y = Mathf.SmoothDamp(transform.position.y, focusPos.y, ref smoothCVelocityY, verticallSmoothTime);
        focusPos += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPos + Vector3.forward * -10f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        Gizmos.DrawCube(focsArea.centre, focusAreaSize);
    }
    struct FocusArea
    {
        public Vector2 centre;
        float left, right;
        float top, bottom;
        public Vector2 velocity;

        public FocusArea(Bounds targetBound, Vector2 size)
        {
            left = targetBound.center.x - size.x / 2;
            right = targetBound.center.x + size.x / 2;
            bottom = targetBound.min.y;
            top = targetBound.min.y + size.y;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = Vector2.zero;
        }
        public void update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;

            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;
            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;

            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
