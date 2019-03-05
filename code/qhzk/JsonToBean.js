/**
 * 使用 './PdmToJson.sql' 脚本从pdm中生成Json文件
 * pdm是一个数据库设计软件
 */

const inits = {
  init_package: 'com.qhzk.interfaces.define.bean',
  init_path: 'D:\\PDMClass\\'
}

let tableJson = []

let createJavaClass = function(tables, init) {
	  var work;
	  try {
	    work = new ActiveXObject('Scripting.FileSystemObject');
	  } catch (e) {
	    console.log('当前浏览器不支持')
	    return;
	  }
	  
	  var au = [
	  	  'pkid',
		  'owner',
		  'ownergroup',
		  'ownertype',
		  'creator',
		  'creatorgroup',
		  'creatortype',
		  'createtime',
		  'deleter',
		  'deletegroup',
		  'deletetype',
		  'deletetime',
		  'lastmodify',
		  'lastmodifygroup',
		  'lastmodifytype',
		  'lastmodifiedtime',
		  'isdelete',
		  'description'
		];
		
	  tables.forEach(function(item){
	    var fo = work.createtextfile(init.init_path + item.classname+'.java',8,true);
	    
	    fo.writeLine('package '+ init.init_package+";");
	    fo.writeLine();
	    fo.writeLine("@Table(DatabaseObjectName = \""+ item.code +"\"  ,ServicePath = \"/"+ item.path +"\",indexNum= \"50003\", Common = \"" + item.name + "\")");
	    fo.writeLine("public class "+ item.classname +"  extends ASoftDeleteBean {");
	    
	    item.column.forEach(function(col){
	    	if(au.indexOf(col.code) == -1){
    		  if("bigint" == col.datatype){
		    	fo.writeLine();
		      	fo.writeLine("	@DBField( Nullable = Nullable.Nullable,Commont=\""+col.name+"\", Precision=20, Scale=0, Unique = false, InnerDataType = InnerDataType.LONG,SQLType = Types.NUMERIC)");
		      	fo.writeLine("	private long "+col.code+";");
		      	fo.writeLine();
		      }else if(col.datatype.indexOf('char') > -1){
		      	fo.writeLine("	@DBField(Nullable = Nullable.Nullable, Commont = \""+col.name+"\", Length = "+col.length+", Unique = false, HasIndexed = true, IndexType = IndexType.BTree, CanConditionable = true)");
		      	fo.writeLine("	private String "+col.code+";");
		      	fo.writeLine();
		      }else if(col.datatype.indexOf('decimal') > -1){
		    	fo.writeLine("	@DBField( Nullable = Nullable.Nullable,Commont=\""+col.name+"\", Precision= "+col.length+", Scale= "+col.precision+", Unique = false, HasIndexed = true, InnerDataType = InnerDataType.DOUBLE,SQLType = Types.DECIMAL)");
		      	fo.writeLine("	private "+ col.datatype +" "+col.code+";");
		      	fo.writeLine();
		      }else if(col.datatype.indexOf('datetime') > -1){
		    	fo.writeLine("	@DBField( Nullable = Nullable.Nullable,Commont=\""+col.name+"\", Precision= "+col.length+", Scale= "+col.precision+", Unique = false, HasIndexed = true, InnerDataType = InnerDataType.DATE,SQLType = Types.DATE)");
		      	fo.writeLine("	private "+ col.datatype +" "+col.code+";");
		      	fo.writeLine();
		      }else{
		      	fo.writeLine("	@DBField( Nullable = Nullable.Nullable,Commont=\""+col.name+"\", Precision= "+col.length+", Scale= "+col.precision+", Unique = false, HasIndexed = true, InnerDataType = InnerDataType.LONG,SQLType = Types.NUMERIC)");
		      	fo.writeLine("	E-private "+ col.datatype +" "+col.code+";");
		      	fo.writeLine();
		      }
	    	}
	    })
	    fo.writeLine("}");
	  })
	}
