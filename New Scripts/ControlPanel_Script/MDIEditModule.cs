using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;//内容--增加文件IO功能，姓名--刘旋，时间--2013-3-20

public class MDIEditModule : MonoBehaviour {
	ControlPanel Main;
	CooSystem CooSystem_script;
	SoftkeyModule Softkey_Script;
	NCCodeFormat NCCodeFormat_Script;
	// Use this for initialization
	void Start () {
		Main = gameObject.GetComponent<ControlPanel>();
		CooSystem_script = gameObject.GetComponent<CooSystem>();
		Softkey_Script = gameObject.GetComponent<SoftkeyModule>();
		NCCodeFormat_Script=gameObject.GetComponent<NCCodeFormat>();
	}
	
	public void Edit ()
	{
		GUI.color = Color.cyan;
		if (GUI.Button(new Rect(840f/1000f*Main.width, 270f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "CAN"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.InputText != "")
				{
					char[] TempCharArray = Main.InputText.ToCharArray();
					string TempStr = "";
					for(int i = 0; i < TempCharArray.Length - 1; i++)
					{
						TempStr += TempCharArray[i];
					}
					Main.InputText = TempStr;
					Main.CursorText.text = Main.InputText;
					Main.InputTextSize = Main.sty_InputTextField.CalcSize(new GUIContent(Main.CursorText.text));
					Main.ProgEDITCusorPos = 57f + Main.InputTextSize.x;
				}
			}
		}

		if (GUI.Button(new Rect(780f/1000f*Main.width, 330f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "ALTER"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.ProgMenu)
				{
					if(Main.ProgProtect)
					{
						Main.ProgProtectWarn = true;
					}
					else
					{
						Main.ProgProtectWarn = false;
						if(Main.ProgEDIT|| Main.ProgMDI)
						{
							if(Main.InputText != "")
							{
								AlterCode();	
							}	
						}
					}
				}
			}
		}
		
		if (GUI.Button(new Rect(840f/1000f*Main.width, 330f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "INSERT"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.ProgMenu)
				{
					if(Main.ProgProtect)
					{
						Main.ProgProtectWarn = true;
					}
					else
					{
						Main.ProgProtectWarn = false;
						if(Main.ProgEDIT|| Main.ProgMDI)
						{
							if(Main.InputText != "")
							{
								InsertCode();	
							}	
						}
					}
				}
			}
		}
		
		if (GUI.Button(new Rect(900f/1000f*Main.width, 330f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "DELETE"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.ProgMenu)
				{
					if(Main.ProgProtect)
					{
						Main.ProgProtectWarn = true;
					}
					else
					{
						Main.ProgProtectWarn = false;
						if(Main.ProgEDIT|| Main.ProgMDI)
							DeleteCode();	
					}
				}
			}
		}
		
		if (GUI.Button(new Rect(600f/1000f*Main.width, 390f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "PAGEu"))            
		{
			if(Main.ScreenPower)
				PageUp();
		}
		
		if (GUI.Button(new Rect(600f/1000f*Main.width, 450f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "PAGEd"))            
		{
			if(Main.ScreenPower)
				PageDown();
		}
		GUI.color = Color.white;
		if (GUI.Button(new Rect(660f/1000f*Main.width, 420f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "LEFT"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.ProgMenu)
				{
					if(Main.ProgEDIT||Main.ProgMDI)
					{
						//编辑程序时
						if(Main.ProgEDITProg)
						{
							EditProgLeft();
						}
					}
				}
				if(Main.ProgAUTO)
				{
					Main.AutoDisplayFindRows(Main.autoSelecedProgRow,false);
				}
				if(Main.SettingMenu)
				{
					//刀偏界面左移
					if(Main.OffSetTool)
					{
						CooSystem_script.tool_left();
					}
					//坐标系界面光标左移
					if(Main.OffSetCoo)
					{
						CooSystem_script.Left();
					}
				}
			}
		}
		
		if (GUI.Button(new Rect(720f/1000f*Main.width, 390f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "UP"))            
		{
			if(Main.ScreenPower)
				UpButton();
		}
		
		if (GUI.Button(new Rect(720f/1000f*Main.width, 450f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "DOWN"))            
		{
			if(Main.ScreenPower)
				DownButton();
		}
		
		if (GUI.Button(new Rect(780f/1000f*Main.width, 420f/1000f*Main.height, 50f/1000f*Main.width, 50f/1000f*Main.height), "RIGHT"))            
		{
			if(Main.ScreenPower)
			{
				if(Main.ProgMenu)
				{
					if(Main.ProgEDIT||Main.ProgMDI)
					{
						//编辑程序时
						if(Main.ProgEDITProg)
						{
							EditProgRight();
						}
					}
					if(Main.ProgAUTO)
					{
						Main.AutoDisplayFindRows(Main.autoSelecedProgRow,true);
					}
				}
				
				if(Main.SettingMenu)
				{
					//刀偏界面右移
					if(Main.OffSetTool)
					{
					CooSystem_script.tool_right();
					}
					//坐标系界面光标右移
					if(Main.OffSetCoo)
					{
						CooSystem_script.Right();
					}
				}
			}
		}
	}
	
	float VerticalValue (int Row)
	{
		float Value = 0;
		if(Row == 1)
			Value = 100f;
		else if(Row == 2)
			Value = 125f;
		else if(Row == 3)
			Value = 150f;
		else if(Row == 4)
			Value = 175f;
		else if(Row == 5)
			Value = 200f;
		else if(Row == 6)
			Value = 225f;
		else if(Row == 7)
			Value = 250f;
		else if(Row == 8)
			Value = 275f;
		else 
			Value = 300f;
		return Value;
	}
/*	
	/// <summary>
	/// 替换功能，不好意思，恶心的代码待修改
	/// </summary>
	void AlterCode()
	{
		if(Main.MoreThanOneArray[Main.VerticalNum - 1])
		{
			List<string> TempCodeSubList = new List<string>();
			List<string> CalStr = new List<string>();
			List<List<string>> SingleRowCodeList = new List<List<string>>();
			char[] TempCharArray;
			bool SpaceFlag = false;
			string TestStr = "";
			string FormerWords = "";
			string LatterWords = "";
			int TargetRow = -1;
			int FirstVerticalRow = -1;
			TempCharArray = Main.CodeForAll[Main.RealCodeNum - 1].ToCharArray();
			for(int k = 0; k < TempCharArray.Length; k++)
			{
				if(TempCharArray[k] >= 'A' && TempCharArray[k] <= 'Z')
				{
					if(SpaceFlag)
					{
						CalStr.Add(TestStr);
						TestStr = ""+TempCharArray[k];
						SpaceFlag = false;
					}
					else
					{
						TestStr += TempCharArray[k];
						SpaceFlag = false;
					}
				}
				else
				{
					TestStr += TempCharArray[k];
					SpaceFlag = true;
				}
				if(k == TempCharArray.Length - 1)
				{
					CalStr.Add(TestStr);
					TestStr = "";
					SpaceFlag = false;
				}
			}
			TestStr = "";
			for(int a = 0; a < CalStr.Count; a++)
			{
				Main.EDITText.text = TestStr + CalStr[a];
				Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
				if(Main.TextSize.x > 320f)
				{
					SingleRowCodeList.Add(TempCodeSubList);
					TempCodeSubList = new List<string>();
					TempCodeSubList.Add(CalStr[a]);
					TestStr = CalStr[a] + "  ";
				}
				else
				{
					TempCodeSubList.Add(CalStr[a]);
					TestStr = TestStr + CalStr[a] + "  ";	
				}
				if(a == CalStr.Count - 1)
					SingleRowCodeList.Add(TempCodeSubList);
			}
			string[] MiddleStr = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
			List<string> RowStrWanted2 = new List<string>();
			for(int i = 0; i < MiddleStr.Length; i++)
			{
				if(MiddleStr[i].Trim() != "" && MiddleStr[i].Trim() != ";")
					RowStrWanted2.Add(MiddleStr[i].Trim());
			}	
			for(int k = 0; k < SingleRowCodeList.Count; k++)
			{
				TestStr = "";
				for(int t = 0; t < SingleRowCodeList[k].Count; t++)
				{
					TestStr += SingleRowCodeList[k][t];
				}
				string TestStr2 = "";
				for(int t = 0; t < RowStrWanted2.Count; t++)
				{
					TestStr2 += RowStrWanted2[t];
				}
				if(TestStr == TestStr2)
				{
					TargetRow = k;
					break;
				}	
			}
			FirstVerticalRow = Main.VerticalNum - TargetRow;
			int RangeInt = 0;
			if(TargetRow == SingleRowCodeList.Count - 1)
			{
				if(RowStrWanted2.Count + 1 == Main.HorizontalNum)
					RangeInt = Main.HorizontalNum - 1;
				else
					RangeInt = Main.HorizontalNum;
			}
			else
				RangeInt = Main.HorizontalNum;
			for(int a = 0; a < TargetRow+1; a++)
			{
				for(int k = 0; k < SingleRowCodeList[a].Count; k++)
				{
					if(a == TargetRow)
					{
						if(k == RangeInt - 1)
							break;		
					}
					FormerWords += SingleRowCodeList[a][k];
				}
			}
			for(int a = TargetRow; a < SingleRowCodeList.Count; a++)
			{
				for(int k = 0; k < SingleRowCodeList[a].Count; k++)
				{
					if(a == TargetRow)
					{
						if(k < Main.HorizontalNum)
							k = Main.HorizontalNum;
					}
					if(k >= SingleRowCodeList[a].Count)
						break;
					LatterWords += SingleRowCodeList[a][k];
				}
			}
			char[] TempCharArray1;
			TestStr = "";
			TempCharArray1 = Main.InputText.ToCharArray();
			CalStr = new List<string>();
			for(int k = 0; k < TempCharArray1.Length; k++)
			{
				if(TempCharArray1[k] == ';')
				{
					if(TestStr == "")
					{
						CalStr.Add("");
					}
					else
					{
						CalStr.Add(TestStr);
						TestStr = "";
					}
				}
				else
				{
					TestStr += TempCharArray1[k];
				}	
			}
			if(CalStr.Count > 0 || ((TargetRow == SingleRowCodeList.Count - 1)&&(Main.HorizontalNum == SingleRowCodeList[TargetRow].Count + 1)))
			{
				if(Main.HorizontalNum != SingleRowCodeList[TargetRow].Count + 1)
				{
					Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + CalStr[0];
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}
					Main.CodeForAll.Insert(Main.RealCodeNum,"");
					Main.CodeForAll[Main.RealCodeNum] = TestStr + LatterWords;
					for(int a = CalStr.Count - 1; a >= 1; a--)
					{
						Main.CodeForAll.Insert(Main.RealCodeNum,CalStr[a]);
					}
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						if(Main.CodeForAll[Main.CodeForAll.Count - 1] == "")
							Main.CodeForAll.RemoveAt(Main.CodeForAll.Count - 1);
					}
					Main.TotalCodeNum = Main.CodeForAll.Count;	
				}
				else if((Main.HorizontalNum == SingleRowCodeList[TargetRow].Count + 1) && CalStr.Count > 0)
				{
					Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + CalStr[0];
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}
					Main.CodeForAll[Main.RealCodeNum] = TestStr + Main.CodeForAll[Main.RealCodeNum];
					for(int a = CalStr.Count - 1; a >= 1; a--)
					{
						Main.CodeForAll.Insert(Main.RealCodeNum,CalStr[a]);
					}
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						if(Main.CodeForAll[Main.CodeForAll.Count - 1] == "")
							Main.CodeForAll.RemoveAt(Main.CodeForAll.Count - 1);
					}
					Main.TotalCodeNum = Main.CodeForAll.Count;	
				}
				else
				{
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}
					Main.CodeForAll[Main.RealCodeNum - 1] = Main.CodeForAll[Main.RealCodeNum - 1] + TestStr + Main.CodeForAll[Main.RealCodeNum];
					Main.CodeForAll.RemoveAt(Main.RealCodeNum);
					Main.TotalCodeNum = Main.CodeForAll.Count;	
				}
				if(FirstVerticalRow > 0)
					MiddleCodeEdit(Main.RealCodeNum, FirstVerticalRow);
				else
					MiddleCodeSpecialEdit(Main.RealCodeNum, FirstVerticalRow);
			}
			else
			{
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + TestStr + LatterWords;	
				if(FirstVerticalRow > 0)
					MiddleCodeEdit(Main.RealCodeNum, FirstVerticalRow);
				else
					MiddleCodeSpecialEdit(Main.RealCodeNum, FirstVerticalRow);	
			}
			
			Main.Code01 = Main.TempCodeArray[0];
			Main.Code02 = Main.TempCodeArray[1];
			Main.Code03 = Main.TempCodeArray[2];
			Main.Code04 = Main.TempCodeArray[3];
			Main.Code05 = Main.TempCodeArray[4];
			Main.Code06 = Main.TempCodeArray[5];
			Main.Code07 = Main.TempCodeArray[6];
			Main.Code08 = Main.TempCodeArray[7];
			Main.Code09 = Main.TempCodeArray[8];
			string[] MiddleStr1 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
			List<string> RowStrWanted3 = new List<string>();
			for(int i = 0; i < MiddleStr1.Length; i++)
			{
				if(MiddleStr1[i].Trim() != "")
					RowStrWanted3.Add(MiddleStr1[i].Trim());
			}
			if(RowStrWanted3.Count < Main.HorizontalNum)
			{
				Main.HorizontalNum = RowStrWanted3.Count;		
			}
			string TempStr = "";
			for(int i = 0; i < Main.HorizontalNum - 1; i++)
			{
				TempStr = TempStr + RowStrWanted3[i] + "0";
			}
			Main.EDITText.text = TempStr;
			Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			Main.ProgEDITCusorH = 32f + Main.TextSize.x;
			Main.EDITText.text = RowStrWanted3[Main.HorizontalNum - 1].Trim();
			Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			Main.ProgEDITCusorV = VerticalValue(Main.VerticalNum);
			Main.InputText = "";
			Main.ProgEDITCusorPos = 57f;
		}
		else
		{
			List<string> CalStr = new List<string>();
			char[] TempCharArray;
			string TestStr = "";
			string FormerWords = "";
			string LatterWords = "";
			string[] MiddleStr = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
			List<string> RowStrWanted = new List<string>();
			for(int i = 0; i < MiddleStr.Length; i++)
			{
				if(MiddleStr[i].Trim() != "")
					RowStrWanted.Add(MiddleStr[i].Trim());
			}
			for(int a = 0; a < Main.HorizontalNum - 1; a++)
			{
				if(RowStrWanted[a] != ";")
					FormerWords += RowStrWanted[a];	
			}
			for(int a = Main.HorizontalNum; a < RowStrWanted.Count; a++)
			{
				if(RowStrWanted[a] != ";")
					LatterWords += RowStrWanted[a];	
			}
			TempCharArray = Main.InputText.ToCharArray();
			CalStr = new List<string>();
			for(int k = 0; k < TempCharArray.Length; k++)
			{
				if(TempCharArray[k] == ';')
				{
					if(TestStr == "")
					{
						CalStr.Add("");
					}
					else
					{
						CalStr.Add(TestStr);
						TestStr = "";
					}
				}
				else
				{
					TestStr += TempCharArray[k];
				}		
			}
			if(CalStr.Count > 0 || Main.HorizontalNum == RowStrWanted.Count)
			{
				if(Main.HorizontalNum != RowStrWanted.Count)
				{
					Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + CalStr[0];
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}
					Main.CodeForAll.Insert(Main.RealCodeNum,"");
					Main.CodeForAll[Main.RealCodeNum] = TestStr + LatterWords;
					for(int a = CalStr.Count - 1; a >= 1; a--)
					{
						Main.CodeForAll.Insert(Main.RealCodeNum,CalStr[a]);
					}
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						if(Main.CodeForAll[Main.CodeForAll.Count - 1] == "")
							Main.CodeForAll.RemoveAt(Main.CodeForAll.Count - 1);
					}
					Main.TotalCodeNum = Main.CodeForAll.Count;
				}
				else if((Main.HorizontalNum == RowStrWanted.Count) && CalStr.Count > 0)
				{
					Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + CalStr[0];
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}
					Main.CodeForAll[Main.RealCodeNum] = TestStr + Main.CodeForAll[Main.RealCodeNum];
					for(int a = CalStr.Count - 1; a >= 1; a--)
					{
						Main.CodeForAll.Insert(Main.RealCodeNum,CalStr[a]);
					}
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						if(Main.CodeForAll[Main.CodeForAll.Count - 1] == "")
							Main.CodeForAll.RemoveAt(Main.CodeForAll.Count - 1);
					}
					Main.TotalCodeNum = Main.CodeForAll.Count;	
				}
				else
				{
					if(Main.RealCodeNum == Main.TotalCodeNum)
					{
						Main.CodeForAll.Add("");
					}		
					Main.CodeForAll[Main.RealCodeNum - 1] = Main.CodeForAll[Main.RealCodeNum - 1] + TestStr + Main.CodeForAll[Main.RealCodeNum];	
					Main.CodeForAll.RemoveAt(Main.RealCodeNum);
					Main.TotalCodeNum = Main.CodeForAll.Count;	
				}
			}
			else
			{
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + TestStr + LatterWords;
			}
			MiddleCodeEdit(Main.RealCodeNum, Main.VerticalNum);
			
			Main.Code01 = Main.TempCodeArray[0];
			Main.Code02 = Main.TempCodeArray[1];
			Main.Code03 = Main.TempCodeArray[2];
			Main.Code04 = Main.TempCodeArray[3];
			Main.Code05 = Main.TempCodeArray[4];
			Main.Code06 = Main.TempCodeArray[5];
			Main.Code07 = Main.TempCodeArray[6];
			Main.Code08 = Main.TempCodeArray[7];
			Main.Code09 = Main.TempCodeArray[8];
			
			string[] MiddleStr1 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
			List<string> RowStrWanted3 = new List<string>();
			for(int i = 0; i < MiddleStr1.Length; i++)
			{
				if(MiddleStr1[i].Trim() != "")
					RowStrWanted3.Add(MiddleStr1[i].Trim());
			}
			if(RowStrWanted3.Count < Main.HorizontalNum)
			{
				Main.HorizontalNum = RowStrWanted3.Count;		
			}
			string TempStr = "";
			for(int i = 0; i < Main.HorizontalNum - 1; i++)
			{
				TempStr = TempStr + RowStrWanted3[i] + "0";
			}
			Main.EDITText.text = TempStr;
			Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			Main.ProgEDITCusorH = 32f + Main.TextSize.x;
			Main.EDITText.text = RowStrWanted3[Main.HorizontalNum - 1].Trim();
			Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			Main.ProgEDITCusorV = VerticalValue(Main.VerticalNum);
			Main.InputText = "";
			Main.ProgEDITCusorPos = 57f;
		}
	}
*/
	
	/// <summary>
	/// 重新编写的AlterCode()，实现代码的替换 董帅 2013-4-2
	/// </summary>
	void AlterCode() 
	{
	   if(Main.SelectStart==0)
			Main.isSelecFirst=true;
	   else
			Main.isSelecFirst=false;
	   DeleteCode();
	   InsertCode();
	}
	
	
	/// <summary>
	/// 中间过程函数
	/// </summary>
	void MiddleCodeEdit (int StartRealNum, int StartVerticalNum) 
	{
		List<string> CalStr = new List<string>();
		List<string> StrStorage = new List<string>();
		int VerticalRow = StartVerticalNum - 1;
		for(int i = StartRealNum - 1; i < Main.TotalCodeNum; i++)
		{
			if(VerticalRow > 8)
				break;
			if(Main.CodeForAll[i] == "%")
			{
				StrStorage.Add("%");
				Main.TempCodeArray[VerticalRow] = "%";
				Main.RealNumArray[VerticalRow] = i;
				Main.MoreThanOneArray[VerticalRow] = false;
				VerticalRow++;
			}
			else if(Main.CodeForAll[i] == "")
			{
				StrStorage.Add(";");
				Main.TempCodeArray[VerticalRow] = ";";
				Main.RealNumArray[VerticalRow] = i;
				Main.MoreThanOneArray[VerticalRow] = false;
				VerticalRow++;
			}
			else
			{
				char[] TempCharArray;
				bool SpaceFlag = false;
				string AimCode = "";
				CalStr.Clear();
				CalStr = new List<string>();
				TempCharArray = Main.CodeForAll[i].ToCharArray();
				for(int k = 0; k < TempCharArray.Length; k++)
				{
					if(TempCharArray[k] >= 'A' && TempCharArray[k] <= 'Z')
					{
						if(SpaceFlag)
						{
							CalStr.Add(AimCode);
							AimCode = ""+TempCharArray[k];
							SpaceFlag = false;
						}
						else
						{
							AimCode += TempCharArray[k];
							SpaceFlag = false;
						}
					}
					else
					{
						AimCode += TempCharArray[k];
						SpaceFlag = true;
					}
					if(k == TempCharArray.Length - 1)
					{
						CalStr.Add(AimCode);
						AimCode = "";
						SpaceFlag = false;
						CalStr.Add(";");
					}
				}
				AimCode = "";
				SpaceFlag = false;
				for(int a = 0; a < CalStr.Count; a++)
				{
					if(VerticalRow > 8)
						break;
					Main.EDITText.text = AimCode + CalStr[a];
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
				
					if(Main.TextSize.x > 320f)
					{
						Main.TempCodeArray[VerticalRow] = AimCode.TrimEnd();						
						Main.RealNumArray[VerticalRow] = i;				
						Main.MoreThanOneArray[VerticalRow] = true;					
						VerticalRow++;					
						AimCode = CalStr[a] + "  ";
						SpaceFlag = true;
					}
					else
					{
						AimCode	= AimCode + CalStr[a] + "  ";
					}
				}
				
				if(VerticalRow <= 8)
				{
					Main.TempCodeArray[VerticalRow] = AimCode.TrimEnd();					
					Main.RealNumArray[VerticalRow] = i;				
					if(SpaceFlag)				
						Main.MoreThanOneArray[VerticalRow] = true;					
					else				
						Main.MoreThanOneArray[VerticalRow] = false;					
					VerticalRow++;
				}	
			}
		}	
		if(VerticalRow < 9)
		{
			for(int a = VerticalRow; a < 9; a++)
			{
				Main.TempCodeArray[a] = "";
				Main.RealNumArray[a] = -1;
				Main.MoreThanOneArray[a] = false;
			}
		}	
	}
	
	/// <summary>
	/// 中间过程函数
	/// </summary>
	void MiddleCodeSpecialEdit(int StartRealNum, int StartVerticalNum)  
	{
		List<string> CalStr = new List<string>();
		List<string> StrStorage = new List<string>();
		int RejectedRow = 1 - StartVerticalNum;
		int VerticalRow = 0;
		int RejectedNum = 0;
		for(int i = StartRealNum - 1; i < Main.TotalCodeNum; i++)
		{
			if(VerticalRow > 8)
				break;
			if(Main.CodeForAll[i] == "%")
			{
				StrStorage.Add("%");
				Main.TempCodeArray[VerticalRow] = "%";
				Main.RealNumArray[VerticalRow] = i;
				Main.MoreThanOneArray[VerticalRow] = false;
				VerticalRow++;
			}
			else if(Main.CodeForAll[i] == "")
			{
				StrStorage.Add(";");
				Main.TempCodeArray[VerticalRow] = ";";
				Main.RealNumArray[VerticalRow] = i;
				Main.MoreThanOneArray[VerticalRow] = false;
				VerticalRow++;
			}
			else
			{
				char[] TempCharArray;
				bool SpaceFlag = false;
				string AimCode = "";
				CalStr.Clear();
				CalStr = new List<string>();
				TempCharArray = Main.CodeForAll[i].ToCharArray();
				for(int k = 0; k < TempCharArray.Length; k++)
				{
					if(TempCharArray[k] >= 'A' && TempCharArray[k] <= 'Z')
					{
						if(SpaceFlag)
						{
							CalStr.Add(AimCode);
							AimCode = ""+TempCharArray[k];
							SpaceFlag = false;
						}
						else
						{
							AimCode += TempCharArray[k];
							SpaceFlag = false;
						}	
					}
					else
					{
						AimCode += TempCharArray[k];
						SpaceFlag = true;
					}	
					if(k == TempCharArray.Length - 1)
					{
						CalStr.Add(AimCode);
						AimCode = "";
						SpaceFlag = false;
						CalStr.Add(";");
					}
				}
				AimCode = "";	
				SpaceFlag = false;
				for(int a = 0; a < CalStr.Count; a++)
				{
					if(VerticalRow > 8)
						break;
					Main.EDITText.text = AimCode + CalStr[a];
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					if(Main.TextSize.x > 320f)
					{
						RejectedNum++;
						if(RejectedNum > RejectedRow)
						{
							Main.TempCodeArray[VerticalRow] = AimCode.TrimEnd();
							Main.RealNumArray[VerticalRow] = i;
							Main.MoreThanOneArray[VerticalRow] = true;
							VerticalRow++;
						}
						AimCode = CalStr[a] + "  ";
						SpaceFlag = true;
					}
					else
					{
						AimCode	= AimCode + CalStr[a] + "  ";
					}
				}	
				if(VerticalRow <= 8)
				{
					Main.TempCodeArray[VerticalRow] = AimCode.TrimEnd();
					Main.RealNumArray[VerticalRow] = i;		
					if(SpaceFlag)		
						Main.MoreThanOneArray[VerticalRow] = true;				
					else				
						Main.MoreThanOneArray[VerticalRow] = false;				
					VerticalRow++;
				}
			}
		}	
		if(VerticalRow < 8)
		{
			for(int a = VerticalRow; a < 9; a++)
			{
				Main.TempCodeArray[a] = "";
				Main.RealNumArray[a] = -1;
				Main.MoreThanOneArray[a] = false;
			}
		}
	}
	
	/// <summary>
	/// 重新编写的InsertCode ()，实现代码的添加 陈晓威 2013-3-31
	/// </summary>
	void InsertCode ()
	{
		//待插入的字符串
		int row_begin = 0;
		int row_end;
		int row_length;
		int append_index;
		int append_pos = 0;
		int flag = 0;
		int tmp_index = 0;		
		//Debug.Log("V:"+Main.ProgEDITCusorV);
		//当程序只为"_"时，删除这个("_"为下划线为了显示光标)
		if(Main.CodeForAll.Count == 1 && Main.CodeForAll[0] == ";")
		{
		    Main.CodeForAll.RemoveAt(0);
		    Main.TotalCodeNum--;
		}
		//记录未插入前的代码长度
		int before_codenum = Main.CodeForAll.Count;
		List<string>appendlist = NCCodeFormat_Script.CodeFormat(Main.EDITText.text);
		//如果是插入;因为会被格式方法CodeFormat去除，这里直接补上;
		if(Main.EDITText.text.Trim() == ";")appendlist.Add(";");
		append_index = Main.ProgEDITCusorV - 1;
//		Debug.Log("V" + Main.ProgEDITCusorV);
		//代码为空时添加则pos=0,否则为selectstart+1
		if(Main.CodeForAll.Count == 0||Main.isSelecFirst)
			append_pos = 0;
		else if(Main.ProgEDITCusorV >= 0 && Main.CodeForAll.Count != 0)
			append_pos = Main.SelectStart + 1;
		
		
		string editstr = Main.EDITText.text;
		//如果插入的代码最后没有;,则删除
		if(editstr.Substring(editstr.Length - 1) != ";")
		{
		    appendlist.RemoveAt(appendlist.Count - 1);
		    flag = 1;
		}
		//插入时的位置
		tmp_index = append_pos > 1 ? (append_pos - 1) : 0;
		//Debug.Log("addpos:"+append_pos);
		foreach(string subappendstr in appendlist)
		{
		    if(append_pos > Main.CodeForAll.Count - 1)
		        Main.CodeForAll.Add(subappendstr);
		    else
		        Main.CodeForAll.Insert(append_pos, subappendstr);
		    append_pos++;
		
		    //Debug.Log(subappendstr);
		}
		Main.TotalCodeNum = Main.CodeForAll.Count;
		Softkey_Script.calcSepo(Main.CodeForAll,Main.SeparatePos,302f);
		//EditProgRight();
		//有分号
		if(flag == 0)
		{
			while(Main.CodeForAll[Main.SelectStart]!=";")EditProgRight();
			EditProgRight();
		}else
		{
			EditProgRight();
		}
		
		
		/*
		//Debug.Log("tmp_index:" + tmp_index);
		//有分号
		if(flag == 0)
		{
		    Debug.Log("PV:"+Main.ProgEDITCusorV);
			Debug.Log("ME:"+Main.EndRow);
			
			// if(Main.SelectStart < Main.TotalCodeNum - 1)
		  //  {
		//		Debug.Log("isenter");
		        //从空到添加新代码,v不增加
		 //       if(before_codenum != 0)
		 //           Main.ProgEDITCusorV++;
		//        Main.ProgEDITCusorH = 0;
		//    }
		    

			//此时为最后一行并且在;后插的时候
			if(Main.ProgEDITCusorV + 1 == Main.EndRow && Main.CodeForAll[Main.SelectStart] == ";")
		    {
		        Main.StartRow++;
		        Main.EndRow++;
		    }
		    Debug.Log(Main.CodeForAll[tmp_index]);
			//插入的位置不是;
		    if(Main.CodeForAll[tmp_index] != ";")
		    {
		        if(append_pos > Main.TotalCodeNum - 1)append_pos = Main.TotalCodeNum - 1;
		        Main.SelectStart = append_pos;
		        Main.SelectEnd = append_pos;
		        Main.ProgEDITCusorH = append_pos;
				
		        if(before_codenum != 0)
				{
		            Main.ProgEDITCusorV++;
					Main.ProgEDITCusorH = 0;
				}
				
		        Debug.Log("tes");
		    }
		    else
		    {
		        if(Main.SelectStart < Main.TotalCodeNum - 1)
		        {
		
		            Main.SelectStart++;
		            Main.SelectEnd++;
					Main.ProgEDITCusorH++;
		
		        }
				if(before_codenum != 0)
				{
		            Main.ProgEDITCusorV++;
					Main.ProgEDITCusorH = 0;
				}
		    }
			Debug.Log("Mainst:"+Main.SelectStart);
			Debug.Log("MTC:"+Main.TotalCodeNum);
		   
		}
		else
		{
		    //Debug.Log(Main.ProgEDITCusorV+"+"+Main.EndRow);
			//Debug.Log("ET1");
		    if(Main.CodeForAll[Main.SelectStart] == ";")
		    {
		        //Debug.Log("ET2");
				if(Main.ProgEDITCusorV + 1 == Main.EndRow)
		        {
		            Main.StartRow++;
		            Main.EndRow++;
		        }
		        if(Main.SelectStart < Main.TotalCodeNum - 1)
		        {
		            Main.ProgEDITCusorV++;
		            Main.ProgEDITCusorH = 0;
		            Main.SelectStart++;
		            Main.SelectEnd++;
		        }
		    }
			//0位置和最后位置ss不增加
			else if(Main.SelectStart<Main.CodeForAll.Count-1&&!Main.isSelecFirst)
			{
					//Debug.Log("ET3");
					Main.SelectStart++;
					Main.SelectEnd++;
					Main.ProgEDITCusorH++;
			}
		}
		
		*/
		//在只有;时候插入处理为在;前插入
		if(before_codenum==0){Main.CodeForAll.Add(";");Main.TotalCodeNum=Main.CodeForAll.Count;}
		//Debug.Log("lastV:" + Main.ProgEDITCusorV);
		Main.InputText = "";
		//插入完毕后光标移到最左边
		Main.ProgEDITCusorPos=57f;
	}
	
	/*
	/// <summary>
	/// 初步格式化，不好意思，恶心的代码待修改
	/// </summary>
	public void CodeEdit ()
	{
		List<int> RealNumList = new List<int>();
		Main.TempCodeList.Clear();
		Main.TempCodeList = new List<List<string>>();
		List<string> TempCodeSubList = new List<string>();
		string TestStr = "";
		int Nine = 9;
		if(Main.TotalCodeNum <= Main.RealCodeNum + 8)
			Nine = Main.TotalCodeNum;
		else
			Nine = Main.RealCodeNum + 8;
		for(int i = 0; i < 9; i++)
		{
			Main.TempCodeArray[i] = "";
			Main.MoreThanOneArray[i] = false;
			Main.RealNumArray[i] = -1;
		}
		for(int i = Main.RealCodeNum - 1; i < Nine; i++)
		{
			TempCodeSubList = new List<string>();
			if(Main.CodeForAll[i] == "%")
			{
				TempCodeSubList.Add(Main.CodeForAll[i]);
				Main.TempCodeList.Add(TempCodeSubList);
				RealNumList.Add(i);
			}
			else if(Main.CodeForAll[i] == "")
			{
				TempCodeSubList.Add(";");
				Main.TempCodeList.Add(TempCodeSubList);
				RealNumList.Add(i);
			}
			else
			{
				TestStr = Main.CodeForAll[i];
				char[] TempCharArray;
				bool SpaceFlag = false;
				TempCharArray = TestStr.ToCharArray();
				TestStr = "";
				for(int k = 0; k < TempCharArray.Length; k++)
				{
					if(TempCharArray[k] >= 'A' && TempCharArray[k] <= 'Z')
					{
						if(SpaceFlag)
						{
							TempCodeSubList.Add(TestStr);
							TestStr = ""+TempCharArray[k];	
							SpaceFlag = false;
						}
						else
						{
							TestStr += TempCharArray[k];
							SpaceFlag = false;
						}
					}
					else
					{
						TestStr += TempCharArray[k];
						SpaceFlag = true;
					}
					
					if(k == TempCharArray.Length - 1)
					{
						TempCodeSubList.Add(TestStr);
						TestStr = "";
						SpaceFlag = false;
						TempCodeSubList.Add(";");
					}
				}
				Main.TempCodeList.Add(TempCodeSubList);
				RealNumList.Add(i);			
			}
		}
		int Row = 0;
		int RowA = 0;
		int Number = Main.TempCodeList.Count;
		for(int i = 0; i < Number; i++)
		{
			Row = i + RowA;
			for(int k =0; k < Main.TempCodeList[i].Count; k++)
			{
				if(k != Main.TempCodeList[i].Count - 1)
				{
					Main.EDITText.text = Main.TempCodeArray[Row] + Main.TempCodeList[i][k];
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					if(Main.TextSize.x > 320f)
					{
						RowA++;
						Number--;
						Row = i + RowA;
						if(Row > 8)
							break;
						Main.TempCodeArray[Row - 1] = Main.TempCodeArray[Row - 1].TrimEnd();
						Main.TempCodeArray[Row] += (Main.TempCodeList[i][k] + "  ");
						Main.RealNumArray[Row] = i + Main.RealCodeNum - 1;
						Main.MoreThanOneArray[Row - 1] = true;
						Main.MoreThanOneArray[Row] = true;
					}
					else
					{
						Main.TempCodeArray[Row] += (Main.TempCodeList[i][k] + "  ");
						Main.RealNumArray[Row] = i + Main.RealCodeNum - 1;	
					}
				}
				else
				{
					Main.EDITText.text = Main.TempCodeArray[Row] + Main.TempCodeList[i][k];
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					if(Main.TextSize.x >= 320f)
					{
						RowA++;		
						Number--;	
						Row = i + RowA;
						if(Row > 8)
							break;
						Main.TempCodeArray[Row - 1] = Main.TempCodeArray[Row - 1].TrimEnd();
						Main.TempCodeArray[Row] += (Main.TempCodeList[i][k]);
						Main.RealNumArray[Row] = i + Main.RealCodeNum - 1;
						Main.MoreThanOneArray[Row - 1] = true;
						Main.MoreThanOneArray[Row] = true;
					}
					else
					{
						Main.TempCodeArray[Row] += (Main.TempCodeList[i][k]);
						Main.RealNumArray[Row] = i + Main.RealCodeNum - 1;
					}
				}
			}
			if(Row > 8)
				break;
		}
		Main.StartRow = Main.RealCodeNum - 1;
		if(Number == 1)
			Main.EndRow = Main.StartRow + 1;
		else if(Number == 0)
			Main.EndRow = Main.StartRow;
		else
			Main.EndRow = Main.StartRow + Number - 1;
		Main.Code01 = Main.TempCodeArray[0];
		Main.Code02 = Main.TempCodeArray[1];
		Main.Code03 = Main.TempCodeArray[2];
		Main.Code04 = Main.TempCodeArray[3];
		Main.Code05 = Main.TempCodeArray[4];
		Main.Code06 = Main.TempCodeArray[5];
		Main.Code07 = Main.TempCodeArray[6];
		Main.Code08 = Main.TempCodeArray[7];
		Main.Code09 = Main.TempCodeArray[8];
	}
	 */
	
	/// <summary>
	/// 重写-董,陈
	/// </summary>
	public void CodeEdit ()
	{
		Main.StartRow = 0;
		Main.EndRow = 9;
	}
	
	/*
	void DeleteCode () 
	{
		List<string> TempCodeSubList = new List<string>();
		List<string> CalStr = new List<string>();
		List<List<string>> SingleRowCodeList = new List<List<string>>();
		char[] TempCharArray;
		bool SpaceFlag = false;
		string TestStr = "";
		string FormerWords = "";
		string LatterWords = "";
		int TargetRow = -1;
		int FirstVerticalRow = -1;	
		string KeyWord = "";		
		TempCharArray = Main.CodeForAll[Main.RealCodeNum - 1].ToCharArray();	
		for(int k = 0; k < TempCharArray.Length; k++)
		{
			if(TempCharArray[k] >= 'A' && TempCharArray[k] <= 'Z')
			{
				if(SpaceFlag)
				{
					CalStr.Add(TestStr);
					TestStr = ""+TempCharArray[k];
					SpaceFlag = false;
				}
				else
				{
					TestStr += TempCharArray[k];
					SpaceFlag = false;
				}
			}
			else
			{
				TestStr += TempCharArray[k];
				SpaceFlag = true;
			}
			if(k == TempCharArray.Length - 1)
			{
				CalStr.Add(TestStr);
				TestStr = "";
				SpaceFlag = false;
			}
		}
		TestStr = "";
		for(int a = 0; a < CalStr.Count; a++)
		{
			Main.EDITText.text = TestStr + CalStr[a];
			Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			if(Main.TextSize.x > 320f)
			{
				SingleRowCodeList.Add(TempCodeSubList);
				TempCodeSubList = new List<string>();
				TempCodeSubList.Add(CalStr[a]);
				TestStr = CalStr[a] + "  ";
			}
			else
			{
				TempCodeSubList.Add(CalStr[a]);
				TestStr = TestStr + CalStr[a] + "  ";	
			}
			if(a == CalStr.Count - 1)
				SingleRowCodeList.Add(TempCodeSubList);	
		}
		string[] MiddleStr = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
		List<string> RowStrWanted = new List<string>();	
		for(int i = 0; i < MiddleStr.Length; i++)
		{
			if(MiddleStr[i].Trim() != "")
				RowStrWanted.Add(MiddleStr[i].Trim());
		}
		KeyWord = RowStrWanted[Main.HorizontalNum - 1];
		if(Main.MoreThanOneArray[Main.VerticalNum - 1])
		{
			for(int k = 0; k < SingleRowCodeList.Count; k++)
			{
				TestStr = "";
				for(int t = 0; t < SingleRowCodeList[k].Count; t++)
				{
					TestStr += SingleRowCodeList[k][t];
				}
				string TestStr2 = "";
				for(int t = 0; t < RowStrWanted.Count; t++)
				{
					if(RowStrWanted[t] != ";")
						TestStr2 += RowStrWanted[t];
				}
				if(TestStr == TestStr2)
				{
					TargetRow = k;
					break;
				}
			}
			FirstVerticalRow = Main.VerticalNum - TargetRow;
			if(KeyWord == ";")
			{
				if(Main.RealCodeNum == Main.TotalCodeNum)
				{
					Main.EDITText.text = KeyWord;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));		
					return;
				}
				FormerWords = Main.CodeForAll[Main.RealCodeNum - 1];
				LatterWords = Main.CodeForAll[Main.RealCodeNum];
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + LatterWords;
				Main.CodeForAll.RemoveAt(Main.RealCodeNum);
				Main.TotalCodeNum = Main.CodeForAll.Count;
				if(FirstVerticalRow > 0)
					MiddleCodeEdit(Main.RealCodeNum, FirstVerticalRow);
				else
					MiddleCodeSpecialEdit(Main.RealCodeNum, FirstVerticalRow);
				
				Main.Code01 = Main.TempCodeArray[0];
				Main.Code02 = Main.TempCodeArray[1];
				Main.Code03 = Main.TempCodeArray[2];
				Main.Code04 = Main.TempCodeArray[3];
				Main.Code05 = Main.TempCodeArray[4];
				Main.Code06 = Main.TempCodeArray[5];
				Main.Code07 = Main.TempCodeArray[6];
				Main.Code08 = Main.TempCodeArray[7];
				Main.Code09 = Main.TempCodeArray[8];
				
				string[] MiddleStr2 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
				List<string> RowStrWanted2 = new List<string>();
				for(int i = 0; i < MiddleStr2.Length; i++)
				{
					if(MiddleStr2[i].Trim() != "")
						RowStrWanted2.Add(MiddleStr2[i].Trim());
				}
				if(RowStrWanted2.Count < Main.HorizontalNum)
				{
					Main.HorizontalNum = RowStrWanted2.Count;
					string TempStr = "";
					for(int i = 0; i < Main.HorizontalNum - 1; i++)
					{
						TempStr = TempStr + RowStrWanted2[i] + "0";
					}
					Main.EDITText.text = TempStr;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					Main.ProgEDITCusorH = 32f + Main.TextSize.x;
				}
				Main.EDITText.text = RowStrWanted2[Main.HorizontalNum - 1].Trim();
				Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));	
			}
			else
			{
				for(int a = 0; a < TargetRow+1; a++)
				{
					for(int k = 0; k < SingleRowCodeList[a].Count; k++)
					{
						if(a == TargetRow)
						{
							if(k == Main.HorizontalNum - 1)
								break;		
						}
						FormerWords += SingleRowCodeList[a][k];
					}
				}
				for(int a = TargetRow; a < SingleRowCodeList.Count; a++)
				{
					for(int k = 0; k < SingleRowCodeList[a].Count; k++)
					{
						if(a == TargetRow)
						{
							if(k < Main.HorizontalNum)
								k = Main.HorizontalNum;
						}
						if(k >= SingleRowCodeList[a].Count)
							break;
						LatterWords += SingleRowCodeList[a][k];
					}
				}
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + LatterWords;
				if(FirstVerticalRow > 0)
					MiddleCodeEdit(Main.RealCodeNum, FirstVerticalRow);
				else
					MiddleCodeSpecialEdit(Main.RealCodeNum, FirstVerticalRow);
				
				Main.Code01 = Main.TempCodeArray[0];
				Main.Code02 = Main.TempCodeArray[1];
				Main.Code03 = Main.TempCodeArray[2];
				Main.Code04 = Main.TempCodeArray[3];
				Main.Code05 = Main.TempCodeArray[4];
				Main.Code06 = Main.TempCodeArray[5];
				Main.Code07 = Main.TempCodeArray[6];
				Main.Code08 = Main.TempCodeArray[7];
				Main.Code09 = Main.TempCodeArray[8];
				
				string[] MiddleStr2 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
				List<string> RowStrWanted2 = new List<string>();
				for(int i = 0; i < MiddleStr2.Length; i++)
				{
					if(MiddleStr2[i].Trim() != "")
						RowStrWanted2.Add(MiddleStr2[i].Trim());
				}
				if(RowStrWanted2.Count < Main.HorizontalNum)
				{
					Main.HorizontalNum = RowStrWanted2.Count;
					string TempStr = "";
					for(int i = 0; i < Main.HorizontalNum - 1; i++)
					{
						TempStr = TempStr + RowStrWanted2[i] + "0";
					}
					Main.EDITText.text = TempStr;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					Main.ProgEDITCusorH = 32f + Main.TextSize.x;
				}
				Main.EDITText.text = RowStrWanted2[Main.HorizontalNum - 1].Trim();
				Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));	
			}	
		}
		else
		{
			if(KeyWord == ";")
			{
				if(Main.RealCodeNum == Main.TotalCodeNum)
				{
					Main.EDITText.text = KeyWord;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					return;
				}
				for(int a = 0; a < RowStrWanted.Count - 1; a++)
				{
					FormerWords += RowStrWanted[a];
				}
				LatterWords = Main.CodeForAll[Main.RealCodeNum];
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + LatterWords;
				Main.CodeForAll.RemoveAt(Main.RealCodeNum);
				Main.TotalCodeNum = Main.CodeForAll.Count;
				MiddleCodeEdit(Main.RealCodeNum, Main.VerticalNum);
				
				Main.Code01 = Main.TempCodeArray[0];
				Main.Code02 = Main.TempCodeArray[1];
				Main.Code03 = Main.TempCodeArray[2];
				Main.Code04 = Main.TempCodeArray[3];
				Main.Code05 = Main.TempCodeArray[4];
				Main.Code06 = Main.TempCodeArray[5];
				Main.Code07 = Main.TempCodeArray[6];
				Main.Code08 = Main.TempCodeArray[7];
				Main.Code09 = Main.TempCodeArray[8];
				
				string[] MiddleStr2 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
				List<string> RowStrWanted2 = new List<string>();
				for(int i = 0; i < MiddleStr2.Length; i++)
				{
					if(MiddleStr2[i].Trim() != "")
						RowStrWanted2.Add(MiddleStr2[i].Trim());
				}
				if(RowStrWanted2.Count < Main.HorizontalNum)
				{
					Main.HorizontalNum = RowStrWanted2.Count;
					string TempStr = "";
					for(int i = 0; i < Main.HorizontalNum - 1; i++)
					{
						TempStr = TempStr + RowStrWanted2[i] + "0";
					}
					Main.EDITText.text = TempStr;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					Main.ProgEDITCusorH = 32f + Main.TextSize.x;
				}
				Main.EDITText.text = RowStrWanted2[Main.HorizontalNum - 1].Trim();
				Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));	 
			}
			else
			{
				for(int a = 0; a < Main.HorizontalNum - 1; a++)
				{
					FormerWords += RowStrWanted[a];
				}
				for(int a = Main.HorizontalNum; a < RowStrWanted.Count - 1; a++)
				{
					LatterWords += RowStrWanted[a];
				}
				Main.CodeForAll[Main.RealCodeNum - 1] = FormerWords + LatterWords;
				MiddleCodeEdit(Main.RealCodeNum, Main.VerticalNum);
				
				Main.Code01 = Main.TempCodeArray[0];
				Main.Code02 = Main.TempCodeArray[1];
				Main.Code03 = Main.TempCodeArray[2];
				Main.Code04 = Main.TempCodeArray[3];
				Main.Code05 = Main.TempCodeArray[4];
				Main.Code06 = Main.TempCodeArray[5];
				Main.Code07 = Main.TempCodeArray[6];
				Main.Code08 = Main.TempCodeArray[7];
				Main.Code09 = Main.TempCodeArray[8];
				
				string[] MiddleStr2 = Main.TempCodeArray[Main.VerticalNum - 1].Split(' ');
				List<string> RowStrWanted2 = new List<string>();
				for(int i = 0; i < MiddleStr2.Length; i++)
				{
					if(MiddleStr2[i].Trim() != "")
						RowStrWanted2.Add(MiddleStr2[i].Trim());
				}
				if(RowStrWanted2.Count < Main.HorizontalNum)
				{
					Main.HorizontalNum = RowStrWanted2.Count;
					string TempStr = "";
					for(int i = 0; i < Main.HorizontalNum - 1; i++)
					{
						TempStr = TempStr + RowStrWanted2[i] + "0";
					}
					Main.EDITText.text = TempStr;
					Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
					Main.ProgEDITCusorH = 32f + Main.TextSize.x;
				}
				Main.EDITText.text = RowStrWanted2[Main.HorizontalNum - 1].Trim();
				Main.TextSize = Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
			}
		}	
	}
	*/
	
	/// <summary>
	/// 重新编写的DeleteCode ()，实现代码的删除 陈晓威 2013-4-1
	/// </summary>
	public void DeleteCode()
	{
		int SelStart = Main.SelectStart > Main.SelectEnd? Main.SelectEnd:Main.SelectStart;
		int SelEnd = Main.SelectStart > Main.SelectEnd? Main.SelectStart:Main.SelectEnd;
		Main.CodeForAll.RemoveRange(SelStart,SelEnd - SelStart + 1);
		Main.TotalCodeNum=Main.CodeForAll.Count;
		if(Main.SelectStart == Main.SelectEnd)
		{
			if(Main.ProgEDITCusorH > 0)
			{
			    Main.SelStartCurV = Main.ProgEDITCusorV;
				Main.SelStartCurH = Main.ProgEDITCusorH - 1;
			}
			else if(Main.ProgEDITCusorV > 0)
			{
			    Main.SelStartCurV = Main.ProgEDITCusorV - 1;
				int row_begin = 0;
                int row_end;
                int row_length;
                if(Main.ProgEDITCusorV > 1)
                    row_begin = Main.SeparatePos[Main.ProgEDITCusorV - 2];
                else
                    row_begin = 0;		                 
                row_end = Main.SeparatePos[Main.ProgEDITCusorV - 1];		                 
                row_length = row_end - row_begin;          
				Main.SelStartCurH = row_length - 1;
			}
			else
			{
			    Main.SelStartCurV = 0;
				Main.SelStartCurH = 0;
			}
			Main.ProgEDITCusorV = Main.SelStartCurV;
			Main.ProgEDITCusorH = Main.SelStartCurH;
		}
		else if(Main.SelectStart > Main.SelectEnd)
		{
			if(Main.ProgEDITCusorH > 0)
				--Main.ProgEDITCusorH;
			else if(Main.ProgEDITCusorV > 0)
			{
				--Main.ProgEDITCusorV;
				int row_begin = 0;
				int row_end;
				int row_length;
				if(Main.ProgEDITCusorV > 1)
				   row_begin = Main.SeparatePos[Main.ProgEDITCusorV - 2];
				else
				   row_begin = 0;
				if(Main.ProgEDITCusorV > 0)
				    row_end = Main.SeparatePos[Main.ProgEDITCusorV - 1];
				else
					row_end = Main.SeparatePos[0];						
				row_length = row_end - row_begin;
				Main.ProgEDITCusorH = row_length - 1;
			}
			else						
			{
			    Main.ProgEDITCusorV = 0;
				Main.ProgEDITCusorH = 0;
			}
		}
		else
		{				
			Main.ProgEDITCusorV = Main.SelStartCurV;
			Main.ProgEDITCusorH = Main.SelStartCurH;
		}				
		if(Main.TotalCodeNum==0){
			Main.CodeForAll.Add(";");
			Main.TotalCodeNum++;
			Main.ProgEDITCusorV=0;
			Main.ProgEDITCusorH=0;
			Main.SelectStart=0;
			Main.SelectEnd=0;
			Main.StartRow=0;
			Main.EndRow=9;
		}
		else
		{
			if(SelStart > 0)
				Main.SelectStart = SelStart - 1;
			else
				Main.SelectStart = 0;
			for (int i=0; i<100000; i++)
                Main.SeparatePos[i] = 0;
			Softkey_Script.calcSepo(Main.CodeForAll,Main.SeparatePos,302f);
			while(Main.StartRow > 0 && Main.SeparatePos[Main.StartRow]==0)
			{
				--Main.StartRow;
				Main.EndRow = Main.StartRow + 9;
			}
			if(Main.StartRow > Main.ProgEDITCusorV)
			{
				Main.StartRow=Main.ProgEDITCusorV;
				Main.EndRow=Main.StartRow+9;
			}
		   Main.SelectEnd = Main.SelectStart;
		}
		//Debug.Log("Main.StartRow:"+Main.StartRow);
		//Debug.Log("Main.ProgEDITCusorV:"+Main.ProgEDITCusorV);
		//Debug.Log("Main.ProgEDITCusorH:"+Main.ProgEDITCusorH);
	   Main.ProgEDITFlip = 2;
	   Main.IsSelect = false;

	}
	
	
	
	/// <summary>
	/// 向上翻页，程序翻页部分待修改
	/// </summary>
	void PageUp () 
	{	
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT|| Main.ProgMDI)
			{
				//程序翻页
				if(Main.ProgEDITProg)
				{
					if(Main.StartRow > 0)	 
				   {	   	
						if(Main.StartRow >= 9)
						{
						    Main.StartRow -= 9;
							Main.EndRow = Main.StartRow + 9;
							Main.ProgEDITCusorV = Main.StartRow;
							Main.ProgEDITCusorH = 0;	
						    if(Main.StartRow > 0) 
							    Main.SelectStart = Main.SeparatePos[Main.StartRow - 1];
						    else
								Main.SelectStart = 0;
						    Main.SelectEnd = Main.SelectStart;
						}
						else
						{
							Main.StartRow = 0;
							Main.EndRow = 9;
							Main.ProgEDITCusorV = 0;
							Main.ProgEDITCusorH = 0;
							Main.SelectStart = 0;
						    Main.SelectEnd = Main.SelectStart;
						}
	                 }
	                    else
		                    Debug.Log("This is the first page!!!"); 

				}
				//列表翻页
				if(Main.ProgEDITList)
				{
					if(Main.CodeName[0] != "")
					{
						if(Main.ProgEDITFlip == 0)
							Main.ProgEDITFlip = 1;
						int name_index = Main.FileNameList.IndexOf(Main.CodeName[0]);
						if(name_index >= 0)
						{//1 level
							int current_page = name_index / 8;
							//下面还有页数则翻页，无则不翻页
							if(current_page > 0)
							{
								current_page--;
								name_index = current_page * 8 +1;
								string[] TempNameArray = new string[8];
								int[] TempSizeArray = new int[8]; 
								string[] TempDateArray = new string[8];
								for(int i = 0; i < 8; i++)
								{
									TempNameArray[i] = "";
									TempSizeArray[i] = 0;
									TempDateArray[i] = "";
								}
								int final_index = name_index + 7;
								if(final_index > Main.TotalListNum)
									final_index = Main.TotalListNum;
								int temp_index = -1;
								for(int i = name_index - 1; i < final_index; i++)
								{
									temp_index++;
									TempNameArray[temp_index] = Main.FileNameList[i];
									TempSizeArray[temp_index] = Main.FileSizeList[i];
									TempDateArray[temp_index] = Main.FileDateList[i];
								}
								for(int i = 0; i < 8; i++)
								{
									Main.CodeName[i] = TempNameArray[i];
									Main.CodeSize[i] = TempSizeArray[i];
									Main.UpdateDate[i] = TempDateArray[i];
								}
								Softkey_Script.Locate_At_Position(Main.RealListNum);
							}
						}//1 level
					}
				}
			}
		}
		
		if(Main.SettingMenu)
		{
			//刀偏向上翻页
			if(Main.OffSetTool)
			{
				if(Main.ToolOffSetPage_num>0)
					CooSystem_script.Tool_pageup();
			}
			if(Main.OffSetCoo)
			{
				if(Main.OffCooFirstPage == false)
				{
					Main.OffCooFirstPage = true;
					CooSystem_script.PageUp();
				}
			}
		}
	}
	
	/// <summary>
	/// 向上翻页，程序翻页部分待修改
	/// </summary>
	void PageDown () {
		
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT|| Main.ProgMDI)
			{
				//程序界面向下翻页
				if(Main.ProgEDITProg)
				{
					if(Main.SeparatePos[Main.StartRow + 9] != 0)
					{
					    Main.StartRow = Main.EndRow;
						Main.EndRow += 9;
						//Main.ProgEDITCusorV += 8;
						Main.ProgEDITCusorV=Main.StartRow;
						//while(Main.SeparatePos[Main.ProgEDITCusorV]==0)Main.ProgEDITCusorV--;
						Main.ProgEDITCusorH = 0;
						Main.SelectStart = Main.SeparatePos[Main.StartRow - 1];
						Main.SelectEnd = Main.SelectStart;
					}
				}
				//列表界面向下翻页
				if(Main.ProgEDITList)
				{
					if(Main.CodeName[0] != "")
					{
						if(Main.ProgEDITFlip == 0)
							Main.ProgEDITFlip = 1;
						int name_index = Main.FileNameList.IndexOf(Main.CodeName[0]);
						if(name_index >= 0)
						{//2 level
							int total_page = (Main.TotalListNum - 1) / 8;
							int current_page = name_index / 8;
							//下面还有页数则翻页，无则不翻页
							if(total_page > current_page)
							{
								current_page++;
								name_index = current_page * 8 +1;
								string[] TempNameArray = new string[8];
								int[] TempSizeArray = new int[8]; 
								string[] TempDateArray = new string[8];
								for(int i = 0; i < 8; i++)
								{
									TempNameArray[i] = "";
									TempSizeArray[i] = 0;
									TempDateArray[i] = "";
								}
								int final_index = name_index + 7;
								if(final_index > Main.TotalListNum)
									final_index = Main.TotalListNum;
								int temp_index = -1;
								for(int i = name_index - 1; i < final_index; i++)
								{
									temp_index++;
									TempNameArray[temp_index] = Main.FileNameList[i];
									TempSizeArray[temp_index] = Main.FileSizeList[i];
									TempDateArray[temp_index] = Main.FileDateList[i];
								}
								for(int i = 0; i < 8; i++)
								{
									Main.CodeName[i] = TempNameArray[i];
									Main.CodeSize[i] = TempSizeArray[i];
									Main.UpdateDate[i] = TempDateArray[i];
								}
								Softkey_Script.Locate_At_Position(Main.RealListNum);
							}
						}//2 level
					}
				}
			}
		}
		
		if(Main.SettingMenu)
		{
			if(Main.OffSetTool)
			{
				//刀偏向下翻页
				if(Main.ToolOffSetPage_num < 49)
					CooSystem_script.Tool_pagedown();
			}
			if(Main.OffSetCoo)
			{
				if(Main.OffCooFirstPage)
				{
					Main.OffCooFirstPage = false;
					CooSystem_script.PageDown();
				}
			}
		}
	}
	

	
	/// <summary>
	/// 光标上移按钮
	/// </summary>
	void UpButton () {
		
		if(Main.ProgMenu)
		{
			if(Main.ProgAUTO)
			{
				if(Main.autoSelecedProgRow>0)Main.autoSelecedProgRow--;
				Main.AutoDisplayFindRows(Main.autoSelecedProgRow,Main.autoDisplayNormal);
			}
			
			
			if(Main.ProgEDIT|| Main.ProgMDI)
			{
				//编辑程序时
				if(Main.ProgEDITProg)
				{
					bool NumInNine = false;
					EditProgUp();
				}
			}
		}
		
		if(Main.SettingMenu)
		{
			//刀偏界面上移
			if(Main.OffSetTool)
			{
				CooSystem_script.tool_up();
			}
			//设定界面上移
			if(Main.OffSetSetting)
			{
				CooSystem_script.argu_up();
			}
			if(Main.OffSetCoo)
			{
				CooSystem_script.Up();
			}
		}
	}
	
