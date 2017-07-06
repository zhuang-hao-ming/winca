# cellular automata land use change simulation winform application

This is a *cellular automata(CA)* land use change simulation winform application.
It includes four different CA algorithms.
There are:
1. random forest ca
2. neural network ca
3. decision tree ca
4. logistic regression ca
**please reference releated paper for the detail of these four algorithms**

You can try all of them and choose one that is sutiable for your requirements.

## techonologies stack

- GUI: c# winform
- data io: gdal
- machine learning algorithm: accord


## usage

please refer [usage manual](https://github.com/zhuang-hao-ming/winca/blob/master/doc/manual.pdf) for a pdf usage manual.
please refer [tutorial video](https://github.com/zhuang-hao-ming/winca/blob/master/doc/usage_tutorial.wmv) for a tutorial vedio.

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