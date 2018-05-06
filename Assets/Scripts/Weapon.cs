using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Ray ray;
    private RaycastHit raycastHit;
    private Transform m_trans;
    private Transform m_Point; //枪口位置
    private LineRenderer lineRenderer; //线渲染器
    private AudioSource audioSource;
	private GameManagement gameManagement; //加分

    private bool enableFlag = false; //是否可以控制动起来
    void Start()
    {
        m_trans = gameObject.GetComponent<Transform>();
        m_Point = m_trans.Find("Point");
        lineRenderer = m_Point.gameObject.GetComponent<LineRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
		gameManagement=GameObject.Find("UI").GetComponent<GameManagement>();
    }

    public void ChangeEnable(bool state)
    {
        enableFlag = state;
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (enableFlag && Physics.Raycast(ray, out raycastHit))
        {
            m_trans.LookAt(raycastHit.point);
            //枪口到点
            Debug.DrawLine(m_Point.position, raycastHit.point, Color.red);
            //看的见的射线
            lineRenderer.SetPosition(0, m_Point.position);
            lineRenderer.SetPosition(1, raycastHit.point);

            //飞盘射击
            if (raycastHit.collider.tag == "FlyDisk" && Input.GetMouseButtonDown(0))
            {
                audioSource.Play();
                //通过碰撞到的子物体找打父物体
                Transform parent = raycastHit.collider.gameObject.GetComponent<Transform>().parent;
                //通过父物体查找到所有的18个子物体的transform组件
                Transform[] patials = parent.GetComponentsInChildren<Transform>();
                //给每一个碎片添加rigidbody组建，模拟破碎下落效果
                foreach (Transform item in patials)
                {
                    item.gameObject.AddComponent<Rigidbody>();
                }
				gameManagement.AddScore();
                //打完后需要销毁该物体
                GameObject.Destroy(parent.gameObject, 2.0f);
            }
        }
    }
}
