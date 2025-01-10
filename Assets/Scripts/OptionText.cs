using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionText : MonoBehaviour
{
    public TextMeshProUGUI displayText; // 用于显示文本的Text组件
    public Button[] buttons; // 所有按钮的数组

    private GameObject currentPanel; // 当前显示的 Panel
    private string currentDisplayText; // 当前显示的文本内容

    private void Start()
    {
        
        // 为每个按钮添加点击事件
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => UpdateText(button));
        }

    }

    private void UpdateText(Button button)
    {
        // 获取按钮上的文本
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;

        // 根据按钮上的文本判断并设置显示文本
        switch (buttonText)
        {
            case "转子与磁钢":
                currentDisplayText = "转子:转子由铁心、磁钢、轴压装而成\r\n磁钢:电机磁通由磁钢提供,对电机性能影响最大;钕铁硼磁体由粉末冶金法制成,是目前磁性能最高的磁性材料;优点是高抗退磁性、高性价比,其缺点是温度依赖性比较强,耐腐蚀性能比较弱,需适当涂层或电镀处理\r\n";
                break;
            case "定子与机壳":
                currentDisplayText = "定子主要由铁心、线圈组成:定子铁心由硅钢片冲片叠亚而成;漆包线绕制成线圈,嵌入铁心槽内,再进行绝缘处理;将绝缘处理后的铁心套入机壳得到定子\r\n";
                break;
            case "控制主板":
                currentDisplayText = "控制主板主要用来采集电机的温度、转速、电流、转子位置等工作信号,一般通过CAN线与VCU等外界控制单元进行通信.(驱动)、超级电容、放电电阻、电流感应器、壳体水道等组成\r\n";
                break;
            case "IGBT":
                currentDisplayText = "绝缘栅双级型晶体管(IGBT),是由BJT(双极型三级管)和MOS(绝缘栅型场效应管)组成的复合全控型电压驱动式功率半导体器件\r\n";
                break;
            case "减速齿轮":
                currentDisplayText = "是一种由封闭在刚性壳体内的齿轮传动、蜗杆传动、齿轮-蜗杆传动所组成的独立部件在原动机和工作机或执行机构间的匹配转速和传递转矩的作用\r\n";
                break;
            case "差速器":
                currentDisplayText = "能够使左右(或前、后)驱动轮实现以不同转速转动的机构.功用是当汽车转弯行驶或在不平路面上行驶时,使左右车轮以不同转速滚动,即保证两侧驱动车轮作纯滚动运动，差速器是为了调整左右车轮的转速差而装置的\r\n";
                break;
            case "电机控制器":
                currentDisplayText = "电机控制器主要是由接口电路、控制主板、IGBT模块(驱动)、超级电容、放电电阻、电流感应器、壳体水道等组成\r\n";
                break;
            case "无":
                currentDisplayText = "无";
                break;
            default:
                currentDisplayText = "未知按钮";
                break;
        }

        // 更新显示文本
        displayText.text = currentDisplayText;
    }

    // 打开对应的 Panel
    
   
   
}