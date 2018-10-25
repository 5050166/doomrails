using UnityEngine;
using UnityEngine.Events;

public abstract class SmartMissile : MonoBehaviour { }

public abstract class SmartMissile<RgbdType, VecType> : SmartMissile
{
	[Header("Missile")]
	[SerializeField, Tooltip("In seconds, 0 for infinite lifetime.")]
	float m_lifeTime = 5;
	[SerializeField]
	UnityEvent m_onNewTargetFound;  //当发现新目标时触发的事件
	[SerializeField]
	UnityEvent m_onTargetLost;  //当目标丢失时触发的事件

	[Header("扫描系统参数设置")]
	
	[SerializeField, Tooltip("设定导弹的搜索范围，范围内将锁定敌人.")]
	protected float m_searchRange = 10f;
	[SerializeField, Range(0, 360)]
	protected int m_searchAngle = 90;
	[SerializeField, Tooltip("如果关闭，敌人如果超出范围将会丢失目标.")]
	bool m_canLooseTarget = true;

	[Header("导航系统参数设置")]
	[SerializeField, Tooltip("设定导弹的导航范围,范围内导弹将追击敌人.")]
	protected float m_guidanceIntensity = 5f;
	[SerializeField, Tooltip("设定导弹追踪时速度根据距离的改变形式,默认线性平滑.")]
	protected AnimationCurve m_distanceInfluence = AnimationCurve.Linear(0, 1, 1, 1);

	[Header("目标参数设置")]
	[SerializeField, Tooltip("修正锁定目标位置.")]
	protected VecType m_targetOffset;
	[SerializeField, HideInInspector]
	protected string m_targetTag = "ball";
	public string TargetTag
	{
		get { return m_targetTag; }
		set { m_targetTag = value; }
	}

	[Header("Debug")]
	[SerializeField, Tooltip("Color of the search zone."), HideInInspector]
	protected Color m_zoneColor = new Color(255, 0, 155, 0.1f);
	[SerializeField, Tooltip("Color of the line to the target."), HideInInspector]
	protected Color m_lineColor = new Color(255, 0, 155, 1);
	[SerializeField, Tooltip("Draw the search zone."), HideInInspector]
	protected bool m_drawSearchZone = false;

	protected RgbdType m_rigidbody;
	protected Transform m_target;
	protected float m_targetDistance;
	protected VecType m_direction;

	void Start()
	{
		m_targetDistance = m_searchRange;

        if (m_lifeTime > 0)
            GameObjectPool.Instance.CloseGameObjectDelay(this.gameObject, m_lifeTime);
			//Destroy(gameObject, m_lifeTime);
	}

	void FixedUpdate()
	{
		if (m_target != null)
		{
			if (m_canLooseTarget && !isWithinRange(m_target.transform.position))
			{
				m_target = null;
				m_targetDistance = m_searchRange;
				m_onTargetLost.Invoke();
			}
			else
			{
				goToTarget();
			}
		}
		else if (m_target = findNewTarget())
			m_onNewTargetFound.Invoke();
	}

	/// <summary>
	/// Find a new target within the search zone. Returns null if no target is found.
	/// </summary>
	protected abstract Transform findNewTarget();
	
	/// <summary>
	/// Returns true if the input Coodinates are within the search zone.
	/// </summary>
	protected abstract bool isWithinRange(Vector3 coordinates);

	/// <summary>
	/// Update the direction of the Rigidbody.
	/// </summary>
	protected abstract void goToTarget();
}