//��ק�߼�
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

    private int completedCount = 0;
    private int currentIndex = 0; 

    public GameObject completionText; 
    public NewObjectController newObjectController; 

    private List<Transform> dragObjects = new List<Transform>();
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>(); 

    private void Start()
    {
        initialCameraRotation = Camera.main.transform.rotation;
        initialCameraPosition = Camera.main.transform.position;

        isDrag = false;
        em = new Dictionary<Transform, Transform>();

        // ��ʼ�����϶������б�ͳ�ʼλ��
        for (int i = 0; i < emParts.childCount; i++)
        {
            Transform child = emParts.GetChild(i);
            em.Add(child, finalEm.Find(child.name));
            dragObjects.Add(child);
            initialPositions.Add(child, child.position); // �洢��ʼλ��
        }

        Debug.Log("em.Count: " + em.Count); // ������־

        if (completionText != null)
        {
            completionText.SetActive(false);
        }

        // ���������ʼ״̬Ϊ��ʾ
        foreach (Transform obj in dragObjects)
        {
            obj.gameObject.SetActive(true);
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
                isDrag = true;
                dragObj = hit.collider.transform;

                Vector3 mouseWorldPos = GetMouseWorldPosition();
                offset = dragObj.position - mouseWorldPos;
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
        // ��鵱ǰ�϶��������Ƿ��ǵ�ǰ˳�������
        if (dragObj == dragObjects[currentIndex])
        {
            if (Vector3.Distance(dragObj.position, em[dragObj].position) < dis)
            {
                dragObj.position = em[dragObj].position;
                dragObj.tag = "Finished";
                em[dragObj].gameObject.SetActive(false);
                Outline outline = dragObj.GetComponent<Outline>();
                outline.enabled = false;
                dragObj = null;
                isDrag = false;

                completedCount++;

                if (currentIndex + 1 < dragObjects.Count)
                {
                    currentIndex++;
                }

                if (completedCount == em.Count)
                {
                    ShowCompletionMessage();
                }
            }
        }
    }

    private void ShowCompletionMessage()
    {
        // ��������ƴ����ɵ�����
        foreach (Transform obj in dragObjects)
        {
            obj.gameObject.SetActive(false);
        }

        // ��ʾ������
        if (newObjectController != null)
        {
            newObjectController.Show();
        }

        // ��ʾ�����ʾ
        if (completionText != null)
        {
            completionText.SetActive(true);
            StartCoroutine(HideCompletionMessageAfterDelay(3f));
        }
    }

    private IEnumerator HideCompletionMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        completionText.SetActive(false);
    }

    
    public void ResetAllObjects()
    {

        completedCount = 0;
        currentIndex = 0;

        foreach (Transform obj in dragObjects)
        {
            obj.position = initialPositions[obj]; 
            obj.tag = "DragObj"; 
            obj.gameObject.SetActive(true); 

            Outline outline = obj.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        foreach (var pair in em)
        {
            pair.Value.gameObject.SetActive(true);
        }

        if (completionText != null)
        {
            completionText.SetActive(false);
        }

        if (newObjectController != null)
        {
            newObjectController.Hide();
        }
    }
}