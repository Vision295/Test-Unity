using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform origin;
    bool hasClickedOn;
    void Awake()
    {
        lineRenderer.SetPosition(0, origin.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasClickedOn)
        {
            lineRenderer.SetPosition(1, Input.mousePosition);
        } else {
            lineRenderer.SetPosition(1, origin.position);
        }
    }

    public void OnClick()
    {
        hasClickedOn = !hasClickedOn;
    }
}
