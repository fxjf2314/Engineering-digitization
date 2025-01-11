//拖拽逻辑
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    Transform dragObj, oldObj;
    public LayerMask mask;

    bool isDrag;
    Vector3 offset;

    Dictionary<Transform, Transform> em;
    public Transform finalEm, emParts;

    [Range(0, 1)]
    public float dis;

    Outline outline;

    private Quaternion initialCameraRotation;
    private Vector3 initialCameraPosition;

    private int completedCount = 0; // 计数器
    private int currentIndex = 0; // 当前可拖动的物体索引

    public GameObject completionText; // UI 提示元素

    private List<Transform> dragObjects = new List<Transform>(); // 所有可拖动物体的列表

    private void Start()
    {
        initialCameraRotation = Camera.main.transform.rotation;
        initialCameraPosition = Camera.main.transform.position;

        isDrag = false;
        em = new Dictionary<Transform, Transform>();

        // 初始化可拖动物体列表
        for (int i = 0; i < emParts.childCount; i++)
        {
            Transform child = emParts.GetChild(i);
            em.Add(child, finalEm.Find(child.name));
            dragObjects.Add(child);
        }

        Debug.Log("em.Count: " + em.Count); // 调试日志

        if (completionText != null)
        {
            completionText.SetActive(false);
        }

        // 禁用所有可拖动物体，除了第一个
        for (int i = 1; i < dragObjects.Count; i++)
        {
            dragObjects[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("DragObj"))
        {
            if (oldObj != hit.collider.transform)
            {
                if (outline) outline.enabled = false;
            }
            if (!isDrag)
            {
                outline = hit.collider.GetComponent<Outline>();
                outline.enabled = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 检查当前拖动的物体是否是当前可拖动的物体
                if (hit.collider.transform == dragObjects[currentIndex])
                {
                    isDrag = true;
                    dragObj = hit.collider.transform;

                    Vector3 mouseWorldPos = GetMouseWorldPosition();
                    offset = dragObj.position - mouseWorldPos;
                }
            }
            oldObj = hit.collider.transform;
        }
        else
        {
            if (outline) outline.enabled = false;
        }

        if (isDrag && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            dragObj.position = mouseWorldPos + offset;

            IsFinished();
        }

        if (isDrag && Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            dragObj = null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Quaternion currentCameraRotation = Camera.main.transform.rotation;
        Vector3 currentCameraPosition = Camera.main.transform.position;

        Camera.main.transform.rotation = initialCameraRotation;
        Camera.main.transform.position = initialCameraPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(initialCameraRotation * Vector3.forward, dragObj.position);

        if (plane.Raycast(ray, out float distance))
        {
            Camera.main.transform.rotation = currentCameraRotation;
            Camera.main.transform.position = currentCameraPosition;

            return ray.GetPoint(distance);
        }

        Camera.main.transform.rotation = currentCameraRotation;
        Camera.main.transform.position = currentCameraPosition;

        return Vector3.zero;
    }

    void IsFinished()
    {
        if (Vector3.Distance(dragObj.position, em[dragObj].position) < dis)
        {
            Debug.Log("物体已吸附"); // 调试日志

            dragObj.position = em[dragObj].position;
            dragObj.tag = "Finished";
            em[dragObj].gameObject.SetActive(false);
            Outline outline = dragObj.GetComponent<Outline>();
            outline.enabled = false;
            dragObj = null;
            isDrag = false;

            completedCount++;
            Debug.Log("completedCount: " + completedCount); // 调试日志

            // 启用下一个可拖动物体
            if (currentIndex + 1 < dragObjects.Count)
            {
                currentIndex++;
                dragObjects[currentIndex].gameObject.SetActive(true);
            }

            if (completedCount == em.Count)
            {
                Debug.Log("所有物体已吸附"); // 调试日志
                ShowCompletionMessage();
            }
        }
    }

    private void ShowCompletionMessage()
    {
        Debug.Log("ShowCompletionMessage 被调用"); // 调试日志
        if (completionText != null)
        {
            completionText.SetActive(true);
            StartCoroutine(HideCompletionMessageAfterDelay(3f));
        }
    }

    private IEnumerator HideCompletionMessageAfterDelay(float delay)
    {
        Debug.Log("协程 HideCompletionMessageAfterDelay 被调用"); // 调试日志
        yield return new WaitForSeconds(delay);
        completionText.SetActive(false);
    }
}