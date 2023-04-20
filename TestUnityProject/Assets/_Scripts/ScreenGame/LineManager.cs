using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public RectTransform origin;
    public Transform isSelectingLine;
    void Awake()
    {
        lineRenderer.SetPosition(0, origin.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelectingLine.position == transform.position)
        {
            lineRenderer.SetPosition(1, Input.mousePosition);
        } else {
            lineRenderer.SetPosition(1, origin.position);
        }
    }

    public void OnClick()
    {
        isSelectingLine.position = transform.position;
    }
}
