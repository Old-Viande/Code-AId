using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public partial class SkillManager
{
    private SkillDatas_SO allSkill;

    private static SkillManager instance;
    public static SkillManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SkillManager();
            }
            return instance;
        }
    }

    


    public SkillManager()
    {
        allSkill = Resources.Load<SkillDatas_SO>("AllSkill");
    }
    public void Init()
    {
        AddSkill("fireball");//��ʱ���ӵļ���
    }
    #region//�ж��Ƿ��ܹ�ʹ��
    /// <summary>
    /// ��0����ȴ��1��ʹ�ô���
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int ColdorCount(SkillData data)
    {
        //1.�ж�����ȴ����ʹ�ô���
        return (int)data.currentSkillProperty;
    }
    public bool SkillCheckColdDown(SkillData data)
    {
        //2.�����ж���������ж��Ƿ���Ա�ʹ��
        if (data.coldDownTime>0)
        {
            return false;
        }
        else if(data.coldDownTime==0)
        {
            return true;
        }
        else
        {
            Debug.Log("��ȴʱ��Ϊ��");
            return false;
        }
    }
    public bool SkillCheckCountDown(SkillData data)
    {
        //2.�����ж���������ж��Ƿ���Ա�ʹ��
        if (data.countDown>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// �Զ��ж������Ƿ����ʹ��
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool SkillCheckAll(SkillData data)
    {
        if( ColdorCount(data)==0)
        {
           return SkillCheckColdDown(data);
        }
        else if(ColdorCount(data)==1)
        {
            return SkillCheckCountDown(data);
        }
        else
        {
            Debug.Log("������ȴ���ʹ���");
            return false;
        }
    }
    #endregion

  
    /// <summary>
    /// ���������Ӽ���
    /// </summary>
    /// <param name="skillname"></param>
    #region//���������Ӽ���
    public void AddSkill(string skillname)
    {
        bool canAdd = true;
        GridManager.Instance.characterData.playerSave.TryGetValue(DataSave.Instance.currentObj.name, out Character player);//���������е�ָ����λ
        //����ϵͳ��Ҫ��һ����allskill�������ַ��似�ܸ�characterSo��api.
        foreach (var skilldata in player.skillSave)
        {
            if( skilldata.skillName==skillname)
            {
                canAdd = false ;
                break;
            }
        }
        if (canAdd)
        {
            allSkill.datasSave.TryGetValue(skillname, out SkillData skill);//�������ֶ�������
            DataSave.Instance.currentObj.GetComponent<CharacterData>().unit.skillSave.Add(skill);//�Ѽ��ܴ��뵱ǰ�ж���λ�ļ��ܱ���
            player.skillSave.Add(skill);
        }
    }

    #endregion
    public void RemoveSkill(string skillname)
    {
        bool canAdd = true;
        GridManager.Instance.characterData.playerSave.TryGetValue(DataSave.Instance.currentObj.name, out Character player);//���������е�ָ����λ
        //����ϵͳ��Ҫ��һ����allskill�������ַ��似�ܸ�characterSo��api.
        foreach (var skilldata in player.skillSave)
        {
            if (skilldata.skillName == skillname)
            {
                canAdd = false;
                break;
            }
        }
        if (canAdd)
        {
            allSkill.datasSave.TryGetValue(skillname, out SkillData skill);//�������ֶ�������
            DataSave.Instance.currentObj.GetComponent<CharacterData>().unit.skillSave.Remove(skill);//�Ѽ��ܴ��뵱ǰ�ж���λ�ļ��ܱ���
            player.skillSave.Remove(skill);
        }
    }
    //��ʼ����
    //ѡ��Ŀ��UI+���ﳯ��+ȡ������+Ⱥ��/����ѡ��Ŀ��ʱui�仯+��ѡ��Χ(���)��ʾ
    public void SkillStart(SkillData skill)
    {       
        //string skillname = "";
        //allSkill.datasSave.TryGetValue(skillname, out SkillData_SO skill);//�������ֶ�������
        //if(skill.currentSkillChoseType == SkillData_SO.skillChoseType.multi)//�������ļ����ǵ��廹��Ⱥ��
        //{
        //    if (skill.attackRange >= (DataSave.Instance.currentObj.transform.position - DataSave.Instance.targetObj.transform.position).magnitude)
        //    {
        //        //������ܵ��ͷŵ��ڷ�Χ��
        //        //���Ҽ��ܵķ�Χ�ڴ�������һ����λ
        //        //��Ŀ��ͷ�ϵı����ʾ����
        //        //Ⱥ������Ҫʹ��Physics.OverlapSphere����ö����λ����ײ�壬�����Ƕ������յ�����Ӱ��ķ�Χ
        //        //��������Ҫʵʱ��������
        //    }
        //}
        //else//�ǵ���
        //{
        //    if (skill.attackRange >= (DataSave.Instance.currentObj.transform.position - DataSave.Instance.targetObj.transform.position).magnitude)//ѡ��ĵ�λ�ڼ��ܵķ�Χ��
        //    {
        //        //��Ŀ��ͷ�ϵı����ʾ����
        //        //����һ�����ȵĻ� ʹ��LineRenderer
        //        //���� �� curve��line������
        //    }
        //}
        GridManager.Instance.skillset(skill);
        UpdataManager.Instance.skillUseButton = true;
    }
    
    
    //��ʾ������̣����ų���Ļ���ߣ�
    //���弼��������������ж��ɹ��󣬾� ͷ����ʾ��ǣ�
    //ͬʱ���������Ӧ����ⲻ��ʱ�����Ӧ������
    //Ⱥ�弼�����������������ʾ��Χ����Χ�������ʾ��ǣ�������һ��ʱ������Ӧ��
    //1.��ʾ��Χ��ʲô��
    //2.�����ʲô

    //����󣬷��������Ļ��������Ϲ�animator�����崫��timeline��


}