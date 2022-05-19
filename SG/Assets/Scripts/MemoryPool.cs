using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------------------
// �޸� Ǯ Ŭ����
// �뵵 : Ư�� ���ӿ�����Ʈ�� �ǽð����� ������ �������� �ʰ�,
//      : �̸� ������ �� ���ӿ�����Ʈ�� ��Ȱ���ϴ� Ŭ�����Դϴ�.
//-----------------------------------------------------------------------------------------
//MonoBehaviour ��� �ȹ���. IEnumerable ��ӽ� foreach ��� ����
//System.IDisposable �������� �ʴ� �޸�(���ҽ�)�� ���� ��
public class MemoryPool : IEnumerable, System.IDisposable
{
    //-------------------------------------------------------------------------------------
    // ������ Ŭ����
    //-------------------------------------------------------------------------------------
    class Item
    {
        public bool active; //��������� ����
        public GameObject gameObject;
    }
    Item[] table;

    //------------------------------------------------------------------------------------
    // ������ �⺻ ������
    //------------------------------------------------------------------------------------
    public IEnumerator GetEnumerator()
    {
        if (table == null)
            yield break;

        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.active)
                yield return item.gameObject;
        }
    }
    //-------------------------------------------------------------------------------------
    // �޸� Ǯ ����
    // original : �̸� ������ �� �����ҽ�
    // count : Ǯ �ְ� ����
    //-------------------------------------------------------------------------------------
    public void Create(Object original, int count)
    {
        Dispose();
        table = new Item[count];

        for (int i = 0; i < count; i++)
        {
            Item item = new Item();
            item.active = false;
            item.gameObject = GameObject.Instantiate(original) as GameObject;
            item.gameObject.SetActive(false);
            table[i] = item;
        }
    }
    //-------------------------------------------------------------------------------------
    // �� ������ ��û - ���� �ִ� ��ü�� �ݳ��Ѵ�.
    //-------------------------------------------------------------------------------------
    public GameObject NewItem()
    {
        if (table == null)
            return null;
        int count = table.Length;
        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.active == false)
            {
                item.active = true;
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }

        return null;
    }

    //--------------------------------------------------------------------------------------
    // ������ ������� - ����ϴ� ��ü�� �����Ѵ�.
    // gameOBject : NewItem���� ����� ��ü
    //--------------------------------------------------------------------------------------
    public void RemoveItem(GameObject gameObject)
    {
        if (table == null || gameObject == null)
            return;
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.gameObject == gameObject)
            {
                item.active = false;
                item.gameObject.SetActive(false);
                break;
            }
        }
    }
    //--------------------------------------------------------------------------------------
    // ��� ������ ������� - ��� ��ü�� �����Ѵ�.
    //--------------------------------------------------------------------------------------
    public void ClearItem()
    {
        if (table == null)
            return;
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item != null && item.active)
            {
                item.active = false;
                item.gameObject.SetActive(false);
            }
        }
    }
    //--------------------------------------------------------------------------------------
    // �޸� Ǯ ����
    //--------------------------------------------------------------------------------------
    public void Dispose()
    {
        if (table == null)
            return;
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            GameObject.Destroy(item.gameObject);
        }
        table = null;
    }

}
