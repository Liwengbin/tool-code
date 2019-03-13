首先谈一谈遍历一个数组的目的是什么？获取数组中的数据、对数组中的数据进行数据处理、筛选符合条件的数据...。`Array.prototype`中也提供了几个循环方法`filter()、map()、find()`来处理数据。

```js
let array = ["one","two","three"];
```
####  ①普通`for`循环遍历 【推荐⭐⭐⭐⭐】
```js
for(var i = 0,len = array.length;i < len;i++){
    console.log(array[i]);
}
```
#### ② `Array.prototype.forEach()` 【推荐⭐⭐⭐】
```js
array.forEach(function(item,index,arr){
    console.log(item,index,arr);
});
```

#### ③ `for-in` 【推荐⭐⭐】
```js
'如果数组是稀疏数组，使用该方法遍历的次数最少'
var xshu = [];
xshu[1] = "one";
xshu[66] = "two";
xshu[999] = "three";
'for-in 遍历 3次'
'for 循环遍历 10000 次'

for(index in array){
    console.log(array[index]);
}
```

#### ④ for-of（ES6中新增）【推荐⭐⭐⭐】
它的出现主要是为了解决ES5中3种遍历方式的缺陷：
`forEach 不能break 或者return`

`for-in` 的缺点:
`它不仅遍历了数组中的元素，还遍历了自定义属性，甚至连原型链上的属性都被访问到。`

使用`for-of`的优势:<br>
```
①这是最简洁、直接遍历数组的方式
②这个方法避开了for-in循环的缺陷
③与forEach不同，它可以正确响应break,continue,return 语句。
```
缺点:<br>
`for-of不支持普通对象遍历，只能遍历可迭代对象`

```js
for(item of array){
    console.log(item);
}
```

#### ⑤ `Array.prototype.filter()` 方法[过滤、筛选] 将符合条件的数据存到一个新数组中
```js
var words = ['spray', 'limit', 'elite', 'exuberant', 'destruction', 'present'];

const result = words.filter((word) => {return word.length > 6});

console.log(result);
// expected output: Array ["exuberant", "destruction", "present"]
```
#### ⑥ `Array.prototype.map()` 方法对数据进行处理然后返回保存到一个新数组中
```js
var array = [1, 4, 9, 16];

// pass a function to map
const map1 = array.map((x) => { return x * 2});

console.log(map1);
// expected output: Array [2, 8, 18, 32]
```
#### ⑦ `Array.prototype.find()` 返回符合条件的元素，之后的值不会再执行函数，如果没有符合条件的元素则返回`undefined`
```js
var array = [5, 12, 8, 130, 44];

var found = array.find(function(element) {
  return element > 10;
});

console.log(found);
// expected output: 12
```


综上分析：使用什么遍历方法取决于具体的业务需求，如果只是遍历一个数组获取数据，那普通`for`循环的性能是最好的，在遍历数组过程中避免使用`for-in`。
