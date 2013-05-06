using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SoftkeyModule : MonoBehaviour {
	ControlPanel Main;
	CooSystem CooSystem_script;
	MDIEditModule MDIEdit_Script;
	//使用新编写的NC代码格式化文件 董帅 2013-3-30
	NCCodeFormat NCCodeFormat_Script;
	//位置界面功能完善---宋荣 ---03.09
	PositionModule Pos_Script;
	MDIInputModule MDIInput_Script;
	bool preSetSelected=false;
	//位置界面功能完善---宋荣 ---03.09
	//Improvement for the RPOG part by Eric---03.28
	string document_path = "";
	bool file_open = false;
	public bool EditList_display_switcher = false;
	Dictionary<char,float> strlenmap;
	//Improvement for the RPOG part by Eric---03.28
	
	void Awake ()
	{
		document_path = Application.dataPath + "/Resources/Gcode/";
	}
	
	// Use this for initialization
	void Start () {
		Main = gameObject.GetComponent<ControlPanel>();
		CooSystem_script = gameObject.GetComponent<CooSystem>();
		MDIEdit_Script = gameObject.GetComponent<MDIEditModule>();
		//位置界面功能完善---宋荣 ---03.09
		Pos_Script=gameObject.GetComponent<PositionModule>();
	    MDIInput_Script=gameObject.GetComponent<MDIInputModule>();
		//使用新编写的NC代码格式化文件 董帅 2013-3-30
		NCCodeFormat_Script = gameObject.GetComponent<NCCodeFormat>();
		//位置界面功能完善---宋荣 ---03.09
		FileInfoInitialize();
		//calsize字典初始化 陈晓威
		StrLenMapInitialize();
	}
	
	public void Softkey () 
	{
		//屏幕下方功能软键++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++	
		if (GUI.Button(new Rect(20f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), "<"))            
		{
			if(Main.ScreenPower)
				PreviousPage();
		}
		
		if (GUI.Button(new Rect(90f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ""))           	
		{
			if(Main.ScreenPower)
				FirstButton();	
		}
		
		if (GUI.Button(new Rect(180f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ""))            	
		{
			if(Main.ScreenPower)
				SecondButton();
		}
		
		if (GUI.Button(new Rect(270f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ""))            
		{
			if(Main.ScreenPower)
				ThirdButton();
		}
		
		if (GUI.Button(new Rect(360f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ""))            
		{
			if(Main.ScreenPower)
				FourthButton();
		}
		
		if (GUI.Button(new Rect(450f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ""))            
		{
			if(Main.ScreenPower)
				FifthButton();
		}
		
		if (GUI.Button(new Rect(520f/1000f*Main.width, 460f/1000f*Main.height, 40f/1000f*Main.width, 40f/1000f*Main.height), ">"))            
		{
			if(Main.ScreenPower)
				NextPage();
		}
	}
	
	//向前翻页软键
	void PreviousPage () {
		//程序界面时按下
		//宋荣
		if(Main.PosMenu)
		{
			if(Main.operationBottomScrInitial)
			{
				if(Main.statusBeforeOperation==1)
				{
					Main.RelativeCoo=false;
				    Main.AbsoluteCoo=true;
				    Main.GeneralCoo=false;
				}
				if(Main.statusBeforeOperation==2)
				{
					Main.RelativeCoo=true;
				    Main.AbsoluteCoo=false;
				    Main.GeneralCoo=false;
				}
				if(Main.statusBeforeOperation==3)
				{
					Main.RelativeCoo=false;
				    Main.AbsoluteCoo=false;
				    Main.GeneralCoo=true;
				}
				Main.operationBottomScrInitial=false;
				Main.posOperationMode=false;
				Pos_Script.xBlink=false;
				Pos_Script.yBlink=false;
				Pos_Script.zBlink=false;
				MDIInput_Script.isXSelected=false;
			    MDIInput_Script.isYSelected=false;
				MDIInput_Script.isZSelected=false;
				//Debug.Log("operationeInitial is true");
			}
			
			else if(Main.operationBottomScrExecute)
			{
				//Debug.Log("operationexec");
				Main.operationBottomScrInitial=true;
				Main.operationBottomScrExecute=false;
				Main.runtimeIsBlink=false;
				Main.partsNumBlink=false;
				
			}
		}
		//宋荣
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT)
			{
				if(Main.ProgEDITProg)
				{
					if(Main.ProgEDITFlip == 1)
					{
						Main.ProgEDITFlip = 0;
					}
					else if(Main.ProgEDITFlip == 2)
						Main.ProgEDITFlip = 1;
					else if(Main.ProgEDITFlip == 3)
						Main.ProgEDITFlip = 2;
					//内容--程序编辑界面下，程序底部按钮有8种显示方式，因此ProgEDITFlip的值由0到7，在向前翻页按钮命令下，ProgEDITFlip的变化如下
					//姓名--刘旋
					//日期2013-3-14
					else if (Main.ProgEDITFlip==4)
				        Main.ProgEDITFlip=3;
			        else if (Main.ProgEDITFlip==5)
				        Main.ProgEDITFlip=2;
			        else if (Main.ProgEDITFlip==6)
				        Main.ProgEDITFlip=5;
			        else if (Main.ProgEDITFlip==7)
				        Main.ProgEDITFlip=2;//变化内容到此
					else if(Main.ProgEDITFlip==8)
						Main.ProgEDITFlip=4;
				}
				if(Main.ProgEDITList)
				{
					if(Main.ProgEDITFlip == 1)
					{
						Main.ProgEDITFlip = 0;
						Main.ProgEDITProg = true;
						Main.ProgEDITList = false;
					}
				}
			}
			//内容--AUTO模式下，程序界面向前翻页的功能
			//姓名--刘旋，时间--2013-3-25
			if(Main.ProgAUTO)
			{
				if(Main.ProgAUTOFlip==1)//“操作”页返回“程序”页
					Main.ProgAUTOFlip=0;
				//内容--AUTO模式下，程序界面向前翻页按钮功能的修改，姓名--刘旋，时间--2013-4-9
				if(Main.ProgAUTOFlip==2)//“绝对”页返回“程序”页
					Main.ProgAUTOFlip=0;
				if(Main.ProgAUTOFlip==3)//“当前段”页返回“程序”页
					Main.ProgAUTOFlip=0;
				if(Main.ProgAUTOFlip==4)//“相对”页返回“程序”页
					Main.ProgAUTOFlip=0;
				if(Main.ProgAUTOFlip==5)//“下一段”页返回“程序”页
					Main.ProgAUTOFlip=0;
			}//增加内容到此
		}
		
		//设置界面时按下
		if(Main.SettingMenu)
		{
			if(Main.OffSetTool)
			{
				if(Main.OffSetTwo)
				{
					Main.OffSetTwo = false;
					Main.OffSetOne = true;
				}
			}
			if(Main.OffSetSetting)
			{
				if(Main.OffSetTwo)
				{
					Main.OffSetTwo = false;
					Main.OffSetOne = true;
				}
			}
			if(Main.OffSetCoo)
			{
				if(Main.OffSetTwo)
				{
					Main.OffSetTwo = false;
					Main.OffSetOne = true;
				}
			}
		}
	}
	
	//软键 Button1
	void FirstButton () {
		//位置界面时按下
		if(Main.PosMenu)
		{
			//绝对坐标
			//宋荣
			if(Main.posOperationMode)
			{
				if(Main.operationBottomScrInitial)
			    {
				   if(Main.statusBeforeOperation==1)
					{
						Main.operationBottomScrInitial=false;
				        Main.operationBottomScrExecute=true;
				        preSetSelected=true;
					}
				   if((Main.statusBeforeOperation==2||Main.statusBeforeOperation==3)&&(MDIInput_Script.isXSelected||MDIInput_Script.isYSelected||MDIInput_Script.isZSelected))
				   {
					    Pos_Script.preSetRelativeCoo=CooSystem_script.relative_pos;
						if(MDIInput_Script.isXSelected)
							Debug.Log("预置相对坐标x成功");
						if(MDIInput_Script.isYSelected)
							Debug.Log("预置相对坐标y成功");
						if(MDIInput_Script.isZSelected)
							Debug.Log("预置相对坐标z成功");
						MDIInput_Script.isXSelected=false;
						MDIInput_Script.isYSelected=false;
						MDIInput_Script.isZSelected=false;
				     	
				   }
				   //Main.operationBottomScrExecute=false;
			     }
				return;
			}
			//宋荣
			Main.AbsoluteCoo = true;
			Main.RelativeCoo = false;
			Main.GeneralCoo = false;
		}
		//程序界面时按下
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT)
			{
				//程序
				//内容--程序和列表菜单下，第一个按钮有不同的功能，因此要分情况定义
				//姓名--刘旋
				//日期2013-3-14
				if(Main.ProgEDITProg)
				{
					//Debug.Log(Main.ProgEDITFlip);
					//选择
					if(Main.ProgEDITFlip==2)
					{
				       Main.IsSelect=true;
						//Debug.Log("bV:"+Main.ProgEDITCusorV);
				        //Debug.Log("bH:"+Main.ProgEDITCusorH);
						
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
						//Debug.Log("V:"+Main.SelStartCurV);
				       // Debug.Log("H:"+Main.SelStartCurH);
					    Main.ProgEDITFlip=3;
					}
			       else if ((Main.ProgEDITFlip==3)||(Main.ProgEDITFlip==50))
				   { //取消选择
					   Main.IsSelect=false;	
					   Main.SelectStart = Main.SelectEnd;
				       Main.ProgEDITFlip=2;
					}
		           else if (Main.ProgEDITFlip==5)
				   {
				      //取消全选择 陈晓威
						Main.IsSelect=false;
						Main.ProgEDITFlip=2;
						Main.SelectStart=0;
						Main.ProgEDITCusorV=Main.SelStartCurV;
						Main.ProgEDITCusorH=Main.SelStartCurH;
						if(Main.ProgEDITCusorV>0)
							Main.SelectStart=Main.SeparatePos[Main.ProgEDITCusorV-1]+Main.ProgEDITCusorH;
						else
							Main.SelectStart=Main.ProgEDITCusorH;
						Main.SelectEnd=Main.SelectStart;	
						//Main.SelectEnd=Main.SelectStart;	
						//Main.StartRow = 0;
						//Main.EndRow = 9;
				   } else if (Main.ProgEDITFlip==7){
				      //粘贴buffer 陈晓威
						if(Main.CodeForAll.Count==1&&Main.CodeForAll[0]==";")
						{
							Main.CodeForAll.RemoveAt(0);
							Main.TotalCodeNum--;
						}				
						int pos=Main.SelectStart+1;
						int buffer_index=0;
						while(buffer_index<Main.CodeBuffer.Count){
							if(pos>Main.TotalCodeNum-1)
							Main.CodeForAll.Add(Main.CodeBuffer[buffer_index]);
							else
							Main.CodeForAll.Insert(pos++,Main.CodeBuffer[buffer_index]);
							buffer_index++;
						}
						Main.TotalCodeNum=Main.CodeForAll.Count;
						Main.ProgEDITFlip=2;
						//Main.CodeBuffer
					}
					else if ((Main.ProgEDITFlip==6)||(Main.ProgEDITFlip==4))//内容--增加程序底部按钮显示“8”，用于实现“替换”功能，姓名--刘旋，时间--2013-3-20
						Main.ProgEDITFlip=8;
				}
				if(Main.ProgEDITList)
				{//变化的内容到此
					if(Main.ProgEDITFlip == 0)
				     {
					 	Main.ProgEDITProg = true;
					 	Main.ProgEDITList = false;
				     }
				}	
			}
			if(Main.ProgAUTO)//内容--AUTO模式下，程序界面，第一个按钮的功能，姓名--刘旋，时间--2013-4-9
			{
				if(Main.ProgAUTOFlip==3)//“当前段”页，按下“程序”按钮，转到“程序”页
					Main.ProgAUTOFlip=0;
				if(Main.ProgAUTOFlip==5)//“下一段”页，按下“程序”按钮，转到“程序”页
					Main.ProgAUTOFlip=0;
				if(Main.ProgAUTOFlip==4)//“相对”页，按下“绝对”按钮，转到“绝对”页
					Main.ProgAUTOFlip=2;
			}
			if(Main.ProgMDI)//内容--MDI模式下，程序界面，第一个按钮的功能，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgMDIFlip=1;
				MDIEdit_Script.CodeEdit();
				//将光标的位置索引初始化
				Main.ProgEDITCusorH=0;
				Main.ProgEDITCusorV=0;
				Main.SelectStart = 0;
				Main.SelectEnd = 0;
			}
			if(Main.ProgDNC)//内容--DNC模式下，程序界面，第一个按钮的功能，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgDNCFlip=0;
			}
			if(Main.ProgHAN)//内容--HAN模式下，程序界面，第一个按钮的功能，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgHANFlip=0;
			}
			if(Main.ProgJOG || Main.ProgREF)//内容--JOG和REF模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgSharedFlip=0;
			}
		}
		//设置界面时按下
		if(Main.SettingMenu)
		{
			if(Main.OffSetOne)
			{
				Main.OffSetTool = true;
				Main.OffSetSetting = false;
				Main.OffSetCoo = false;
			}
			//刀偏界面号搜索
			if(Main.OffSetTool && Main.OffSetTwo)
			{
				if(Main.InputText != "")
				{
					CooSystem_script.SearchToolNo(Main.InputText);
					Main.InputText = "";
					Main.CursorText.text = Main.InputText;
					Main.ProgEDITCusorPos = 57f;
				}
			}
			else if(Main.OffSetCoo)
			{
				if(Main.InputText != "")
				{
					CooSystem_script.SearchNo(Main.InputText);
					Main.InputText = "";
					Main.CursorText.text = Main.InputText;
					Main.ProgEDITCusorPos = 57f;
				}
			}	
		}
		
		if(Main.MessageMenu)
		{
			Main.MessageFlip=0;
		}
	}
	
	//软键 Button2
	void SecondButton () {
		//位置界面时按下
		if(Main.PosMenu)
		{
			//相对坐标
			//宋荣
			if(Main.posOperationMode)
			{
				if(Main.operationBottomScrInitial)
			    {
				   if((Main.statusBeforeOperation==2||Main.statusBeforeOperation==3)&&(MDIInput_Script.isXSelected||MDIInput_Script.isYSelected||MDIInput_Script.isZSelected))
				   {
						if(MDIInput_Script.isXSelected)
						{
					        CooSystem_script.relative_pos.x=0;
							Debug.Log("归零x成功");
						}
						
						if(MDIInput_Script.isYSelected)
						{		
							CooSystem_script.relative_pos.y=0;
							Debug.Log("归零Y成功");
						}
						if(MDIInput_Script.isZSelected)
						{
							CooSystem_script.relative_pos.z=0;
							Debug.Log("归零Z成功");
						}
						MDIInput_Script.isXSelected=false;
						MDIInput_Script.isYSelected=false;
						MDIInput_Script.isZSelected=false;
				   }
			     }
				return;
			}
			//宋荣
			Main.AbsoluteCoo = false;
			Main.RelativeCoo = true;
			Main.GeneralCoo = false;
		}
		//程序界面时按下
		if(Main.ProgMenu)
		{//1 level
			if(Main.ProgEDIT)
			{//2 level
				if(Main.ProgEDITProg)
				{
					
					//到最后 陈晓威
					if (Main.ProgEDITFlip==3){
					calcSepo(Main.CodeForAll,Main.SeparatePos,302f);
				     Main.ProgEDITFlip=50;
					 Main.SelectEnd=Main.CodeForAll.Count-1;
					int j=0; 
					while(Main.SeparatePos[j]<Main.CodeForAll.Count&&Main.SeparatePos[j]!=0)j++;
					//j--;
					Main.StartRow=j/9*9;
					Main.EndRow=Main.StartRow+9;
					Main.ProgEDITCusorV=j;
					Main.ProgEDITCusorH=Main.CodeForAll.Count-Main.SeparatePos[j-1];
					//Debug.Log("lastV:"+Main.ProgEDITCusorV);
					//Debug.Log("lastH:"+Main.ProgEDITCusorH);
						
						
					}
					
					//全选择 陈晓威
					else if (Main.ProgEDITFlip==2){
				     Main.ProgEDITFlip=5;
					 Main.SelStartCurH=Main.ProgEDITCusorH;
					 Main.SelStartCurV=Main.ProgEDITCusorV;
					 Main.SelectStart=0;
					 Main.SelectEnd=Main.CodeForAll.Count-1;
					
					}
					else if (Main.ProgEDITFlip==0)
					{
						Main.ProgEDITList=true;
						Main.ProgEDITProg=false ;
					}	
					//O检索
					else if(Main.ProgEDITFlip==1)
					{
						O_Search();
						Locate_At_Position(Main.RealListNum);
						Main.needRecalc=true;
					}
				}
				if(Main.ProgEDITList)
				{
					if(Main.ProgEDITFlip == 0)
					{
						if(Main.at_position >= 0)
						{
							Locate_At_Position(Main.RealListNum);
						}
					}
					if(Main.ProgEDITFlip == 1)
					{
						//O检索
						O_Search();	
						Locate_At_Position(Main.RealListNum);
						Main.needRecalc=true;
					}
				}
			}//2 level
			
			//内容--AUTO模式下，程序界面，第二个按钮的功能
			//姓名--刘旋，时间--2013-3-25
			if (Main.ProgAUTO)
			{
				if(Main.ProgAUTOFlip==0)//“程序”页，按下“检测”按钮，转到“绝对”页
				{
					Main.ProgAUTOFlip=2;
				}
				//内容--AUTO模式下，程序界面，第二个按钮功能的修改，姓名--刘旋，时间--2013-4-9
				else if(Main.ProgAUTOFlip==2)//“绝对”页，按下“相对”按钮，转到“相对”页
				{
					Main.ProgAUTOFlip=4;
				}
				else if(Main.ProgAUTOFlip==3)//内容--“当前段”页，按下“检索”按钮，转到“绝对”页，姓名--刘旋，时间--2013-4-11
				{
					Main.ProgAUTOFlip=2;
				}
				else if(Main.ProgAUTOFlip==5)//内容--“下一段”页，按下“检索”按钮，转到“绝对”页，姓名--刘旋，时间--2013-4-11
				{
					Main.ProgAUTOFlip=2;
				}
			}//增加内容到此
			
			if(Main.ProgMDI)
			{
				Main.ProgMDIFlip=1;
				MDIEdit_Script.CodeEdit();
				//将光标的位置索引初始化
				Main.ProgEDITCusorH=0;
				Main.ProgEDITCusorV=0;
				Main.SelectStart = 0;
				Main.SelectEnd = 0;
			}	
		}//1 level
		
		if(Main.SettingMenu)
		{
			if(Main.OffSetOne)
			{
				Main.OffSetTool = false;
				Main.OffSetSetting = true;
				Main.OffSetCoo = false;
			}
			else if(Main.OffSetCoo)
			{
				CooSystem_script.Measure(Main.InputText);
				Main.InputText = "";
				Main.CursorText.text = Main.InputText;
				Main.ProgEDITCusorPos = 57f;
			}
		}
		
		if(Main.MessageMenu)
		{
			Main.MessageFlip=1;
		}
	}
	
	//软键 Button3
	void ThirdButton () 
	{
		//位置界面时按下
		if(Main.PosMenu)
		{
			//综合显示
			//宋荣
			if(Main.posOperationMode)
				return;
			//宋荣
			Main.AbsoluteCoo = false;
			Main.RelativeCoo = false;
			Main.GeneralCoo = true;
		}
		//程序界面时按下
		if(Main.ProgMenu)
		{
			//程序界面下，第三个按钮功能的增加
			//姓名--刘旋
		   //日期2013-3-14
			if(Main.ProgEDIT)
			{
				
				//向下检索 ---陈晓威 董帅
				if(Main.ProgEDITFlip == 1)
				{
				    Main.NotFoundWarn = false;
					string SearchWord = Main.EDITText.text.Trim();
					//Debug.Log("l"+SearchWord.Length);
			        //Debug.Log(SearchWord);
					if(SearchWord.Equals(""))
					{
						Main.NotFoundWarn = false;
						
					}
					else
					{
					int CurIndex =0;
					//bool FoundInPage= false;
					calcSepo(Main.CodeForAll,Main.SeparatePos,302f);
					//Debug.Log("OKKK");
					CurIndex = Main.SelectStart + 1;
					int irow=Main.ProgEDITCusorV;
					for(CurIndex=Main.SelectStart + 1;CurIndex<Main.CodeForAll.Count;CurIndex++)
					{
						if(CurIndex>=Main.SeparatePos[irow])irow++;
						if(Main.CodeForAll[CurIndex].IndexOf(SearchWord) > -1)break;	
					}
					//Debug.Log("irowowo"+irow);	
					//如果已经找到
					if(CurIndex<Main.CodeForAll.Count)
					{
						Main.NotFoundWarn = false;
						Main.StartRow=irow/9*9;
						Main.EndRow=Main.StartRow+9;
						Main.SelectStart=CurIndex;
						Main.SelectEnd=Main.SelectStart;
						Main.ProgEDITCusorV=irow;
						Main.MDIProgEDITCusorH=Main.SelectStart-Main.SeparatePos[irow-1];	
					}
					else
					{//找不到的情况
						Main.StartRow=irow/9*9;
						Main.EndRow=Main.StartRow+9;
						Main.NotFoundWarn = true;
						Main.SelectStart = Main.CodeForAll.Count - 1;
						Main.SelectEnd = Main.SelectStart;
						Main.ProgEDITCusorV=irow;
						Main.MDIProgEDITCusorH=Main.SelectStart-Main.SeparatePos[irow-1];			
					}
						
					}
				
				}
				
				//选择 复制
				if(Main.ProgEDITFlip == 3 ||Main.ProgEDITFlip == 5||Main.ProgEDITFlip == 50)
				{
				    Debug.Log("Main.ProgEDITFlip:"+Main.ProgEDITFlip);
					Main.CodeBuffer = new List<string>();
					Main.CodeBuffer.Clear();
					int SelStart = Main.SelectStart > Main.SelectEnd? Main.SelectEnd:Main.SelectStart;
					int SelEnd = Main.SelectStart > Main.SelectEnd? Main.SelectStart:Main.SelectEnd;
					for(int i = SelStart;i <= SelEnd;++i)
					Main.CodeBuffer.Add(Main.CodeForAll[i]);
					Main.ProgEDITFlip = 2;
				    Main.IsSelect = false;
					Main.SelectStart=Main.SelectEnd;
					
				}
				
				
				
				
				
				if(Main.ProgEDITProg)
				{
					
				}
				if(Main.ProgEDITList)
				{
	
				}
			}//变化内容到此
			
			//内容--AUTO模式下，程序界面，第三个按钮的功能
			//姓名--刘旋，时间--2013-3-25
			if (Main.ProgAUTO)
			{
				if(Main.ProgAUTOFlip==0)//“程序”页，按下“当前段”按钮，转到“当前段”页
					Main.ProgAUTOFlip=3;
				//内容--AOTO模式下，程序界面，第三个按钮功能的修改，姓名--刘旋，时间--2013-4-9
				if(Main.ProgAUTOFlip==5)//“下一度”页，按下“当前段”按钮，转到“当前段”页
					Main.ProgAUTOFlip=3;
			}//增加能容到此
			if(Main.ProgMDI)//内容--MDI模式下，程序界面，第三个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
			     if(Main.ProgMDIFlip==0)
					Main.ProgMDIFlip=2;
				else if(Main.ProgMDIFlip==1)
					Main.ProgMDIFlip=2;
				else if(Main.ProgMDIFlip==3)
					Main.ProgMDIFlip=2;
			}
			if(Main.ProgDNC)//内容--DNC模式下，程序界面，第三个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
			    Main.ProgDNCFlip=1;;
			}
			if(Main.ProgHAN)//内容--HAN模式下，程序界面，第三个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
			    Main.ProgHANFlip=1;
			}
			if(Main.ProgJOG || Main.ProgREF)//内容--JOG和REF模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
			    Main.ProgSharedFlip=1;
			}
		}
		//设置界面时按下
		if(Main.SettingMenu)
		{
			if(Main.OffSetOne)
			{
				Main.OffSetTool = false;
				Main.OffSetSetting = false;
				Main.OffSetCoo = true;
			}
			//刀偏界面的C输入
			else if(Main.OffSetTool && Main.OffSetTwo)
			{
				 if(Main.InputText != "")
				{	
					CooSystem_script.C_Input(Main.InputText);		
					Main.InputText = "";
					Main.CursorText.text = Main.InputText;
					Main.ProgEDITCusorPos = 57f;
				}
			}
		}
		
		if(Main.MessageMenu)
		{
			Main.MessageFlip=2;
		}
	}
	
	//软键 Button4
	void FourthButton () 
	{	
		//宋荣
		if(Main.PosMenu)
		{
			if(Main.operationBottomScrInitial)
			{
				Main.operationBottomScrInitial=false;
				Main.operationBottomScrExecute=true;
				Main.runtimeIsBlink=false;
				Main.partsNumBlink=true;
				//Main.operationBottomScrExecute=false;
			}
		}
		//宋荣
		
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT)
			{
				if(Main.ProgEDITFlip == 0)
				{
					
				}//向上检索
				else if(Main.ProgEDITFlip == 1)
				{
					Main.NotFoundWarn = false;
					string SearchWord = Main.EDITText.text.Trim();	
					if(SearchWord != "")
					{
						int CurIndex = Main.SelectEnd - 1;
						int i = 0;
						bool IsFound= false;
						for(i = CurIndex;i >0 && !IsFound;--i)
						{
							if(Main.CodeForAll[i].IndexOf(SearchWord) > -1)
							{
							    Main.SelectStart = i;
								Main.SelectEnd = Main.SelectStart;
								int j = 0;
							    while(Main.SeparatePos[j] <= i){++j;}				
								Main.ProgEDITCusorV = j;
									
								//Debug.Log(Main.ProgEDITCusorV+"fff");	
								if(Main.ProgEDITCusorV > 0)
									Main.ProgEDITCusorH = i - Main.SeparatePos[Main.ProgEDITCusorV - 1];
								else
									Main.ProgEDITCusorH = i;
							    if(Main.ProgEDITCusorV < Main.StartRow )
								{
									Main.StartRow = Main.ProgEDITCusorV;
									Main.EndRow = Main.StartRow + 9;
								}
								IsFound = true;
							}

						}
						if(!IsFound)
						{
							Main.NotFoundWarn = true;
							Main.SelectStart = 0;
							Main.SelectEnd = Main.SelectStart;
							Main.ProgEDITCusorV = 0;
							Main.ProgEDITCusorH = 0;
							Main.StartRow = 0;
						    Main.EndRow = Main.StartRow + 9;
		
						}

					}
				}
				else if(Main.ProgEDITFlip == 2)
				{
					
				}
				else if(Main.ProgEDITFlip == 3||Main.ProgEDITFlip == 5||Main.ProgEDITFlip == 50)
				{
					//剪切				
					Main.CodeBuffer = new List<string>();
					Main.CodeBuffer.Clear();
					//Debug.Log("s"+Main.SelectStart+" "+"e"+Main.SelectEnd);
					//Debug.Log("tot"+Main.CodeForAll.Count);
					int SelStart = Main.SelectStart > Main.SelectEnd? Main.SelectEnd:Main.SelectStart;
					int SelEnd = Main.SelectStart > Main.SelectEnd? Main.SelectStart:Main.SelectEnd;
					for(int i = SelStart;i <= SelEnd;++i)
					    Main.CodeBuffer.Add(Main.CodeForAll[i]);
					MDIEdit_Script.DeleteCode();
				}
				else 
				{
					
				}
			}
			if(Main.ProgAUTO)//内容--AUTO模式下，程序界面，第四个按钮的功能，姓名--刘旋，时间--2013-4-9
			{
				if(Main.ProgAUTOFlip==0)//“程序”页，按下“下一段”按钮，转到“下一段”页
					Main.ProgAUTOFlip=5;
				else if(Main.ProgAUTOFlip==3)//“当前段”页，按下“下一段”按钮，转到“下一段”页
					Main.ProgAUTOFlip=5;
			}
			if(Main.ProgMDI)//内容--MDI模式下，程序界面，第四个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				if(Main.ProgMDIFlip==0)
					Main.ProgMDIFlip=3;
				else if(Main.ProgMDIFlip==1)
					Main.ProgMDIFlip=3;
				else if(Main.ProgMDIFlip==2)
					Main.ProgMDIFlip=3;
			}
			if(Main.ProgDNC)//内容--DNC模式下，程序界面，第四个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgDNCFlip=2;
			}
			if(Main.ProgHAN)//内容--HAN模式下，程序界面，第四个按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgHANFlip=2;
			}
			if(Main.ProgJOG || Main.ProgREF)//内容--JOG和REF模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				Main.ProgSharedFlip=2;
			}
		}
		
		if(Main.SettingMenu)
		{
			//刀片界面+输入
			if(Main.OffSetTool && Main.OffSetTwo)
			{
				if(Main.InputText != "")
				{
					CooSystem_script.Plus_Tool_Input(Main.InputText, true);  //第二页+输入后面的输入
					Main.InputText = "";
					Main.CursorText.text = Main.InputText;
					Main.ProgEDITCusorPos = 57f;
				}
			}
			if(Main.OffSetCoo && Main.OffSetTwo)
			{
				if(Main.InputText != "")
				{
					CooSystem_script.PlusInput(Main.InputText, true);
					Main.InputText = "";
					Main.CursorText.text = Main.InputText;
					Main.ProgEDITCusorPos = 57f;
				}
			}
		}
	}
	
	//软键 Button5
	void FifthButton () {
		//宋荣 position模式下响应函数
		if(Main.PosMenu)
		{
			if(!Main.operationBottomScrInitial&&(Main.RelativeCoo||Main.AbsoluteCoo||Main.GeneralCoo))
			{
				Main.operationBottomScrInitial=true;
				Main.posOperationMode=true;
				if(Main.RelativeCoo)
					Main.statusBeforeOperation=2;
				if(Main.AbsoluteCoo)
					Main.statusBeforeOperation=1;
				if(Main.GeneralCoo)
					Main.statusBeforeOperation=3;
				Main.RelativeCoo=false;
				Main.AbsoluteCoo=false;
				Main.GeneralCoo=false;
				Debug.Log("响应fifth");
			}
			else if(Main.operationBottomScrInitial)
			{
				Main.operationBottomScrInitial=false;
				Main.operationBottomScrExecute=true;
				Main.runtimeIsBlink=true;
				Debug.Log("runtimeIsBlink变为true");
				Main.partsNumBlink=false;
			}
		    else if(Main.operationBottomScrExecute)
			{
				Main.operationBottomScrInitial=true;
				Main.operationBottomScrExecute=false;
				if(Main.runtimeIsBlink)
				{
					Main.RunningTimeH=0;
					Main.RunningTimeM=0;
				}
				if(Main.partsNumBlink)
				{
					Main.PartsNum=0;
				}
				if(preSetSelected)
				{
					if(Main.statusBeforeOperation==1)
				   {
					    Pos_Script.preSetAbsoluteCoo=CooSystem_script.absolute_pos;
						preSetSelected=false;
				 	    Debug.Log("预置成功");
				   }
				}
				Main.runtimeIsBlink=false;
				Main.partsNumBlink=false;
				//
			}
		}
		//宋荣
		
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT||Main.ProgMDI)
			{
				if(Main.ProgEDITProg)
				{
					if(Main.ProgEDITFlip == 0)
						Main.ProgEDITFlip = 1;
					//内容--增加第五个按钮的功能
					//姓名--刘旋
					//日期--2013-3-14
					else if (Main.ProgEDITFlip==2)
				    	Main.ProgEDITFlip=7;//变化内容到此
					
					//按了返回-陈晓威
					else if(Main.ProgEDITFlip==1)
					{
						Main.NotFoundWarn = false;
						Main.ProgEDITCusorV=0;
						Main.ProgEDITCusorH=0;
						Main.StartRow=0;
						Main.EndRow=9;
						Main.SelectStart=0;
						Main.SelectEnd=0;
					}
				}
				if(Main.ProgEDITList)
				{
					if(Main.ProgEDITFlip == 0)
						Main.ProgEDITFlip = 1;
				}
			}
			//内容--AUTO模式下，程序界面第五个按钮的功能
			//姓名--刘旋，时间--2013-3-25
			if(Main.ProgAUTO)
			{
				if (Main.ProgAUTOFlip==0)//“程序”页，按下“操作”按钮，转到“操作”页
					Main.ProgAUTOFlip=1;
				else if(Main.ProgAUTOFlip==2)//“绝对”页，按下“操作”按钮，转到“操作”页
					Main.ProgAUTOFlip=1;
				else if (Main.ProgAUTOFlip==3)//“当前段”页，按下“操作”按钮，转到“操作”页
					Main.ProgAUTOFlip=1;
				//内容--AUTO模式下，程序界面，第五个按钮功能的修改，姓名--刘旋，时间--2013-4-9
				else if(Main.ProgAUTOFlip==4)//“相对”页，按下“操作”按钮，转到“操作”页
					Main.ProgAUTOFlip=1;
				else if(Main.ProgAUTOFlip==5)//“下一段”页，按下“操作”按钮，转到“操作”页
					Main.ProgAUTOFlip=1;
				
			}//增加内容到此
		}
		
		if(Main.SettingMenu)
		{
			if(Main.OffSetTool)
			{
				if(Main.OffSetOne)
				{
					Main.OffSetTwo = true;
					Main.OffSetOne = false;
				}
				//刀偏界面输入功能
				else if(Main.OffSetTool)
				{
					if(Main.InputText != "")
					{
						CooSystem_script.Plus_Tool_Input(Main.InputText, false);
						Main.InputText = "";
						Main.CursorText.text = Main.InputText;
						Main.ProgEDITCusorPos = 57f;
					}
				}
			}
			if(Main.OffSetCoo)
			{
				if(Main.OffSetOne)
				{
					Main.OffSetTwo = true;
					Main.OffSetOne = false;
				}
				else if(Main.OffSetCoo)
				{
					if(Main.InputText != "")
					{
						CooSystem_script.PlusInput(Main.InputText, false);
						Main.InputText = "";
						Main.CursorText.text = Main.InputText;
						Main.ProgEDITCusorPos = 57f;
					}
				}
			}
		}
	}
	
	//向后翻页软键
	void NextPage () {
		//宋荣
		if(Main.PosMenu)
		{
			if(!Main.operationBottomScrInitial&&(Main.RelativeCoo||Main.AbsoluteCoo||Main.GeneralCoo))
			{
				Main.operationBottomScrInitial=true;
				Main.posOperationMode=true;
				if(Main.RelativeCoo)
					Main.statusBeforeOperation=2;
				if(Main.AbsoluteCoo)
					Main.statusBeforeOperation=1;
				if(Main.GeneralCoo)
					Main.statusBeforeOperation=3;
				Main.RelativeCoo=false;
				Main.AbsoluteCoo=false;
				Main.GeneralCoo=false;
			}	
		}
		//宋荣
		if(Main.ProgMenu)
		{
			if(Main.ProgEDIT)
			{
				if(Main.ProgEDITProg)
				{
					if(Main.ProgEDITFlip == 1)	
						Main.ProgEDITFlip = 2;
					else if(Main.ProgEDITFlip == 2)
						Main.ProgEDITFlip = 0; //内容--修改内容，把3改为0，姓名--刘旋，日期--2013-3-14
					//内容--增加向下翻页按钮的功能
					//姓名--刘旋
					//日期--2013-3-14
					else if (Main.ProgEDITFlip==3)
				         Main.ProgEDITFlip=4;
			        else if (Main.ProgEDITFlip==4)
				         Main.ProgEDITFlip=2;
			        else if (Main.ProgEDITFlip==5)
				         Main.ProgEDITFlip=6;
		            else if (Main.ProgEDITFlip==6)
				         Main.ProgEDITFlip=2;//变化内容到此
					else if (Main.ProgEDITFlip==8)
				         Main.ProgEDITFlip=0;
				}
				
				if(Main.ProgEDITList)
				{
					if(Main.ProgEDITFlip == 1)
					{
						Main.ProgEDITFlip = 2;
						Main.ProgEDITProg = true;
						Main.ProgEDITList = false;
					}
				}
			}
			if(Main.ProgAUTO)//内容--AUTO模式下，程序界面，向后翻页按钮功能的修改，姓名--刘旋，时间--2013-4-9
			{
				if(Main.ProgAUTOFlip==1)//“操作”页，按“+”按钮，返回“程序”页
					Main.ProgAUTOFlip=0;
				else if(Main.ProgAUTOFlip==0)//内容--“程序”页，按“+”按钮，返回“操作”页，姓名--刘旋，时间--2013-4-11
					Main.ProgAUTOFlip=1;
				else if(Main.ProgAUTOFlip==3)//内容--“当前段”页，按“+”按钮，返回“操作”页，姓名--刘旋，时间--2013-4-11
					Main.ProgAUTOFlip=1;
				else if(Main.ProgAUTOFlip==5)//内容--“下一段”页，按“+”按钮，返回“操作”页，姓名--刘旋，时间--2013-4-11
					Main.ProgAUTOFlip=1;
			}
			if(Main.ProgMDI)//内容--MDI模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				if(Main.ProgMDIFlip==0)
					Main.ProgMDIFlip=1;
				else if(Main.ProgMDIFlip==1)
					Main.ProgMDIFlip=2;
				else if(Main.ProgMDIFlip==2)
					Main.ProgMDIFlip=3;
			}
			if(Main.ProgDNC)//内容--DNC模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				if(Main.ProgDNCFlip==0)
					Main.ProgDNCFlip=1;
				else if(Main.ProgDNCFlip==1)
					Main.ProgDNCFlip=2;
			}
			if(Main.ProgHAN)//内容--HAN模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				if(Main.ProgHANFlip==0)
					Main.ProgHANFlip=1;
				else if(Main.ProgHANFlip==1)
					Main.ProgHANFlip=2;
			}
			if(Main.ProgJOG || Main.ProgREF)//内容--JOG和REF模式下，程序界面，向后翻页按钮功能的实现，姓名--刘旋，时间--2013-4-22
			{
				if(Main.ProgSharedFlip==0)
					Main.ProgSharedFlip=1;
				else if(Main.ProgSharedFlip==1)
					Main.ProgSharedFlip=2;
			}
		}
		
		if(Main.SettingMenu)
		{
			if(Main.OffSetTool)
			{
				if(Main.OffSetOne)
				{
					Main.OffSetTwo = true;
					Main.OffSetOne = false;
				}
			}
			if(Main.OffSetSetting)
			{
				if(Main.OffSetOne)
				{
					Main.OffSetTwo = true;
					Main.OffSetOne = false;
				}
			}
			if(Main.OffSetCoo)
			{
				if(Main.OffSetOne)
				{
					Main.OffSetTwo = true;
					Main.OffSetOne = false;
				}
			}
		}
		
		if(Main.MessageMenu)
		{
			if(Main.MessageFlip==0)
				Main.MessageFlip=1;
			else if(Main.MessageFlip==1)
				Main.MessageFlip=1;
			else if(Main.MessageFlip==2)
				Main.MessageFlip=2;
		}
	}
	
	/// <summary>
	/// 获取当前目录下符合要求的文件的文件名
	/// </summary>
	public void FileInfoInitialize ( )
	{
		//Judge whether the file directory is right or not
		if(Directory.Exists(document_path))
		{
			string temp_name = "";
			if(Main.current_filenum > 0)
				Main.RealListNum = Main.current_filenum;
			else
				Main.RealListNum = 1;
			//Main.ProgEDITCusor = 175f;
			//考虑到可能增减程序的情况，如果当前有打开程序，先记录下当前的程序号
			if(Main.FileNameList.Count > 0)
				temp_name = Main.FileNameList[Main.RealListNum - 1];
			Main.FileNameList.Clear();
			Main.FileSizeList.Clear();
			Main.FileDateList.Clear();
			Main.ProgUnusedNum = 400;
			Main.ProgUnusedSpace = 512;//内容--内存总容量为512K，姓名--刘旋，时间--2013-3-180;
			Main.ProgUsedNum = 0;
			Main.ProgUsedSpace = 0;
			//Acquire all of the files's name under current directory.
			string[] tempFileList = Directory.GetFiles(document_path);
			if(tempFileList.Length > 0)
			{// 10 level
				FileInfo get_fileinfo;
				int fileSize = 0;
				foreach(string fullname in tempFileList)
				{
					//Regular Expression: 判断文件路径中是否包含指定格式的字符串："O"+"4个数字"+"."+"2或3个字符结尾"
					Regex fullname_Reg = new Regex(@"O\d{4}.\w{2,3}$");
					//Get files's information
					if(fullname_Reg.IsMatch(fullname))
					{
						Main.FileNameList.Add(fullname_Reg.Match(fullname).Value.Substring(0,5));
						get_fileinfo = new FileInfo(fullname);
						Main.FileDateList.Add(get_fileinfo.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss"));
						fileSize = (int)(get_fileinfo.Length / 1024);
						if(fileSize * 1204 < get_fileinfo.Length)
							fileSize++;
						Main.FileSizeList.Add(fileSize);
						Main.ProgUnusedSpace += fileSize;
					}
				}
				//Initialize some arquments for display
				Main.TotalListNum = Main.FileNameList.Count;
				Main.ProgUsedNum = Main.TotalListNum;
				Main.ProgUnusedNum -= Main.ProgUsedNum;
				Main.ProgUnusedSpace -= Main.ProgUsedSpace;
				//考虑到程序有增减的可能性，重新定义位置参数 Main.RealListNum
				if(temp_name != "")
				{
					if(Main.FileNameList.IndexOf(temp_name) != -1)
						Main.RealListNum = Main.FileNameList.IndexOf(temp_name) + 1;
					else
					{
						Main.RealListNum = 1;
						Main.at_position = -1;
						Main.at_page_number = -1;
					}
				}
				string[] TempNameArray = new string[8];
				int[] TempSizeArray = new int[8];
				string[] TempDateArray = new string[8];
				for(int i = 0; i < 8; i++)
				{
					TempNameArray[i] = "";
					TempSizeArray[i] = 0;
					TempDateArray[i] = "";
				}
				int currentpage=(Main.RealListNum-1)/8;	
				int startnum=currentpage*8+1;	
				int finalnum=currentpage*8+8;
				if(finalnum >Main.TotalListNum)				
					finalnum=Main.TotalListNum;	
				int initial_index = -1;
				for(int i = startnum - 1; i < finalnum; i++)
				{
					initial_index++;
					TempNameArray[initial_index] = Main.FileNameList[i];
					TempSizeArray[initial_index] = Main.FileSizeList[i];
					TempDateArray[initial_index] = Main.FileDateList[i];
				}
				for(int i = 0; i < 8; i++)
				{
					Main.CodeName[i] = TempNameArray[i];
					Main.CodeSize[i] = TempSizeArray[i];
					Main.UpdateDate[i] = TempDateArray[i];
				}
			}// 10 level
			else
				Debug.LogWarning("Can't find any file in current working directory. 	Warning caused by Eric.");
		}
		else
			Debug.LogError("The file directory doesn't exist. 	Error caused by Eric.");
	}
	
	/// <summary>
	/// 加载相应路径下的NC文件，并以List<string>类型返回该文件中的所有NC代码
	/// --1--调用了NCFileList函数，获得当前目录下所有符合要求的文件的文件名
	/// --2--上一步操作在这里显得有点多余，其实是因为数控面板中的程序列表可以显示存储器中的文件信息，这是为改善那一步准备的
	/// </summary>
	/// <returns>
	/// 该NC程序文件中的所有NC代码，返回类型：List<string>
	/// </returns>
	/// <param name='filename'>
	/// NC程序的程序名
	/// </param>
	public List<string> CodeLoad (string filename)
	{
		List<string> original_code = new List<string>();
		bool success_open = true;
		List<string> file_name_list = Main.FileNameList; 
		if(file_name_list.Count > 0)
		{//1 level
			//Judge whether the input string is right or not.
			if(filename.StartsWith("O"))
			{//2 level
				string temp_name = filename.Trim('O');
				//Regular Expression: 判断输入的是否为数字字符，且大小在(0,10000)之间
				Regex name_Reg = new Regex(@"^\d{1,4}$");
				if(name_Reg.IsMatch(temp_name))
				{//3 level
					int name_num = Convert.ToInt32(temp_name);
					if(name_num > 0 && name_num < 10)
						temp_name = "O000" + name_num.ToString();
					else if(name_num >= 10 && name_num < 100 )
						temp_name = "O00" + name_num.ToString();
					else if(name_num >= 100 && name_num < 1000)
						temp_name = "O0" + name_num.ToString();
					else
						temp_name = "O" + name_num.ToString();
					if(file_name_list.IndexOf(temp_name) >= 0)
					{//4 level
						string file_path = "";
						FileInfo exist_check = new FileInfo(document_path + temp_name + ".txt");
						if(exist_check.Exists)
							file_path = document_path + temp_name + ".txt";
						else
						{
							exist_check = new FileInfo(document_path + temp_name + ".cnc");
							if(exist_check.Exists)
								file_path = document_path + temp_name + ".cnc";
							else
							{
								exist_check = new FileInfo(document_path + temp_name + ".nc");
								if(exist_check.Exists)
									file_path = document_path + temp_name + ".nc";
								else
									success_open = false;
							}
						}
						//Acquire original code.
						if(success_open)
						{
							//全局变量
							Main.RealListNum = file_name_list.IndexOf(temp_name) + 1;
							Main.ProgramNum = Convert.ToInt32(temp_name.Trim('O'));
							FileStream code_file_stream = new FileStream(file_path, FileMode.Open, FileAccess.Read); 
							StreamReader code_SR = new StreamReader(code_file_stream);
							string s_Line = code_SR.ReadLine();
							while(s_Line != null)
							{
								original_code.Add(s_Line);
								s_Line = code_SR.ReadLine();
							}
							code_SR.Close();
						}
						else
							Debug.LogError("Unexpected error! Program: " + temp_name + " disappears.  Error caused by Eric.");
					}//4 level
					else
						Debug.LogWarning("Can't find Program: " + temp_name + " in current working directory!  Warning caused by Eric.");
				}//3 level
				else
					Debug.LogError("格式错误! Error caused by Eric.");
			}//2 level
			else
				Debug.LogError("格式错误! Error caused by Eric.");
		}//1 level
		else
			Debug.LogWarning("Can't find any file in current working directory. 	Warning caused by Eric.");
		return original_code;
	}
	
	/// <summary>
	/// O检索，加载代码，此处需要初步格式化
	/// </summary>
	public void O_Search() 
	{
		string file_name = "";
		int temp_reallistnum = Main.RealListNum;
		bool open_success = false;
		//无输入或者输入为"O"时
		if(Main.InputText == "" || Main.InputText == "O")
		{
			if(Main.RealListNum < Main.TotalListNum)
				Main.RealListNum++;
			else
				Main.RealListNum=1;
			if(Main.at_position < 0)
				Main.RealListNum=1;
			file_name = Main.FileNameList[Main.RealListNum - 1];
		}
		//有合适的输入时
		else
		{
			file_name = Main.InputText;
		}
		
		//从文件加载NC代码
		List<string> temp_code_list =new List<string>();
		temp_code_list = NCCodeFormat_Script.AllCode(file_name);
		if(temp_code_list.Count > 0)
		{
			if(temp_code_list[temp_code_list.Count-1] == "")
				temp_code_list.RemoveAt(temp_code_list.Count - 1);
			if(temp_code_list.Count > 0)
			{
				Main.CodeForAll.Clear();
				Main.CodeForAll = new List<string>();
				Main.CodeForAll = temp_code_list;
				Main.RealCodeNum=1;
				Main.HorizontalNum=1;
				Main.VerticalNum=1;
				Main.TotalCodeNum=Main.CodeForAll.Count;
				//待修改的函数，祝你好运
				MDIEdit_Script.CodeEdit();
				Main.ProgEDITCusorH=0;
				Main.ProgEDITCusorV=0;
				Main.SelectStart = 0;
				Main.SelectEnd = 0;
				//Main.EDITText.text=Main.TempCodeList[0][0];
				Main.TextSize=Main.sty_EDITTextField.CalcSize(new GUIContent(Main.EDITText.text));
				open_success = true;
				Main.ProgEDITList = false;
				Main.ProgEDITProg = true;
				Main.ProgEDITAt = true;
				Main.at_position = Main.RealListNum%8;	
				Main.at_page_number = (Main.RealListNum - 1)/8;
			}
		}
		if(open_success == false)
		{
			Main.RealListNum = temp_reallistnum;
		}
		Main.InputText="";
		Main .ProgEDITCusorPos=57f;
		Main.EDITText.text="";
	}
	
	/// <summary>
	/// 确定"@"的位置
	/// </summary>
	/// <param name='name_index'>
	/// 当前所打开的程序在列表中的index
	/// </param>
	public void Locate_At_Position (int name_index)
	{
		if(Main.at_position >= 0)
		{
			Main.at_position = name_index%8;
			switch(Main.at_position)
			{
				case 1:
					Main.ProgEDITCusor =175f;
					break;
				case 2:
					Main.ProgEDITCusor =195f;
					break;
				case 3:
					Main.ProgEDITCusor =215f;
					break;
				case 4:
					Main.ProgEDITCusor =235f;
					break;
				case 5:
					Main.ProgEDITCusor =255f;
					break;
				case 6:
					Main.ProgEDITCusor =275f;
					break;
				case 7:
					Main.ProgEDITCusor =295f;
					break;
				case 0:
					Main.ProgEDITCusor =315f;
					break;	
			}
			if(Main.CodeName[0] != "")
			{
				int current_page = Main.FileNameList.IndexOf(Main.CodeName[0]);
				if(current_page >= 0)
				{
					current_page = current_page / 8; 
					if(Main.at_page_number != current_page)
						Main.ProgEDITAt = false;
					else
						Main.ProgEDITAt = true;
				}
				else
					Main.ProgEDITAt = false;
			}
			else
				Main.ProgEDITAt = false;
		}
	}
	
	public void calcSepo(List<string>codelist,int[]codeSeparatePos,float row_length)
	{
		Vector2 word_size = new Vector2(0,0);
		const float blank_length = 10;
		float pos_x = 0;
		int index_str = 0;
	    float cur_length = 0;
		//row_length = 302f, 
		string each_word=null;
		int irow;
		cur_length = getStrLen(codelist[0]);
		for(irow = 0;index_str < codelist.Count ;) 
	    {
				each_word = codelist[index_str];   
			    //word_size = Main.sty_EDITTextField.CalcSize(new GUIContent(each_word));
				//pos_x = (getStrLen(each_word) + 4)/440f*Main.width + 10f; 
				//cur_length += (pos_x) ;
				
				if((index_str+1<codelist.Count-1)&&codelist[index_str+1] != ";")
				{
					float next_wordsize=getStrLen(codelist[index_str+1]);
					cur_length=cur_length+next_wordsize+blank_length;
				}
				++index_str;
				if(each_word.Equals(";")&&index_str<codelist.Count) 
				{
					codeSeparatePos[irow] = index_str;
					
					cur_length=getStrLen(codelist[index_str]);
					++irow;
				}									
				else if((cur_length > row_length)&&(index_str<codelist.Count))
				{
				    //Debug.Log("superpassindex:"+index_str+" "+codelist[index_str]);
					codeSeparatePos[irow] = index_str;
				    //Debug.Log("irow:"+irow);
					cur_length=getStrLen(codelist[index_str]);
					++irow;
				}	
					
			
			
		}
		
		//Debug.Log("calc compelet!"+irow);
		//int j=0;
		//while(codeSeparatePos[j]!=0){Debug.Log("sppp"+j+" "+codeSeparatePos[j]+codelist[codeSeparatePos[j]]);j++;}
	}
	
	public float getStrLen(string str)
	{
		float conlen=0;
		char[] temp=str.ToCharArray();
		for(int i=0;i<temp.Length;i++){
			try{
			conlen+=strlenmap[temp[i]];
			}catch(Exception ee)
			{
				Debug.Log(temp[i]+"not found");
			}
			
		}
		return conlen;	
		
	}
	
	public void StrLenMapInitialize()
	{
		strlenmap=new Dictionary<char, float>();
		//数字
		for(int i=0;i<=9;i++)
			strlenmap.Add((char)('0'+i),9);
		//字母
		strlenmap.Add('A',11);
		strlenmap.Add('B',12);
		strlenmap.Add('C',12);
		strlenmap.Add('D',12);
		strlenmap.Add('E',11);
		strlenmap.Add('F',10);
		strlenmap.Add('G',13);
		strlenmap.Add('H',12);
		strlenmap.Add('I',4);
		strlenmap.Add('J',9);
		strlenmap.Add('K',12);
		strlenmap.Add('L',10);
		strlenmap.Add('M',13);
		strlenmap.Add('N',12);
		strlenmap.Add('O',13);
		strlenmap.Add('P',11);
		strlenmap.Add('Q',13);
		strlenmap.Add('R',12);
		strlenmap.Add('S',11);
		strlenmap.Add('T',10);
		strlenmap.Add('U',12);
		strlenmap.Add('V',11);
		strlenmap.Add('W',17);
		strlenmap.Add('X',11);
		strlenmap.Add('Y',10);
		strlenmap.Add('Z',9);
		//符号
		strlenmap.Add('*',7);
		strlenmap.Add('-',6);
		strlenmap.Add('/',5);
		strlenmap.Add('+',10);
		strlenmap.Add(';',6);
		strlenmap.Add('.',5);
		strlenmap.Add('#',9);
		strlenmap.Add('[',6);
		strlenmap.Add(']',6);
		strlenmap.Add('=',10);
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
