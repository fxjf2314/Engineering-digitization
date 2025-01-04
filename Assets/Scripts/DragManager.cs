using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DragManager : MonoBehaviour
{
    //����ʵ�ֿ���ק����Ļ�ȡ
    RaycastHit hit;
    Ray ray;
    Transform dragObj,oldObj;
    public LayerMask mask;
    //����ʵ��������ק
    bool isDrag;
    Vector3 mousePos,dragObjScreenPos;
    //����ʵ������ƴ��ʱ������
    Dictionary<Transform, Transform> em;
    public Transform finalEm,emParts;
    //������������
    [Range(0,1)]
    public float dis;
    Outline outline;

    private void Start()
    {
        mousePos = Input.mousePosition;
        isDrag = false;
        em = new Dictionary<Transform, Transform>();
        for(int i = 0; i < emParts.childCount; i++)
        {
            Transform child = emParts.GetChild(i);
            em.Add(child, finalEm.Find(child.name));
        }
    }

    private void Update()
    {
        
        //������·������߼���Ƿ��п���ק����
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("DragObj"))
        {
            //ָ��������ʱ�رվ���������
            if(oldObj != hit.collider.transform)
            {
                if(outline)outline.enabled = false;
            }
            if(!isDrag)
            {
                outline = hit.collider.GetComponent<Outline>();
                outline.enabled = true;
            }
            
            if(Input.GetMouseButtonDown(0))
            {
                isDrag = true;
                dragObj = hit.collider.transform;
                mousePos = Input.mousePosition;
                dragObjScreenPos = Camera.main.WorldToScreenPoint(dragObj.transform.position);
            }
            oldObj = hit.collider.transform;
        }
        else//ָ��ʱ�رվ��������
        {
            if (outline) outline.enabled = false;
        }
        
        
        if(isDrag && Input.GetMouseButton(0))
        {
            Vector3 newDragObjScreenPos = Input.mousePosition - mousePos + dragObjScreenPos;
            dragObj.position = Camera.main.ScreenToWorldPoint(newDragObjScreenPos);
            IsFinished();
        }
        if(isDrag && Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            dragObj = null;
        }

    }

    //��������Ŀ���Զ�����
    void IsFinished()
    {
        if(Vector3.Distance(dragObj.position, em[dragObj].position) < dis)
        {
            //�̶�λ�ã����ı�ǩ���װ�ʧ��ر����
            dragObj.position = em[dragObj].position;
            dragObj.tag = "Finished";
            em[dragObj].gameObject.SetActive(false);
            Outline outline = dragObj.GetComponent<Outline>();
            outline.enabled = false;
            dragObj = null;
            isDrag = false;
        }
    }

}
