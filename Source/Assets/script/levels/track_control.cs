using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class track_control : MonoBehaviour
{
    public GameObject track_element;
    public GameObject energybar;
    public Transform start;
    public Transform end;
    public float maxLength = 100;
    private GameObject track_element_new;
    private Vector3 begin_pos;
    private Vector3 end_pos;
    private Vector3 temp_pos;
    private float totalLength;
    private bool first = true;
    // Use this for initialization
    void Start()
    {
        totalLength = 0;
        Debug.Log("your maxLength now is " + maxLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            temp_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp_pos.z = 0;
            if (first == true)
            {
                Debug.Log("begin_pos = null");
                begin_pos = temp_pos;
                first = false;
            }
            else if ((Vector3.Magnitude(temp_pos - begin_pos) >= 0.1) && (totalLength < maxLength))//防止在一点重复实例化&&未超出长度限制
            {
                end_pos = temp_pos;
                float tempLength = Vector3.Magnitude(end_pos - begin_pos);
                if (totalLength + tempLength > maxLength)//若在本次超出长度限制
                {
                    end_pos = begin_pos + (end_pos - begin_pos) * (maxLength - totalLength) / tempLength;//调整end_pos，进行截取
                    totalLength = maxLength;
                }
                else
                {
                    totalLength += tempLength;
                }
                Vector3 new_pos = (begin_pos + end_pos) / 2;
                Vector3 new_scale = new Vector3(Vector3.Magnitude(end_pos - begin_pos), 1, 1);
                float new_degree = 0;
                if (begin_pos.y > end_pos.y)
                {//终点在起点下方
                    new_degree = -Vector3.Angle(Vector3.right, end_pos - begin_pos);//因为Angle方法只能返回0到180度的角
                }
                else
                {//终点在起点上方
                    new_degree = Vector3.Angle(Vector3.right, end_pos - begin_pos);//因为Angle方法只能返回0到180度的角
                }
                track_element_new = (GameObject)Instantiate(track_element, new_pos, Quaternion.identity);//实例化
                track_element_new.transform.localScale = new_scale;
                track_element_new.transform.Rotate(Vector3.forward, new_degree);
                begin_pos = end_pos;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            totalLength = maxLength;
            first = true;
        }
        draw_energybar();//每一帧都更新energybar

    }

    void OnGUI()
    {
        //GUI.Label(new Rect(300, 100, 50, 50), "" + (int)(maxLength - totalLength));
    }

    void clear()
    {
        GameObject[] track_element_array = GameObject.FindGameObjectsWithTag("track");
        for (int i = 0; i < track_element_array.Length; i++)
        {
            Destroy(track_element_array[i]);
        }
        totalLength = 0;
    }

    void draw_energybar()
    {
        Vector3 start_temp = start.position;
        Vector3 end_temp = start.position + (end.position - start.position) * (maxLength - totalLength) / maxLength;
        Vector3 new_pos = (start_temp + end_temp) / 2;
        energybar.transform.position = new_pos;
        energybar.transform.localScale = new Vector3((end_temp.x - start_temp.x), 1, 1);
    }
}
