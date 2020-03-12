'============================================================
'pdm是一个数据库设计软件
' 统一添加字段
'============================================================

Option   Explicit    
ValidationMode   =   True    
InteractiveMode   =   im_Batch    
  
Dim   mdl   '   the   current   model    
  
'   get   the   current   active   model    
Set   mdl   =   ActiveModel    
If   (mdl   Is   Nothing)   Then    
      MsgBox   "There   is   no   current   Model "    
ElseIf   Not   mdl.IsKindOf(PdPDM.cls_Model)   Then    
      MsgBox   "The   current   model   is   not   an   Physical   Data   model. "    
Else    
      ProcessFolder   mdl    
End   If    
  
Private   sub   ProcessFolder(folder)    
On Error Resume Next   
      Dim   Tab   'running     table    
      for   each   Tab   in   folder.tables    
            if   not   tab.isShortcut   then    
                  'if tab.comment="" then
                     'tab.comment   =  tab.name
                  'end if   
                  Dim isNeedAdd:isNeedAdd = true
                  Dim   col   '   running   column    
                  for   each   col   in   tab.columns    
                     if col.code="create_tm" then   
                         isNeedAdd = false   
                     end if  
                  next
                  if isNeedAdd then
                     Set col = tab.columns.CreateNew '创建一列/字段
                     If not col is Nothing then
                        col.name = "create_time"
                        col.code = "create_time"
                        col.comment = "创建时间"
                        col.DataType = "TIMESTAMP"
                        col.Mandatory = 1
                        output " col.name: " + col.name
                     End If
                     set col = nothing
                     
                     Set col = tab.columns.CreateNew '创建一列/字段
                     If not col is Nothing then
                        col.name = "create_user"
                        col.code = "create_user"
                        col.comment = "创建人"
                        col.DataType = "varchar(64)"
                        col.Mandatory = 0
                        output " col.name: " + col.name
                     End If
                     set col = nothing
                     
                  end if  
            end   if    
      next    
  
      Dim   view   'running   view    
      for   each   view   in   folder.Views    
            if   not   view.isShortcut   then    
                  view.name   =   view.comment    
            end   if    
      next    
  
      '   go   into   the   sub-packages    
      Dim   f   '   running   folder    
      For   Each   f   In   folder.Packages    
            if   not   f.IsShortcut   then    
                  ProcessFolder   f    
            end   if    
      Next    
end   sub 