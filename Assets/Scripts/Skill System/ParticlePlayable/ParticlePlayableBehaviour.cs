using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ParticlePlayableBehaviour : PlayableBehaviour
{
    public ParticleSystem particle;
    public Transform trans;
    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);//���������ȳ�ʼ������ִ�С�
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

        particle = GameObject.Instantiate(particle).GetComponent<ParticleSystem>();
        particle.Play();
    }
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (trans==null)
        {
            return;
        }
        particle.transform.position = trans.position;
        particle.transform.rotation = trans.rotation;
    }
    public override void OnGraphStop(Playable playable)
    {
        particle.Stop();
    }
}
