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

The program is designed with oop(object oriented programming) and we use multithread technology to improve performance.

## usage

please refer [usage manual](https://github.com/zhuang-hao-ming/winca/blob/master/doc/manual.pdf) for a pdf usage manual.
please refer [tutorial video](https://github.com/zhuang-hao-ming/winca/blob/master/doc/usage_tutorial.wmv) for a tutorial vedio.

## development

- clone the repository to local using git clone
```
git clone https://github.com/zhuang-hao-ming/winca.git
```
- open the .sln file in visual studio 2015(or other versions that use a packages.config file to manage nuget)
- right click the solution and choose restore nuget packages
- build and run the project

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