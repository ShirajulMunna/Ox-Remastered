using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    public RectTransform imageRectTransform; // Reference to the RectTransform of the Image
    public float floatDuration = 2f; // Time it takes to float up or down
    public float floatDistance = 50f; // Distance the image will move up or down

    private void Start()
    {
        // Start the floating animation
        StartFloating();
    }

    void StartFloating()
    {
        // Move the image up and down repeatedly using DOTween
        imageRectTransform.DOMoveY(imageRectTransform.position.y + floatDistance, floatDuration)
            .SetEase(Ease.InOutSine) // Smooth movement
            .SetLoops(-1, LoopType.Yoyo); // Infinite looping (up and down)
    }
}