	/// <summary>
	/// 光标下移按钮
	/// </summary>
	void DownButton () {
		
		if(Main.ProgMenu)
		{
			if(Main.ProgAUTO)
			{
			//Debug.Log(Main.autoSelecedProgRow+"nowr");
			//Debug.Log(Main.AUTOSeparatePos[Main.autoSelecedProgRow]+"nowsp");
			//Debug.Log("realrow:"+Main.AutoRunItemRows[1]);
			int realrow=Main.AutoRunItemRows[1];
			if(Main.AUTOSeparatePos[realrow+1]!=0)Main.autoSelecedProgRow++;
			Main.AutoDisplayFindRows(Main.autoSelecedProgRow,Main.autoDisplayNormal);
			}
			if(Main.ProgEDIT|| Main.ProgMDI)
			{
				//编辑程序时
				//分两种情况：如果输入以“O”开始，则执行“O检索”功能；否则执行光标下移功能.
				if(Main.ProgEDITProg)
				{
					if(Main.InputText.StartsWith("O"))
					{
						Softkey_Script.O_Search();
						Softkey_Script.Locate_At_Position(Main.RealListNum);
					}
					else
					{
						bool NumInNine = false;
						//Debug.Log("hello");
						EditProgDown();
					}
				}
				//O检索时
				//分为两种情况：光标选择模式开或者关
				//当光标模式关时，如果有输入，且以"O"开始，则进行相应的O检索操作；
				//Todo: 当光标模式开时，上下移动光标；
				if(Main.ProgEDITList)
				{
					if(Main.InputText.StartsWith("O"))
					{
						Softkey_Script.O_Search();
						Softkey_Script.Locate_At_Position(Main.RealListNum);
					}	
				}
			}
			
		}
		
		if(Main.SettingMenu)
		{
			//刀偏界面下移
			if(Main.OffSetTool)
			{
				CooSystem_script.tool_down();
			}
			//设定界面下移
			if(Main.OffSetSetting)
			{
				CooSystem_script.argu_down();
			}

			if(Main.OffSetCoo)
			{
				CooSystem_script.Down();
			}	
		}
	}

