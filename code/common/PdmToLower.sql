/*
*pdm是一个数据库设计软件
*将数据库表中的所有大写转为小写 包括表名，列名
*/
Option Explicit  
ValidationMode = True  
InteractiveMode = im_Batch  
Dim mdl ' 当前模型  
' 获取当前模型  
Set mdl = ActiveModel  
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
   for each Tab in folder.Tables  
    ' if not Tab.isShortcut then  
        ' Tab.code = tab.name  
        '表名处理，前边添加前缀，字母小写  
        Tab.name=  LCase(Tab.name)  
        Tab.code= LCase(Tab.code)  
         Dim col ' 要处理的列  
         for each col in Tab.columns  
            '列名称和code全部小写，转大写时UCase  
            col.code= LCase(col.code)  
            col.name= LCase(col.name)  
         next  
      'end if 
   next    
' 处理视图  
'  Dim view 'running view  
'   for each view in folder.Views  
   '   if not view.isShortcut then  
       '  view.code = view.name  
    '  end if 
  ' next     
   ' 递归进入 sub-packages  
   Dim f ' sub  folder  
   For Each f In folder.Packages  
      if not f.IsShortcut then  
         ProcessFolder f  
      end if 
   Next  
end sub 
