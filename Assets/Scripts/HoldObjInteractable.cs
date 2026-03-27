using UnityEngine;
using UnityEngine.UI;

public class HoldObjInteractable : Interactable, Iinteractable
{
    [Header("Hold Settings")]
    public float holdTimeThreshold = 2.0f; // ต้องกดค้าง 2 วินาที
    private float currentTimer = 0f;

    public Image fillImage; // สำหรับแสดงความคืบหน้าของการกดค้าง

    public bool ActiveReturn()
    {
        if (alreadyDone || !_onFogus) return false;

        // สะสมเวลา
        Time.timeScale = 1f;
        currentTimer += Time.deltaTime;

        // อัปเดต UI
        if (fillImage != null)
            fillImage.fillAmount = Mathf.Clamp01(currentTimer / holdTimeThreshold);

        if (currentTimer >= holdTimeThreshold)
        {
            ResetHold(); // ทำเสร็จแล้วก็รีเซ็ตค่าหลอด
            alreadyDone = true;
            Success(); // เรียก Success ที่นี่เลย หรือตาม Logic ของคุณ
            return true;
        }

        return false;
    }

    // ฟังก์ชันสำหรับรีเซ็ตค่า เมื่อปล่อยปุ่ม หรือหลุดโฟกัส
    public void ResetHold()
    {
        if (alreadyDone) return; // ถ้าทำสำเร็จไปแล้ว ไม่ต้องรีเซ็ตเพื่อเริ่มใหม่ (นอกจากจะอยากให้ทำซ้ำได้)

        currentTimer = 0f;
        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    public void Active()
    {
        onFogus();
    }

    public void onFogus()
    {
        if (!alreadyDone && goHightLight != null)
        {
            _onFogus = !_onFogus;
            goHightLight.SetActive(_onFogus);
        }
    }

    

}
