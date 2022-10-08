using UnityEngine.Playables;
using UnityEngine;
[System.Serializable]
public class SkillData
{
    public string skillName;
    public float attackRange;//������Χ
    public float playSpeed;
    public int skillLeve;//���ܵȼ�
   // public float skillboots;//���ܱ���
    public float orginAttack;//��ʼ�˺�
    public float skillEffectRange;//Ⱥ�弼�ܵ�Ӱ�췶Χ
    public PlayableAsset timelineAsset;

    public skillType currentSkillType;
    public enum skillType { hill, attack, control, enhanch }
    public skillProperty currentSkillProperty;
    public enum skillProperty { colddown, countdown }
    public skillChoseType currentSkillChoseType;
    public enum skillChoseType { multi,single}
    public skillShape currentSkillShape;
    public enum skillShape {circle,square,fan }
    public int maxUnitChose;
    public int coldDownTime;
    public int maxcoldDownTime;
    public int countDown;
    public int maxcountDown;
}
