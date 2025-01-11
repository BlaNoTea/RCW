using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaMoving : MonoBehaviour
{
    public RectTransform parentRect;
    public RectTransform[] imagesRect;
    public float speed = 1000f;
    public float bounceAngleVariation = 50f;

    private void Awake()
    {
        float initialAngle;
        Vector2 direction;

        for(int i = 0; i < imagesRect.Length; i++){
            initialAngle = Random.Range(0, 360f);
            direction = new Vector2(Mathf.Cos(initialAngle * Mathf.Deg2Rad), Mathf.Sin(initialAngle * Mathf.Deg2Rad)).normalized;
            StartCoroutine(MoveImage(imagesRect[i], direction));
        }
    }

    IEnumerator MoveImage(RectTransform imageRect, Vector2 direction)
    {
        while (true)
        {
            Vector2 movement = direction * speed * Time.deltaTime;
            imageRect.anchoredPosition += movement;

            Vector2 pos = imageRect.anchoredPosition;
            Vector2 size = imageRect.sizeDelta / 2;
            Vector2 parentSize = parentRect.rect.size / 2;

            bool bounced = false;

            if (pos.x - size.x < -parentSize.x || pos.x + size.x > parentSize.x)
            {
                direction.x = -direction.x;
                bounced = true;
                pos.x = Mathf.Clamp(pos.x, -parentSize.x + size.x, parentSize.x - size.x);
            }

            if (pos.y - size.y < -parentSize.y || pos.y + size.y > parentSize.y)
            {
                direction.y = -direction.y;
                bounced = true;
                pos.y = Mathf.Clamp(pos.y, -parentSize.y + size.y, parentSize.y - size.y);
            }

            if (bounced)
            {
                direction = AdjustDirectionWithVariation(direction);
            }

            imageRect.anchoredPosition = pos;

            yield return null;
        }
    }

    private Vector2 AdjustDirectionWithVariation(Vector2 direction)
    {
        float angleVariation = Random.Range(-bounceAngleVariation, bounceAngleVariation);
        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float newAngle = currentAngle + angleVariation;

        return new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad)).normalized;
    }
}
