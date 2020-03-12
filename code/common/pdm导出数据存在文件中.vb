' pdm是一个数据库设计软件
' 将 PowerDesigner 15 Physical Data Model File 模型中的数据库表的相关内容以JSON的数据格式 提取到xxx.txt文件中

Option Explicit  
ValidationMode = True  
InteractiveMode = im_Batch   
Dim mdl,objFSO,objTextFile ' 当前模型  
' 获取当前模型  
Set mdl = ActiveModel  
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objTextFile = objFSO.OpenTextFile ("D:\tableJson.txt", 8, True)
If (mdl Is Nothing) Then  
   MsgBox "没有打开一个模型" 
ElseIf Not mdl.IsKindOf(PdPDM.cls_Model) Then  
   MsgBox "当前模型不是一个PDM" 
Else  
'调用处理程序  
   ProcessFolder mdl  
End If    
'调用的处理程序 
Private sub ProcessFolder(folder)
   Dim Tab '要处理的表  
   objTextFile.write("[")
   for each Tab in folder.Tables    
        objTextFile.write("{code:'" + Tab.code +"',name:'" + Tab.name+"',classname:'',path:'/',columns:[")
         Dim col ' 要处理的列  
         for each col in Tab.columns  
            objTextFile.write("{code: '")
            objTextFile.write(col.code)
            objTextFile.write("',name:'" + col.name + "',datatype:'" + col.datatype + "',length:'")
            objTextFile.write(col.length)
            objTextFile.write("',precision:'")
            objTextFile.write(col.precision)
            objTextFile.writeline("'},")
         next  
      'end if 
      objTextFile.writeline("]},")
   next  
   objTextFile.write("]") 
end sub