	/// 重新编写的光标移动函数，实现光标的上、下、左、右移动 董帅 2013-4-2
	/// </summary>
	void EditProgRight () 
	{
	    
		int row_begin = 0;
		int row_end;
		int row_length;
		//Debug.Log(Main.SelectStart+" "+Main.TotalCodeNum);
		if(Main.ProgEDITCusorV > 0)
		   row_begin = Main.SeparatePos[Main.ProgEDITCusorV - 1];
		else
		   row_begin = 0;
		row_end = Main.SeparatePos[Main.ProgEDITCusorV];
		row_length = row_end - row_begin;
		/// <remarks ProgEDITCusorH，ProgEDITCusorV均从0开始记
	   // Debug.Log(Main.ProgEDITCusorH +"**"+Main.ProgEDITCusorV);
	   // Debug.Log("len:"+(row_length-1));
		
		if(Main.ProgEDITCusorH < row_length - 1)
		{
		    ++Main.ProgEDITCusorH;
			if(Main.IsSelect)
			    ++Main.SelectEnd;
			else
			{
				Main.SelectStart = Main.SelectEnd;   
				++Main.SelectStart;
				++Main.SelectEnd;
			}
		}
		else
		{
			//Debug.Log("bs"+Main.SelectStart);
		   // Debug.Log("be"+Main.SelectEnd);
			//Debug.Log("be"+Main.TotalCodeNum);
			if(Main.SelectEnd< Main.TotalCodeNum-1)
			{
			    ++Main.ProgEDITCusorV;
				if(Main.IsSelect)
				    ++Main.SelectRowNum;
				if(Main.ProgEDITCusorV == Main.EndRow)
				{
				    ++Main.EndRow;
				    ++Main.StartRow;
				}
				Main.ProgEDITCusorH = 0;
				if(Main.IsSelect)
			       ++Main.SelectEnd;
				else
				{
				   ++Main.SelectStart;
				   ++Main.SelectEnd;
				}
			}
			else
			{
			    Debug.Log("This is the end!!!");
			}
		}
		//Debug.Log("s"+Main.SelectStart);
		//Debug.Log("e"+Main.SelectEnd);
//		if(Main.ProgEDITCusorV > 0)
//		{
//		       Main.SelectStart = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;				
//		       Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
//			   
//	    }
//	    else
//		{
//		       Main.SelectStart =  Main.ProgEDITCusorH;
//		       Main.SelectEnd =  Main.ProgEDITCusorH;
//		}	
			
	}
	void EditProgLeft () 
	{
	    int row_begin = 0;
		int row_end;
		int row_length;
		bool IsReverse = false;
		if(Main.ProgEDITCusorV > 1)
		   row_begin = Main.SeparatePos[Main.ProgEDITCusorV - 2];
		else
		   row_begin = 0;
		if(Main.ProgEDITCusorV > 0)
		    row_end = Main.SeparatePos[Main.ProgEDITCusorV - 1];
		else
			row_end = Main.SeparatePos[0];
		
		row_length = row_end - row_begin;
		/// <remarks ProgEDITCusorH，ProgEDITCusorV均从0开始记
		if(Main.ProgEDITCusorH > 0)
		{
		    --Main.ProgEDITCusorH;
			if(Main.IsSelect)
			       --Main.SelectEnd;
			else
			{
		        Main.SelectStart = Main.SelectEnd;   
				--Main.SelectStart;
				--Main.SelectEnd;
			}
		}
		else
		{
			if(Main.ProgEDITCusorV > 0)
			{
			    --Main.ProgEDITCusorV;
				if(Main.IsSelect)
				{
					if(Main.SelectRowNum > 0 && !IsReverse)
					    --Main.SelectRowNum;
					else
					{
						++Main.SelectRowNum;
						IsReverse = true;
					}
				}
				if(Main.StartRow > 0 && Main.ProgEDITCusorV < Main.StartRow) 
				{
				    --Main.StartRow;
				    --Main.EndRow;
				}
				
				Main.ProgEDITCusorH = row_length - 1;
				if(Main.IsSelect)
			       --Main.SelectEnd;
				else
				{
				   Main.SelectStart = Main.SelectEnd;
				   --Main.SelectStart;
				   --Main.SelectEnd;
				}
			}
			else
			{
			    Debug.Log("This is the start!!!");
			}
		}
		//Debug.Log("Main.ProgEDITCusorV:"+Main.ProgEDITCusorV);
		//Debug.Log("Main.ProgEDITCusorH:"+Main.ProgEDITCusorH);
//		if(Main.ProgEDITCusorV > 0)
//		{
//		       Main.SelectStart = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
//		       Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
//	    }
//	    else
//		{
//		       Main.SelectStart =  Main.ProgEDITCusorH;
//		       Main.SelectEnd =  Main.ProgEDITCusorH;
//		}	
			
	}
	void EditProgDown () 
	{
	    
		int row_begin = 0;
		int row_end;
		int row_length;
		//Debug.Log((Main.ProgEDITCusorV+1)+" V "+(Main.SeparatePos[Main.ProgEDITCusorV+1]-1)+" sp "+Main.TotalCodeNum);
		//int poss=Main.SelectEnd;
		//while(poss<(Main.CodeForAll.Count-1)&&Main.CodeForAll[poss]!=";")poss++;
		
		//Debug.Log(Main.SeparatePos[Main.ProgEDITCusorV]+"()"+(Main.TotalCodeNum-1)+"&&"+(Main.CodeForAll.Count-1));
		if(Main.SeparatePos[Main.ProgEDITCusorV]-1 < Main.CodeForAll.Count-1)
		//if(poss<Main.CodeForAll.Count-1)
		{	   	
		   
		 if(Main.ProgEDITCusorV >= 0)
		   row_begin = Main.SeparatePos[Main.ProgEDITCusorV];
		// else
		 //  row_begin = 0;
		   row_end = Main.SeparatePos[Main.ProgEDITCusorV+1];
		   //计算出下一行的单词个数
		   row_length = row_end - row_begin;
			//Debug.Log(row_length+"!!");
		   if(Main.ProgEDITCusorV == (Main.EndRow-1))
			{
		        ++Main.StartRow;
				++Main.EndRow;	
				
			}
		   //Debug.Log(row_begin+"b");
			//Debug.Log(row_end+"e");
		   if(Main.ProgEDITCusorH < row_length)
			{
		       ++Main.ProgEDITCusorV;
			   
			   if(Main.IsSelect)
			   {
				 //Main.ProgEDITCusorH = row_length - 1;
				 ++Main.SelectRowNum;
			   }
			}
		   else
		   {
			    ++Main.ProgEDITCusorV;
				if(Main.IsSelect)
				    ++Main.SelectRowNum;
				Main.ProgEDITCusorH = row_length - 1;
		   }
		   //Debug.Log(Main.ProgEDITCusorV);
		   //Debug.Log(Main.ProgEDITCusorH);
		   //Debug.Log("asdf");
		   //Main.SelectStart = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		   //Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		   if(Main.IsSelect)
				Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		   else
		   {
				Main.SelectStart = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		        Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		   }
		   //Debug.Log(Main.SelectStart);
		   //Debug.Log(Main.SelectEnd);
		}
		else
		   Debug.Log("This is the last line!!!"); 	
		//Debug.Log(Main.ProgEDITCusorH +"**"+Main.ProgEDITCusorV);
			
	}
	void EditProgUp () 
	{
		int row_begin;
		int row_end;
		int row_length;
		bool IsReverse = false;
		if(Main.ProgEDITCusorV > 1)
		    row_begin = Main.SeparatePos[Main.ProgEDITCusorV - 2];
		else
		    row_begin = 0;
		if(Main.ProgEDITCusorV > 0)
		    row_end = Main.SeparatePos[Main.ProgEDITCusorV - 1];
		else
		    row_end = Main.SeparatePos[0];
		
		row_length = row_end - row_begin;
		if(Main.ProgEDITCusorV > 0)
		{
		    if(Main.ProgEDITCusorV == Main.StartRow)
		    {
		        if(Main.StartRow > 0)
		        {
		            --Main.StartRow;
		            --Main.EndRow;
		        }
		    }
		    if(Main.ProgEDITCusorH < row_length)
		        --Main.ProgEDITCusorV;
		    else
		    {
		        --Main.ProgEDITCusorV;
		        Main.ProgEDITCusorH = row_length - 1;
		    }
			if(Main.IsSelect)
			{
				if(Main.SelectRowNum > 0 && !IsReverse)
				    --Main.SelectRowNum;
				else
				{
					++Main.SelectRowNum;
					IsReverse = true;
				}
			}
			
		    if(Main.ProgEDITCusorV > 0)
		    {
		        if(Main.IsSelect)
				{
					Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV -  1] + Main.ProgEDITCusorH;
				}
		        else
		        {
		            Main.SelectStart = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		            Main.SelectEnd = Main.SeparatePos[Main.ProgEDITCusorV - 1] + Main.ProgEDITCusorH;
		        }
		    }
		    else
		    {
		        if(Main.IsSelect)
		            Main.SelectEnd = Main.ProgEDITCusorH;
		        else
		        {
		            Main.SelectStart = Main.ProgEDITCusorH;
		            Main.SelectEnd = Main.ProgEDITCusorH;
		        }
		    }
		     //  Debug.Log(Main.ProgEDITCusorV);
		     //  Debug.Log(Main.ProgEDITCusorH);
		    //   Debug.Log("asdf");
		    //	Debug.Log(Main.SelectStart);
		    //   Debug.Log(Main.SelectEnd);
		}
		else
		    Debug.Log("This is the first line!!!");
		//Debug.Log(Main.ProgEDITCusorH +"**"+Main.ProgEDITCusorV);
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
