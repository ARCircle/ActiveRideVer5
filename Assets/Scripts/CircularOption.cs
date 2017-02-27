using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CircularOption : UIBehaviour, ILayoutGroup
{
    public float radius = 800;
    public float offsetAngle;

    protected override void OnValidate()
    {
        base.OnValidate();
        Arrange();
    }

    // 要素数が変わると自動的に呼ばれるコールバック
    #region ILayoutController implementation
    public void SetLayoutHorizontal() { }
    public void SetLayoutVertical()
    {
        Arrange();
    }
    #endregion

    void Arrange()
    {
        float splitAngle = 360 / transform.childCount;

        for (int elementId = 0; elementId < transform.childCount; elementId++)
        {
            var child = transform.GetChild(elementId) as RectTransform;
            float currentAngle = splitAngle * elementId + offsetAngle;
            child.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }
}