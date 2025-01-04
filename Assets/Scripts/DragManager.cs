using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DragManager : MonoBehaviour
{
    //用于实现可拖拽物体的获取
    RaycastHit hit;
    Ray ray;
    Transform dragObj,oldObj;
    public LayerMask mask;
    //用于实现物体拖拽
    bool isDrag;
    Vector3 mousePos,dragObjScreenPos;
    //用于实现物体拼接时的吸附
    Dictionary<Transform, Transform> em;
    public Transform finalEm,emParts;
    //控制吸附距离
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
        
        //左键按下发射射线检测是否有可拖拽物体
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("DragObj"))
        {
            //指向新物体时关闭旧物体的描边
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
        else//指空时关闭旧物体描边
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

    //靠近最终目标自动吸附
    void IsFinished()
    {
        if(Vector3.Distance(dragObj.position, em[dragObj].position) < dis)
        {
            //固定位置，更改标签，底板失活，关闭描边
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
