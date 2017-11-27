using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlFunctions
{
    BasicMoverByRotate basicMoverByRot;
    AngleChanger angleChanger;
    AttackCubeShoter attCubeShoter;
    LineDrawerWithHull lnDrWHull;
    SetMeshTo2Vecs stMeshTo2Vecs;
    public void DisableAll()
    {
        basicMoverByRot.enabled = false;
        angleChanger.enabled = false;
        attCubeShoter.enabled = false;
        lnDrWHull.enabled = false;
        stMeshTo2Vecs.enabled = false;

    }

    public void EnableAll()
    {
        basicMoverByRot.enabled = true;
        angleChanger.enabled = true;
        attCubeShoter.enabled = true;
        lnDrWHull.enabled = true;
        stMeshTo2Vecs.enabled = true;
    }

    public ControlFunctions()
    {

    }
    public void SetAllFunc(BasicMoverByRotate basicMoverByRot,
                AngleChanger angleChanger,
                AttackCubeShoter attCubeShoter,
                 LineDrawerWithHull lnDrWHull,
                 SetMeshTo2Vecs stMeshTo2Vecs)
    {
        this.basicMoverByRot = basicMoverByRot;
        this.angleChanger = angleChanger;
        this.attCubeShoter = attCubeShoter;
        this.lnDrWHull = lnDrWHull;
        this.stMeshTo2Vecs = stMeshTo2Vecs;
    }
}




public class ControlNavigator : MonoBehaviour
{

    public List<Transform> lstOfBobots;
    List<ControlFunctions> lstOfControlFnc;
    int idxOfCurrentBobot;
    public Transform cameraTr;

    CameraTarget camTar;

    private void ChangeCameraTarget()
    {

    }

    public void ChangeBobot()
    {
        lstOfControlFnc[idxOfCurrentBobot%lstOfBobots.Count].DisableAll();
        idxOfCurrentBobot++;
        camTar.trg = lstOfBobots[idxOfCurrentBobot % lstOfBobots.Count];
        lstOfControlFnc[idxOfCurrentBobot % lstOfBobots.Count].EnableAll();


    }

    private void Awake()
    {
        camTar = cameraTr.GetComponent<CameraTarget>();
        if(camTar == null) { Debug.LogWarning("cameraTargetSc is null"); return; }
        idxOfCurrentBobot = 0;
    }
    // Use this for initialization
    void Start ()
    {
        lstOfControlFnc = new List<ControlFunctions>();
        BasicMoverByRotate basicMoverByRotTmp;
        AngleChanger angleChangerTmp;
        AttackCubeShoter attCubeShoterTmp;
        LineDrawerWithHull lnDrWHullTmp;
        SetMeshTo2Vecs stMeshTo2VecsTmp;
        Transform currTr;

        for(int i=0;i<lstOfBobots.Count;i++)
        {
            lstOfControlFnc.Add(new ControlFunctions());
        }

        for (int i=0;i<lstOfBobots.Count;i++)
        {
           // lstOfControlFnc[i] = new ControlFunctions();
            currTr = lstOfBobots[i];
            basicMoverByRotTmp = currTr.GetComponent<BasicMoverByRotate>();
            if(basicMoverByRotTmp == null) { Debug.LogWarning("someCompOf" + currTr.name + "is null"); return; }
            currTr = currTr.GetChild(0);
            angleChangerTmp = currTr.GetComponent<AngleChanger>();
            if (angleChangerTmp == null) { Debug.LogWarning("someCompOf" + currTr.name + "is null"); return; }
            attCubeShoterTmp = currTr.GetComponent<AttackCubeShoter>();
            if (attCubeShoterTmp == null) { Debug.LogWarning("someCompOf" + currTr.name + "is null"); return; }
            currTr = currTr.GetChild(1);
            lnDrWHullTmp = currTr.GetComponent<LineDrawerWithHull>();
            if (lnDrWHullTmp == null) { Debug.LogWarning("someCompOf" + currTr.name + "is null"); return; }
            stMeshTo2VecsTmp = currTr.GetComponent<SetMeshTo2Vecs>();
            if (stMeshTo2VecsTmp == null) { Debug.LogWarning("someCompOf" + currTr.name + "is null"); return; }

            lstOfControlFnc[i].SetAllFunc(basicMoverByRotTmp,
                angleChangerTmp,
                attCubeShoterTmp,
                lnDrWHullTmp,
                stMeshTo2VecsTmp);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
