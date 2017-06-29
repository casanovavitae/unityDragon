using UnityEngine;
using System.Collections;

//When this powerup has been picked up it will first change TimeScale to the WantedTimeScale following the curve of
//BeginCurve, it will stay at the value for BeginLength seconds and will speedup using the curve of EndCurve.
//The total length of the effect will be: BeginLength + EffectLength + EndLength
//Setting WantedTimeScale to > 1 will result in a faster game, < 1 will result in a slowmotion / bullet time effect 
public class ChangeSpeed : PowerUpBase
{
    //When there are multiple slowdown powerups, make sure there can be only one active at at time
    //else it will most likely break 
    private static bool IsEffectAlreadyInProgress = false;

    //TimeScale that will be set when this powerup has been picked up
    public float WantedTimeScale = 0.5F;
    public float EffectLength = 2;  //in seconds

    //To make the effect more interesting, don't use a liniear change but a nice curve
    //You can change these curves in the editor! These really make your game feel more polished.
    public AnimationCurve BeginCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    public float BeginLength = 0.8F;
    public AnimationCurve EndCurve = AnimationCurve.EaseInOut(1, 1, 0, 0);
    public float EndLength = 0.4F;

    //Since we are changing the TimeScale we need to keep track of the deltaTime ourself
    private float deltaTime = 0;
    private float previousTime;
		
    protected override void OnPickup()
    {
        base.OnPickup();

        //Begin the effect
        if (!IsEffectAlreadyInProgress)
        {
            StartCoroutine(StartSlowMotionEffect());
        }
    }

    private IEnumerator StartSlowMotionEffect()
    {
        IsEffectAlreadyInProgress = true;
		
        //First slow down following the SlowDownCurve:
        float time = 0;
        //This is basicly a mini thread, it will loop/stay in this while for the given time
        //and change the TimeScale following the SlowdownCurve
        while (time < BeginLength)
        {
            time += deltaTime;
            float n = BeginCurve.Evaluate(time / BeginLength);
            Time.timeScale = Mathf.Lerp(WantedTimeScale, 1, n);

            yield return null;
        }

        //WaitForSeconds is affected by the change of timescale
        //i.e. TimeScale of 0.5F will make an EffectLength of 3 seconds feel like 6 seconds
        //To counter this we make the EffectLength 1.5F

		time = 0;
		while(time < EffectLength)
		{
			time += deltaTime;
			yield return null;
		}
		
        time = 0;
        while (time < EndLength)
        {
            time += deltaTime;
            float n = EndCurve.Evaluate(time / EndLength);
            Time.timeScale = Mathf.Lerp(WantedTimeScale, 1, n);

            yield return null;
        }
		
        IsEffectAlreadyInProgress = false;
    }

    protected override void Update()
    {
        base.Update();

        //Since we are changing the TimeScale we need to keep track of the deltaTime ourself
        deltaTime = Time.realtimeSinceStartup - previousTime;
        previousTime = Time.realtimeSinceStartup;
    }
}