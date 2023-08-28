using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Tree tree;

    public int testNum = 0;
    public HashSet<int> testHashSet = new HashSet<int>();

    [ContextMenu("AAA")]
    public void testLOOG()
    {
        testHashSet.Add(testNum);

        int num = 1; 
        foreach (var testNum in testHashSet)
        {
            Debug.Log("테스트" + num + " : "+ testNum);
            ++num; 
        }
    }
    
    [ContextMenu("ㅁㅁㅁ")]
    public void TestLog()
    {
        KeyInputData keyInputData1 = new KeyInputData { inputType = InputType.Key, keyCode = KeyCode.A };
        KeyInputData keyInputData2 = new KeyInputData { inputType = InputType.Key, keyCode = KeyCode.A };
        
        Debug.Log($"record 테스트 : {keyInputData1 == keyInputData2}");

        int a = 1;
        int b = 2;
        int c = 1;
        int d = a; 
        Debug.Log($"Equal 테스트 : {a==c}");
        Debug.Log($"Equal 테스트 : {a==b}");
        Debug.Log($"Equal 테스트 : {a==d}");
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}