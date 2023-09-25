using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModule : AbBaseModule
{
    [SerializeField]
    private SkillSO skillSO;

    private AIConditions aiConditions; 
    private UnitAniamtion unitAnimation; 
    // 범위 -> 히트박스로 
    public SkillCollider hitCollider;
    public ParticleSystem[] particleSystems;
    protected override void Start()
    {
        base.Start();
        unitAnimation ??= mainModule.GetModule<UnitAniamtion>(ModuleType.Animation);
        aiConditions ??= mainModule.GetModule<AIConditions>(ModuleType.AICondition);
        hitCollider.Init(skillSO,mainModule.GetComponent<ITeam>());
        particleSystems = skillSO.effectParent.GetComponentsInChildren<ParticleSystem>(); 
    }

    public bool CanUseSkill()
    {
        var _cols = MyCollisions();
        if (_cols.Length > 0)
        {
            return true;  
        }
        return false; 
    }
    
    public Collider[] MyCollisions() 
    {
        Vector3 size =  hitCollider.Collider.size;
        var lossyScale = hitCollider.Collider.transform.lossyScale;
        Vector3 center = new Vector3(
            (transform.position.x) + (hitCollider.Collider.center.x +  hitCollider.Collider.transform.localPosition.x) * hitCollider.Collider.transform.lossyScale.x,
            (transform.position.y) + (hitCollider.Collider.center.y +  hitCollider.Collider.transform.localPosition.y) * hitCollider.Collider.transform.lossyScale.y,
            (transform.position.z) + (hitCollider.Collider.center.z +  hitCollider.Collider.transform.localPosition.z) * hitCollider.Collider.transform.lossyScale.z
        );
        Quaternion rot = hitCollider.Collider.transform.rotation;
        Collider [] hitColliders = Physics.OverlapBox (
            center,
            new Vector3(size.x * lossyScale.x,
                size.y * lossyScale.y,
                size.z * lossyScale.z) /2, 
            rot
        );

        int i = 0;
        while (i < hitColliders.Length) {
            //Debug.Log ("Hit : " + hitColliders [i].name);
            i++;
        }

        return hitColliders; 
    }
    
    /// <summary>
    /// 이펙토
    /// </summary>
    public void PlayEffect()
    {
        bool _isCanPerformSkill = false;
        if (aiConditions.IsCanSkill == true && aiConditions.IsSkillCoolTime == false)
        {
            _isCanPerformSkill = true; 
        }
        if (_isCanPerformSkill == true)
        {
            // 이펙트 나오고 
            // 범위 안에 있으면 피격     
            Debug.Log("스킬 사용! ");
            aiConditions.IsSkillCoolTime = true; // 쿨타임이다.. 
            unitAnimation.PlaySkillAnim(true);
            foreach (var _particle in particleSystems)
            {
                _particle.Play();
            }

            // 스킬 쿨타임 체크
            StartCoroutine(CheckCoolTime());
            // 애니메이션 실행 
            // 콜라이더 활성화 
            // 레이어로 걸러졌는데 히트된 적들 데미지 입히기 
        }
        else
        {
            unitAnimation.PlaySkillAnim(false);
        }
    }

    public void PerformSkill()
    {
        StartCoroutine(CoSkill()); 
    }

    private IEnumerator CoSkill()
    {
        hitCollider.ActiveCollider(true);
        yield return null; 
        hitCollider.ActiveCollider(false);
    }

    [SerializeField]
    private float curCoolTIme = 0; 
    private IEnumerator CheckCoolTime()
    {
        while (true)
        {
            curCoolTIme += Time.deltaTime;
            if (curCoolTIme >= skillSO.coolTime)
            {
                aiConditions.IsSkillCoolTime = false; 
                curCoolTIme = 0; 
                foreach (var _particle in particleSystems)
                {
                    _particle.Stop();
                }
                yield break;
            }
        }
    }
}
