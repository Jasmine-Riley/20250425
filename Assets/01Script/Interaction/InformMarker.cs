using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformMarker : MonoBehaviour
{
    private LineRenderer line;
    public Material lineMaterial;
    public GameObject uiPrefab;
    public GameObject iconPrefab;
    public Vector3 offset;
    private GameObject canvasUI;
    private GameObject iconUI;

    [SerializeField] private string title;
    [SerializeField] private string touchText;

    private void Awake()
    {
        if (!uiPrefab) return;


        canvasUI = Instantiate(uiPrefab, transform);
        canvasUI.transform.localPosition = offset;
        canvasUI.SetActive(false);


        UIClicker iconClicker = null;

        if (iconPrefab)
        {
            iconUI = Instantiate(iconPrefab, transform);
            iconUI.SetActive(false);
            iconClicker = iconUI.GetComponentInChildren<UIClicker>();
        }

        var tmps = canvasUI.GetComponentsInChildren<TextMeshProUGUI>();

        if (tmps.Length > 1)
        {
            tmps[0].text = title;
            tmps[1].text = touchText;
        }

        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;

        line.positionCount = 3;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        line.material = lineMaterial;

        var size = Vector3.zero;
        size.x = canvasUI.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

        var posOffset = new Vector3(0, 0, -0.05f);

        if (TryGetComponent<InteractionObject>(out var interact))
        {
            if(iconClicker) iconClicker.OnClick += interact.Interaction;

            interact.OnInteractable += (tf) =>
            {
                canvasUI.SetActive(tf);

                line.SetPosition(0, transform.position);
                line.SetPosition(1, transform.position + offset - (size * 0.5f) + posOffset);
                line.SetPosition(2, transform.position + offset + (size * 0.5f) + posOffset);


                if (iconUI)
                {
                    iconUI.SetActive(tf);
                    line.SetPosition(0, transform.position + Vector3.right * iconUI.transform.GetChild(0).transform.localScale.x);
                }


                line.enabled = tf;
            };
        }
    }
}
