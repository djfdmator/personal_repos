using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CSVParser
{
    public CSVParser()
    {
    }
    ~CSVParser()
    {
        _reader = null;
        _Header = null;
    }

    protected FileInfo _sourceFile = null;
    protected TextReader _reader = null;
    protected string[] _Header = null;


    public virtual void Load() {}
    public virtual void Parse(string[] inputData) {}

    public void LoadFile(string filePath)
    {

        TextAsset _txtFile = (TextAsset)Resources.Load("Data/"+filePath);
        		
		StringReader _reader = new StringReader(_txtFile.text);
   
		int lineCount = 0;
        string inputData = _reader.ReadLine();
		
        while (inputData != null)
        {
            //don't realize new-line("\\n") in ngui UILabel
            inputData = inputData.Replace("\\n", "\n");
            string[] stringList = inputData.Split('^');
            Debug.Log(stringList[0]);
            if (stringList.Length == 0)
            {
                continue;
            }

            if (ParseData(stringList, lineCount) == false)
            {
                Debug.LogError("Parsing fail : " + stringList.ToString());
            }

            inputData = _reader.ReadLine();
            lineCount++;
        }
		_reader.Close (); 		
    }
    public bool ParseData(string[] inputData, int lineCount)
    {
    	if(lineCount < 1)//어느 라인부터 데이터를 입력할 것인지 제한하는 것
    	{
    		return true;
    	}
        if (VarifyKey(inputData[0]) == false)
        {
           //Debug.Log\(//Debug.Log\( Debug.Log("VarifyKey fail : " + inputData[0]);\);\);
            return false;
        }
        Parse(inputData);
        return true;
    }

    public virtual bool VarifyKey(string keyValue)
    {
        return true;
    }


}