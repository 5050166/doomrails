// Custom_Physics
using UnityEngine;

public class Custom_Physics : MonoBehaviour
{
    public static float Galileo(float time)
    {
        float num = 7.06999969f;
        return 2f * num / Mathf.Pow(time, 2f);// Mathf.Pow(x,y)     ָ����x��y����  ʱ��*ʱ��
    }
}
