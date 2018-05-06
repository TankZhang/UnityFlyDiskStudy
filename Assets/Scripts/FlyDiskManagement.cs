using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDiskManagement : MonoBehaviour
{

    public GameObject prefab_flydisk;
	private Transform m_transform;
    // Use this for initialization
    void Start()
    {
		m_transform=gameObject.GetComponent<Transform>();
    }

	public void StartCreateFlydisk()
	{
        InvokeRepeating("CreateFlydisk", 0f, 1.0f);
	}
	public void StopCreateFlydisk()
	{
        CancelInvoke("CreateFlydisk");		
	}

	public void RemoveFlydisk()
	{
		Transform[] toRemove=m_transform.GetComponentsInChildren<Transform>();
		bool flag=true;
		foreach	(Transform item in toRemove){
			//第一个不能清除掉，要不然会将整个FlyDiskParent清除掉而报错
			if(flag)
				{flag=false;continue;}
			GameObject.Destroy(item.gameObject);
		}
	}
    void CreateFlydisk()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(1.0f, 4.0f), Random.Range(4.0f, 8.0f));
           GameObject go = GameObject.Instantiate(prefab_flydisk, pos, Quaternion.identity);
		   GameObject.Destroy(go,3.0f);
		   //给生成的飞盘设置父物体
		   go.GetComponent<Transform>().SetParent(m_transform);
        }
    }
}
