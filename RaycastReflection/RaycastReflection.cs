using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    //本物体的Transform
    private Transform trans;
    //添加的line renderer组件
    private LineRenderer lineRender;
    //一条射线
    private Ray ray;
    //一个RaycastHit类型的变量, 可以得到射线碰撞点的信息
    private RaycastHit hit;
    //折射的方向
    private Vector3 inDirection;
    //折射次数
    public int reflectionNum = 2;
    void Awake()
    {
        //初始化组件
        trans = this.GetComponent<Transform>();
        lineRender = this.GetComponent<LineRenderer>();
    }
    void Update()
    {
        ray = new Ray(trans.position, trans.forward);
        Debug.DrawRay(trans.position, trans.forward * 100, Color.green);
        Vector3 dir;
        lineRender.positionCount=1;
        lineRender.SetPosition(0, trans.position);
        for (int i = 0; i < reflectionNum; ++i)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
            {
                Debug.DrawRay(hit.point, hit.normal * 3, Color.red);
                dir = Vector3.Reflect(ray.direction, hit.normal);
                ray = new Ray(hit.point, dir);
                Debug.DrawRay(hit.point, dir * 100, Color.green);
                lineRender.positionCount = i + 2;
                lineRender.SetPosition(i + 1, hit.point);
            }
        }
    }
}