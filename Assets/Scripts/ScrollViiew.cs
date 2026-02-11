using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarouselSnap : MonoBehaviour, IEndDragHandler
{
    [Header("Scroll View Settings")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public float snapSpeed = 10f;

    [Header("Optional Scaling Effect")]
    public float centerScale = 1.2f;  // scale for center image
    public float normalScale = 1f;    // scale for other images

    private RectTransform[] items;
    private int targetIndex = 0;
    private bool isSnapping = false;

    void Start()
    {
        // Automatically get all children inside Content
        int childCount = content.childCount;
        items = new RectTransform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            items[i] = content.GetChild(i).GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (isSnapping)
        {
            Vector2 targetPos = new Vector2(-items[targetIndex].anchoredPosition.x, content.anchoredPosition.y);
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPos, Time.deltaTime * snapSpeed);

            if (Vector2.Distance(content.anchoredPosition, targetPos) < 0.1f)
            {
                content.anchoredPosition = targetPos;
                isSnapping = false;
            }
        }

        // Optional: scale effect for center image
        for (int i = 0; i < items.Length; i++)
        {
            if (i == targetIndex)
                items[i].localScale = Vector3.Lerp(items[i].localScale, Vector3.one * centerScale, Time.deltaTime * snapSpeed);
            else
                items[i].localScale = Vector3.Lerp(items[i].localScale, Vector3.one * normalScale, Time.deltaTime * snapSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Detect swipe direction
        float swipeDelta = eventData.pressPosition.x - eventData.position.x;

        if (swipeDelta > 0) // next
            targetIndex = (targetIndex + 1) % items.Length; // loops back to 0
        else if (swipeDelta < 0) // previous
            targetIndex = (targetIndex - 1 + items.Length) % items.Length; // loops to last

        isSnapping = true;
    }
}
