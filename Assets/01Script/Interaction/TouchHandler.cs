using UnityEditor;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    void Update()
    {
        if (UIManager.Instance)
            if (UIManager.Instance.touchBlocking) return;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));
            RaycastHit[] hit;

            hit = Physics.RaycastAll(ray, Mathf.Infinity, mask, QueryTriggerInteraction.UseGlobal);

            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider == null) continue;
                if (hit[i].collider.isTrigger) continue;

                if (hit[i].collider.TryGetComponent<IInteract>(out var interact))
                    interact.Interaction();
            }
        }



        // ¸ð¹ÙÀÏ¿ë

        //if (Input.touchCount > 0)
        //{
        //    var touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        var ray = Camera.main.ScreenPointToRay(touch.position);
        //        RaycastHit hit;

        //        Physics.Raycast(ray, out hit);
        //        if (hit.collider == null) return;

        //        if (hit.collider.TryGetComponent<IInteract>(out var interact))
        //            interact.Interaction();
        //    }
        //}
    }
}
