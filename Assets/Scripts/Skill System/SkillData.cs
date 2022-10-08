using UnityEngine.Playables;
using UnityEngine;
[System.Serializable]
public class SkillData
{
    public string skillName;
    public float attackRange;//攻击范围
    public float playSpeed;
    public int skillLeve;//技能等级
   // public float skillboots;//技能倍率
    public float orginAttack;//初始伤害
    public float skillEffectRange;//群体技能的影响范围
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
