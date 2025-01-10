using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionText : MonoBehaviour
{
    public TextMeshProUGUI displayText; // ������ʾ�ı���Text���
    public Button[] buttons; // ���а�ť������

    private GameObject currentPanel; // ��ǰ��ʾ�� Panel
    private string currentDisplayText; // ��ǰ��ʾ���ı�����

    private void Start()
    {
        
        // Ϊÿ����ť��ӵ���¼�
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => UpdateText(button));
        }

    }

    private void UpdateText(Button button)
    {
        // ��ȡ��ť�ϵ��ı�
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;

        // ���ݰ�ť�ϵ��ı��жϲ�������ʾ�ı�
        switch (buttonText)
        {
            case "ת����Ÿ�":
                currentDisplayText = "ת��:ת�������ġ��Ÿ֡���ѹװ����\r\n�Ÿ�:�����ͨ�ɴŸ��ṩ,�Ե������Ӱ�����;����������ɷ�ĩұ���Ƴ�,��Ŀǰ��������ߵĴ��Բ���;�ŵ��Ǹ߿��˴��ԡ����Լ۱�,��ȱ�����¶������ԱȽ�ǿ,�͸�ʴ���ܱȽ���,���ʵ�Ϳ����ƴ���\r\n";
                break;
            case "���������":
                currentDisplayText = "������Ҫ�����ġ���Ȧ���:���������ɹ��Ƭ��Ƭ���Ƕ���;��������Ƴ���Ȧ,Ƕ�����Ĳ���,�ٽ��о�Ե����;����Ե����������������ǵõ�����\r\n";
                break;
            case "��������":
                currentDisplayText = "����������Ҫ�����ɼ�������¶ȡ�ת�١�������ת��λ�õȹ����ź�,һ��ͨ��CAN����VCU�������Ƶ�Ԫ����ͨ��.(����)���������ݡ��ŵ���衢������Ӧ��������ˮ�������\r\n";
                break;
            case "IGBT":
                currentDisplayText = "��Եդ˫���;����(IGBT),����BJT(˫����������)��MOS(��Եդ�ͳ�ЧӦ��)��ɵĸ���ȫ���͵�ѹ����ʽ���ʰ뵼������\r\n";
                break;
            case "���ٳ���":
                currentDisplayText = "��һ���ɷ���ڸ��Կ����ڵĳ��ִ������ϸ˴���������-�ϸ˴�������ɵĶ���������ԭ�����͹�������ִ�л������ƥ��ת�ٺʹ���ת�ص�����\r\n";
                break;
            case "������":
                currentDisplayText = "�ܹ�ʹ����(��ǰ����)������ʵ���Բ�ͬת��ת���Ļ���.�����ǵ�����ת����ʻ���ڲ�ƽ·������ʻʱ,ʹ���ҳ����Բ�ͬת�ٹ���,����֤���������������������˶�����������Ϊ�˵������ҳ��ֵ�ת�ٲ��װ�õ�\r\n";
                break;
            case "���������":
                currentDisplayText = "�����������Ҫ���ɽӿڵ�·���������塢IGBTģ��(����)���������ݡ��ŵ���衢������Ӧ��������ˮ�������\r\n";
                break;
            case "��":
                currentDisplayText = "��";
                break;
            default:
                currentDisplayText = "δ֪��ť";
                break;
        }

        // ������ʾ�ı�
        displayText.text = currentDisplayText;
    }

    // �򿪶�Ӧ�� Panel
    
   
   
}