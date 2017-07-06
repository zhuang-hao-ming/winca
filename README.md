# cellular automata land use change simulation winform application

这是一个元胞自动机土地利用变化模拟的winform应用程序。程序提供了*随机森林ca*，*神经网络ca*，*决策树ca*,*逻辑回归ca*共4种不同的元胞自动机土地利用变化模拟算法（算法细节请参阅对应论文）。



## techonologies stack

- GUI: c# winform
- data io: gdal
- machine learning algorithm: accord

## usage

please refer the 

## development

- 使用git clone将项目克隆到本地
```
git clone https://github.com/zhuang-hao-ming/winca.git
```
- 使用vs2015(要求支持使用packages.config管理nuget包)打开.sln文件
- 右键单击solution在弹出菜单中点击还原nuget包
- build项目

## screen shot

- main window:

![main window](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/main_window.jpg)

- setup window:

![setup window](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/random_forest.jpg)

- setup window landuse:

![setup window landuse](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/landuse.jpg)

- setup window transfer control

![transfer control](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/transfer_control.jpg)

- console

![console](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/console.jpg)

- animation

![animation](https://github.com/zhuang-hao-ming/winca/blob/master/doc/screen_shot/running.jpg)