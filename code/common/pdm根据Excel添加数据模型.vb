'============================================================
'从Excel文件中导入PowerDesigner 物理数据模型
'
'注意：1，Excel表格中不能有合并的单元格
'      2，列之间不能有空行
' 装载地址：https://www.cnblogs.com/yitouniu/p/5546984.html
'============================================================


Option Explicit

'============================================================
'私有全局变量。
'============================================================
Private CURRENT_MODEL_NAME
Private CURRENT_MODEL
Private TABLES
Private EXCEL_APP
Private FILE_PATH

CURRENT_MODEL_NAME = "Excel导入"
Set EXCEL_APP = CreateObject("Excel.Application")
FILE_PATH="D:\models.xlsx"    '文件的绝对路径

'检查文件是否存在
If CheckFileExsistence() Then
   '检查当前是否有已经打开的物理图
   Call GetModelByName(CURRENT_MODEL)
   If CURRENT_MODEL Is Nothing Then
      MsgBox("请先打开一个名称为“" & CURRENT_MODEL_NAME & "”的物理数据模型（Physical Data Model），然后再进执行导入！")
   Else
      Set TABLES = CURRENT_MODEL.Tables
      '根据EXCEL表格创建模型
      ImportModels()
   End If
Else
   MsgBox "文件" + FILE_PATH + "不存在！"
End If


'============================================================
'导入模型
'============================================================
Sub ImportModels
   '打开Excel文件
   Dim Filename
   Dim ReadOnly
   EXCEL_APP.Workbooks.Open FILE_PATH

   Dim worksheets
   Dim worksheetCount
   Set worksheets = EXCEL_APP.Worksheets
   worksheetCount = worksheets.Count
   If worksheetCount <= 0 Then
      Exit Sub
   End If
   
   Dim index
   Dim currentSheet
   For index = 1 to worksheetCount
      Set currentSheet = worksheets(index)
      Call CreateTable(currentSheet)
   Next
   
   '关闭Excel文件
   EXCEL_APP.Workbooks.Close
End Sub


'============================================================
'创建表
'============================================================
Sub CreateTable(ByRef worksheet)
   Dim cells
   Set cells = worksheet.Cells
   Dim table
   
   '检查具有相同名称的表是否已经存在
   Call GetTableByName(table, worksheet.Name)
   If table Is Nothing Then
      Set table = TABLES.CreateNew
      Set table = TABLES.CreateNew
      table.Name = cells(1, 1).Value
      table.Code = cells(2, 1).Value
      table.Comment = cells(3, 1).Value
   End If
   
   Dim index
   Dim rows
   Dim col
   Set rows = worksheet.Rows
   For index = 4 to 512
      If EXCEL_APP.WorksheetFunction.CountA(rows(index)) <= 0 Then
         Exit For
      End If
      
      '判断列是否已经存在
      If Not ColumnExists(table, cells(index, 1).Value) Then
         Set col = table.Columns.CreateNew
         col.Name = cells(index, 1).Value                  '字段的中文含义
         col.Code = cells(index, 2).Value                  '字段名
         col.Unit = cells(index, 4).Value                  '字段的单位
         col.DataType = cells(index, 3).Value              '字段的数据类型
         'col.DataType = GenerateDataType(cells(index, 3).Value, cells(index, 4).Value, cells(index, 5).Value)              '字段的数据类型
         col.Comment = cells(index, 5).Value               '字段的注释
      End If      
   Next
End Sub


'============================================================
'检查文件是否存在
'============================================================
Function CheckFileExsistence
   Dim fso
   Set fso = CreateObject("Scripting.FileSystemObject")
   CheckFileExsistence = fso.FileExists(FILE_PATH)
End Function


'============================================================
'根据数据类型名称，精度和刻度生成数据类型
'============================================================
Function GenerateDataType(dataTypeName, precision, scale)
   Select Case Ucase(dataTypeName)
      Case Empty
         GenerateDataType = Empty
      Case "NUMBER"
         GenerateDataType = "NUMBER(" & precision & "," & scale & ")"
   End Select
End Function


'============================================================
'获取指定指定名称的数据模型
'============================================================
Sub GetModelByName(ByRef model)
   Dim md
   For Each md in Models
      If StrComp(md.Name, CURRENT_MODEL_NAME) = 0 Then
         Set model = md
         Exit Sub
      End If
   Next
   Set model = Nothing
End Sub


'============================================================
'根据表名称获取对应的表
'============================================================
Sub GetTableByName(ByRef table, tableName)
   Dim tb
   For Each tb in TABLES
      If StrComp(tb.Name, tableName) = 0 Then
         Set table = tb
         Exit Sub
      End If
   Next
   Set table = Nothing
End Sub


'============================================================
'检查字段是否已经存在
'============================================================
Function ColumnExists(ByRef table, columnName)
   Dim col
   For Each col in table.Columns
      If StrComp(col.Name, columnName) = 0 Then
         ColumnExists = True
         Exit Function
      End If
   Next
   ColumnExists = False
End Function