using UnityEngine;

public class EnemyClickHandler : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var enemy = hit.collider.GetComponent<IEnemy>();
                enemy?.OnClick();
            }
        }
    }
}
