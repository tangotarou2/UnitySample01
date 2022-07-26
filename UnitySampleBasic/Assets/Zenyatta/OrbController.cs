using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour {

	public int orbNum = 8;
	private List<GameObject> orbs = new List<GameObject>();

	public float orbRotVel_T0   = 0.0f;
	public float orbHeight_T0   = 2.25f;
	public float orbRadius_T0   = 0.25f;
	public float orbPit_T0      = 0.0f;
	public float orbRotVel_T1   = 240.0f;
	public float orbHeight_T1   = 1.0f;
	public float orbRadius_T1   = 2.0f;
	public float orbPit_T1      = 20.0f;
	public float orbKf          = 0.2f;
	public float orbMotionCycle = 2.0f;

	private int   orbParamIdx;
	private float orbRot;
	private float orbRotVel;
	private float orbHeight;
	private float orbRadius;
	private float orbPit;
	private float orbMotionSec;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < orbNum; ++i) {
			GameObject prefab = (GameObject)Resources.Load ("Prefabs/Orb");
			GameObject orb = Instantiate (prefab, Vector3.zero, Quaternion.identity);
			orbs.Add (orb);
		}

		orbParamIdx  = 0;
		orbRot       = 0.0f;
		orbRotVel    = orbRotVel_T0;
		orbHeight    = orbHeight_T0;
		orbRadius    = orbRadius_T0;
		orbPit       = orbPit_T0;
		orbMotionSec = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// モーション周期
		orbMotionSec += Time.deltaTime;
		if (orbMotionSec >= orbMotionCycle) {
			orbMotionSec = 0.0f;
			orbParamIdx = ( orbParamIdx + 1 ) % 2;
		}

		// パラメータ更新
		{
			orbRotVel = Mathf.Lerp (orbRotVel, orbParamIdx == 0 ? orbRotVel_T0 : orbRotVel_T1, orbKf);
			orbHeight = Mathf.Lerp (orbHeight, orbParamIdx == 0 ? orbHeight_T0 : orbHeight_T1, orbKf);
			orbRadius = Mathf.Lerp (orbRadius, orbParamIdx == 0 ? orbRadius_T0 : orbRadius_T1, orbKf);
			orbPit    = Mathf.Lerp (orbPit,    orbParamIdx == 0 ? orbPit_T0    : orbPit_T1,    orbKf);
		}

		// オーブ位置計算
		{
			// オーブ座標軸
			Vector3 orbForward = Quaternion.AngleAxis (orbPit, transform.right) * transform.forward;
			Vector3 orbUp      = Quaternion.AngleAxis (orbPit, transform.right) * transform.up;

			// 回転
			orbRot += orbRotVel * Time.deltaTime;
			orbForward = Quaternion.AngleAxis (orbRot, orbUp) * orbForward;

			for (int i = 0; i < orbNum; ++i) {
				Vector3 pos = transform.position + transform.up * orbHeight + orbForward * orbRadius;
				orbs [i].transform.position = pos;

				orbForward = Quaternion.AngleAxis (360.0f / orbNum, orbUp) * orbForward;
			}
		}
	}
}
